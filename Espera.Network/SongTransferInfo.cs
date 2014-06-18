using System;

namespace Espera.Network
{
    public class SongTransferInfo
    {
        public NetworkSong Metadata { get; set; }

        public Guid TransferId { get; set; }
    }
}