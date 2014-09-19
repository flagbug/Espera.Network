namespace Espera.Network
{
    /// <summary>
    /// A listing of actions that a request can perform.
    ///
    /// Common actions are in the range of 0 - 99.
    /// Library actions are in the range of 100 - 199.
    /// Playlist and playback actions are in the range of 200 - 299.
    /// UI actions are in the range of 300 - 399.
    /// </summary>
    public enum RequestAction
    {
        /// <summary>
        /// Get the initial connection information, such as the access permission and server version.
        /// </summary>
        GetConnectionInfo = 0,

        /// <summary>
        /// Get the library stored on the server.
        /// </summary>
        GetLibraryContent = 100,

        GetSoundCloudSongs = 101,

        GetYoutubeSongs = 102,

        /// <summary>
        /// Get the currently active playlist.
        /// </summary>
        GetCurrentPlaylist = 200,

        /// <summary>
        /// Add one or more songs to the playlist.
        /// </summary>
        AddPlaylistSongs = 201,

        /// <summary>
        /// Add one or more songs to the playlist and play them immediately.
        /// </summary>
        AddPlaylistSongsNow = 202,

        /// <summary>
        /// Removes the song from the playlist.
        /// </summary>
        RemovePlaylistSong = 203,

        /// <summary>
        /// Moves the song up in the playlist.
        /// </summary>
        MovePlaylistSongUp = 204,

        /// <summary>
        /// Moves the song down in the playlist.
        /// </summary>
        MovePlaylistSongDown = 205,

        /// <summary>
        /// Play an existing song in the playlist.
        /// </summary>
        PlayPlaylistSong = 206,

        /// <summary>
        /// Pause the currently playing song.
        /// </summary>
        PauseSong = 207,

        /// <summary>
        /// Continue the currently paused song.
        /// </summary>
        ContinueSong = 208,

        /// <summary>
        /// Play the next song in the playlist.
        /// </summary>
        PlayNextSong = 209,

        /// <summary>
        /// Play the previous song in the playlist.
        /// </summary>
        PlayPreviousSong = 210,

        /// <summary>
        /// Gets the current volume.
        /// </summary>
        GetVolume = 211,

        /// <summary>
        /// Sets the current volume.
        /// </summary>
        SetVolume = 212,

        /// <summary>
        /// Votes for a song.
        /// </summary>
        VoteForSong = 213,

        /// <summary>
        /// Tells the file transfer handler that a song will be sent next.
        /// </summary>
        QueueRemoteSong = 214,

        /// <summary>
        /// Sets the current playback time.
        /// </summary>
        SetCurrentTime = 215,

        /// <summary>
        /// Toggles the open state of the YouTube player flyout.
        /// </summary>
        ToggleYoutubePlayer = 300
    }
}