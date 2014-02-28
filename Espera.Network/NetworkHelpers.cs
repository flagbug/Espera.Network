using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Espera.Network
{
    public static class NetworkHelpers
    {
        public static async Task<byte[]> PackFileTransferMessageAsync(FileTransferMessage message)
        {
            using (var ms = new MemoryStream())
            {
                var serializer = new JsonSerializer();

                using (var writer = new BsonWriter(ms))
                {
                    await Task.Run(() => serializer.Serialize(writer, message));

                    byte[] length = BitConverter.GetBytes(ms.Length); // We have a fixed size of 4 bytes

                    var returnData = new byte[length.Length + ms.Length];

                    // We could simply call .ToArray() everywhere, but with Buffer.BlockCopy, we are
                    // reducing memory pressure and CPU time on mobile devices by an order of magnitude
                    Buffer.BlockCopy(length, 0, returnData, 0, length.Length);
                    Buffer.BlockCopy(ms.ToArray(), 0, returnData, length.Length, (int)ms.Length);

                    return returnData;
                }
            }
        }

        public static async Task<byte[]> PackMessageAsync(NetworkMessage message)
        {
            byte[] contentBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message, Formatting.None));

            contentBytes = await CompressDataAsync(contentBytes);

            byte[] length = BitConverter.GetBytes(contentBytes.Length); // We have a fixed size of 4 bytes

            return length.Concat(contentBytes).ToArray();
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

            byte[] decompressed = await DecompressDataAsync(messageContent);
            string decoded = Encoding.UTF8.GetString(decompressed);

            var message = JObject.Parse(decoded).ToObject<NetworkMessage>();

            return message;
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

        private static async Task<byte[]> DecompressDataAsync(byte[] data)
        {
            using (var sourceStream = new MemoryStream(data))
            {
                using (var stream = new GZipStream(sourceStream, CompressionMode.Decompress))
                {
                    using (var targetStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(targetStream);
                        return targetStream.ToArray();
                    }
                }
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