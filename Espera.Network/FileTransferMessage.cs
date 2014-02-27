using System;

namespace Espera.Network
{
    public class FileTransferMessage
    {
        public byte[] Data { get; set; }

        public Guid TransferId { get; set; }
    }
}