using System;
using System.IO;
using System.Threading.Tasks;
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

        public class TheReadNextFileTransferMessageAsyncMethod
        {
            [Fact]
            public async Task SmokeTest()
            {
                var message = new FileTransferMessage
                {
                    Data = new byte[] { 0, 1, 0, 1 },
                    TransferId = Guid.NewGuid()
                };

                byte[] packedMessage = await NetworkHelpers.PackFileTransferMessageAsync(message);

                using (var ms = new MemoryStream(packedMessage))
                {
                    FileTransferMessage unpackedMessage = await ms.ReadNextFileTransferMessageAsync();

                    Assert.Equal(message.Data, unpackedMessage.Data);
                    Assert.Equal(message.TransferId, unpackedMessage.TransferId);
                }
            }
        }
    }
}