using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace Espera.Network
{
    public static class NetworkHelpers
    {
        public static async Task<byte[]> PackFileTransferMessageAsync(FileTransferMessage message)
        {
            byte[] serialized;

            using (var ms = new MemoryStream())
            {
                using (var writer = new BsonWriter(ms))
                {
                    var serializer = new JsonSerializer();

                    await Task.Run(() => serializer.Serialize(writer, message));

                    serialized = ms.ToArray();
                }
            }

            byte[] length = BitConverter.GetBytes(serialized.Length); // We have a fixed size of 4 bytes

            var returnData = new byte[length.Length + serialized.Length];

            // We could simply call .ToArray() everywhere, but with Buffer.BlockCopy, we are
            // reducing memory pressure and CPU time on mobile devices by an order of magnitude
            Buffer.BlockCopy(length, 0, returnData, 0, length.Length);
            Buffer.BlockCopy(serialized, 0, returnData, length.Length, serialized.Length);

            return returnData;
        }

        public static async Task<byte[]> PackMessageAsync(NetworkMessage message)
        {
            string serialized = await Task.Run(() => JsonConvert.SerializeObject(message, Formatting.None));
            byte[] contentBytes = Encoding.UTF8.GetBytes(serialized);

            contentBytes = await CompressDataAsync(contentBytes);

            byte[] length = BitConverter.GetBytes(contentBytes.Length); // We have a fixed size of 4 bytes

            var returnData = new byte[length.Length + contentBytes.Length];

            Buffer.BlockCopy(length, 0, returnData, 0, length.Length);
            Buffer.BlockCopy(contentBytes, 0, returnData, length.Length, contentBytes.Length);

            return returnData;
        }

        public static async Task<FileTransferMessage> ReadNextFileTransferMessageAsync(this Stream stream)
        {
            byte[] messageLength = await stream.ReadAsync(4);

            if (messageLength.Length == 0)
            {
                return null;
            }

            int realMessageLength = BitConverter.ToInt32(messageLength, 0);

            byte[] messageContent = await stream.ReadAsync(realMessageLength);

            if (messageContent.Length == 0)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream(messageContent))
            {
                using (var reader = new BsonReader(memoryStream))
                {
                    var deserializer = new JsonSerializer();

                    return deserializer.Deserialize<FileTransferMessage>(reader);
                }
            }
        }

        /// <summary>
        /// Reads the next message for the Espera protocol from the stream.
        /// </summary>
        /// <returns>
        /// The uncompressed, deserialized message in JSON, or null, if the underlying client has
        /// closed the connection.
        /// </returns>
        public static async Task<NetworkMessage> ReadNextMessageAsync(this Stream stream)
        {
            byte[] messageLength = await stream.ReadAsync(4);

            if (messageLength.Length == 0)
            {
                return null;
            }

            int realMessageLength = BitConverter.ToInt32(messageLength, 0);

            byte[] messageContent = await stream.ReadAsync(realMessageLength);

            if (messageContent.Length == 0)
            {
                return null;
            }

            using (var contentStream = new MemoryStream(messageContent))
            {
                using (var gzipStream = new GZipStream(contentStream, CompressionMode.Decompress))
                {
                    using (var sr = new StreamReader(gzipStream, Encoding.UTF8))
                    {
                        using (var reader = new JsonTextReader(sr))
                        {
                            var serializer = new JsonSerializer();

                            return await Task.Run(() => serializer.Deserialize<NetworkMessage>(reader));
                        }
                    }
                }
            }
        }

        private static async Task<byte[]> CompressDataAsync(byte[] data)
        {
            using (var targetStream = new MemoryStream())
            {
                using (var stream = new GZipStream(targetStream, CompressionMode.Compress))
                {
                    await stream.WriteAsync(data, 0, data.Length);

                    return targetStream.ToArray();
                }
            }
        }

        private static async Task<byte[]> ReadAsync(this Stream stream, int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException("length", "Length must be greater than 0");

            int count = 0;
            var buffer = new byte[length];

            do
            {
                int read = await stream.ReadAsync(buffer, count, length - count);
                count += read;

                // The client has closed the connection
                if (read == 0)
                    return new byte[0];
            }
            while (count < length);

            return buffer;
        }
    }
}