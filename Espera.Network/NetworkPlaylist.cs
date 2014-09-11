using System.Collections.ObjectModel;
using System;
using Newtonsoft.Json;

namespace Espera.Network
{
    public class NetworkPlaylist
    {
        [JsonProperty(Required = Required.AllowNull)]
        public int? CurrentIndex { get; set; }

        [JsonProperty(Required = Required.Always)]
        public TimeSpan CurrentTime { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        public NetworkPlaybackState PlaybackState { get; set; }

        [JsonProperty(Required = Required.Always)]
        public ReadOnlyCollection<NetworkSong> Songs { get; set; }

        [JsonProperty(Required = Required.Always)]
        public TimeSpan TotalTime { get; set; }
    }
}