using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Espera.Network.Tests
{
    public class NetworkHelpersTest
    {
        public class TheIsPortValidMethod
        {
            [Fact]
            public void ValidatesMaximumPort()
            {
                Assert.True(NetworkHelpers.IsPortValid(NetworkConstants.MaxPort));
                Assert.False(NetworkHelpers.IsPortValid(NetworkConstants.MaxPort + 1));
            }

            [Fact]
            public void ValidatesMinimumPort()
            {
                Assert.True(NetworkHelpers.IsPortValid(NetworkConstants.MinPort));
                Assert.False(NetworkHelpers.IsPortValid(NetworkConstants.MinPort - 1));
            }
        }

        public class ThePackMessageAsyncMethod
        {
            [Fact]
            public async Task SmokeTest()
            {
                var message = new NetworkMessage
                {
                    MessageType = NetworkMessageType.Request,
                    Payload = JObject.FromObject(new RequestInfo
                    {
                        RequestAction = RequestAction.GetConnectionInfo,
                        RequestId = new Guid()
                    })
                };

                byte[] packed = await NetworkHelpers.PackMessageAsync(message);
            }
        }

        public class TheReadNextFileTransferMessageAsyncMethod
        {
            [Fact]
            public async Task SmokeTest()
            {
                var message = new SongTransferMessage
                {
                    Data = new byte[] { 0, 1, 0, 1 },
                    TransferId = Guid.NewGuid()
                };

                byte[] packedMessage = await NetworkHelpers.PackFileTransferMessageAsync(message);

                using (var ms = new MemoryStream(packedMessage))
                {
                    SongTransferMessage unpackedMessage = await ms.ReadNextFileTransferMessageAsync();

                    Assert.Equal(message.Data, unpackedMessage.Data);
                    Assert.Equal(message.TransferId, unpackedMessage.TransferId);
                }
            }
        }
    }
}