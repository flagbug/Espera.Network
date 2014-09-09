using System;
using Newtonsoft.Json;

namespace Espera.Network
{
    public class NetworkSong
    {
        public string Album { get; set; }

        public string Artist { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public byte[] ArtworkData { get; set; }

        public TimeSpan Duration { get; set; }

        public string Genre { get; set; }

        public Guid Guid { get; set; }

        public NetworkSongSource Source { get; set; }

        public string Title { get; set; }

        public int TrackNumber { get; set; }
    }
}