using System;
using Newtonsoft.Json;

namespace Espera.Network
{
    public class NetworkSong
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Album { get; set; }

        /// <summary>
        /// The artist or uploader of a song.
        /// </summary>
        public string Artist { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ArtworkKey { get; set; }

        public TimeSpan Duration { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Genre { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Guid Guid { get; set; }

        /// <summary>
        /// YouTube views or SoundCloud playbacks
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int PlaybackCount { get; set; }

        [JsonProperty(Required = Required.Always)]
        public NetworkSongSource Source { get; set; }

        public string Title { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int TrackNumber { get; set; }
    }
}