using System.Collections.ObjectModel;

namespace Espera.Network
{
    public class NetworkPlaylist
    {
        public int? CurrentIndex { get; set; }

        public string Name { get; set; }

        public int? RemainingVotes { get; set; }

        public ReadOnlyCollection<NetworkSong> Songs { get; set; }
    }
}