using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.IO;
using System.IO.Compression;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Espera.Network
{
    public static class NetworkHelpers
    {
        public static async Task<byte[]> PackFileTransferMessageAsync(FileTransferMessage message)
        {
            byte[] serialized;

            using (var ms = new MemoryStream())
            {
                var serializer = new JsonSerializer();

                using (var writer = new BsonWriter(ms))
                {
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

        public static async Task<FileTransferMessage> ReadNextFileTransferMessageAsync(this TcpClient client)
        {
            byte[] messageLength = await client.ReadAsync(4);

            if (messageLength.Length == 0)
            {
                return null;
            }

            int realMessageLength = BitConverter.ToInt32(messageLength, 0);

            byte[] messageContent = await client.ReadAsync(realMessageLength);

            if (messageContent.Length == 0)
            {
                return null;
            }

            using (var stream = new MemoryStream(messageContent))
            {
                await stream.WriteAsync(messageContent, 0, messageContent.Length);

                var deserializer = new JsonSerializer();

                using (var reader = new BsonReader(stream))
                {
                    return deserializer.Deserialize<FileTransferMessage>(reader);
                }
            }
        }

        /// <summary>
        /// Reads the next message for the Espera protocol from the TCP client.
        /// </summary>
        /// <returns>
        /// The uncompressed, deserialized message in JSON, or null, if the underlying client has
        /// closed the connection.
        /// </returns>
        public static async Task<NetworkMessage> ReadNextMessageAsync(this TcpClient client)
        {
            byte[] messageLength = await client.ReadAsync(4);

            if (messageLength.Length == 0)
            {
                return null;
            }

            int realMessageLength = BitConverter.ToInt32(messageLength, 0);

            byte[] messageContent = await client.ReadAsync(realMessageLength);

            if (messageContent.Length == 0)
            {
                return null;
            }

            using (var contentStream = new MemoryStream(messageContent))
            {
                using (var stream = new GZipStream(contentStream, CompressionMode.Decompress))
                {
                    using (var sr = new StreamReader(stream, Encoding.UTF8))
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
                }

                return targetStream.ToArray();
            }
        }

        private static async Task<byte[]> ReadAsync(this TcpClient client, int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException("length", "Length must be greater than 0");

            int count = 0;
            var buffer = new byte[length];

            do
            {
                int read = await client.GetStream().ReadAsync(buffer, count, length - count);
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