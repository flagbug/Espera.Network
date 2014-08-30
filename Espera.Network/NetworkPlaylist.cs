using System.Collections.ObjectModel;
using System;

namespace Espera.Network
{
    public class NetworkPlaylist
    {
        public int? CurrentIndex { get; set; }

        public TimeSpan CurrentTime { get; set; }

        public string Name { get; set; }

        public NetworkPlaybackState PlaybackState { get; set; }

        public ReadOnlyCollection<NetworkSong> Songs { get; set; }

        public TimeSpan TotalTime { get; set; }
    }
}