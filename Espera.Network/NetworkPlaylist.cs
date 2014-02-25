using System.Collections.Generic;

namespace Espera.Network
{
    public class Playlist
    {
        public int? CurrentIndex { get; set; }

        public string Name { get; set; }

        public int? RemainingVotes { get; set; }

        public IEnumerable<NetworkSong> Songs { get; set; }
    }
}