using System;

namespace Espera.Network
{
    public class SongTransferMessage
    {
        public byte[] Data { get; set; }

        public Guid TransferId { get; set; }
    }
}