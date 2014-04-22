﻿namespace Espera.Network
{
    /// <summary>
    /// A listing of actions that a request can perform.
    ///
    /// Common actions are in the range of 0 - 99.
    /// Library actions are in the range of 100 - 199.
    /// Playlist and playback actions are on the range of 200 - 299.
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
        AddPlaylistSongNow = 202,

        /// <summary>
        /// Removes the song from the playlist.
        /// </summary>
        RemovePlaylistSong = 203,

        /// <summary>
        /// Play a song in the playlist.
        /// </summary>
        PlayPlaylistSong = 204,

        /// <summary>
        /// Pause the currently playing song.
        /// </summary>
        PauseSong = 205,

        /// <summary>
        /// Continue the currently paused song.
        /// </summary>
        ContinueSong = 206,

        /// <summary>
        /// Play the next song in the playlist.
        /// </summary>
        PlayNextSong = 207,

        /// <summary>
        /// Play the previous song in the playlist.
        /// </summary>
        PlayPreviousSong = 208,

        /// <summary>
        /// Gets the current volume.
        /// </summary>
        GetVolume = 209,

        /// <summary>
        /// Sets the current volume.
        /// </summary>
        SetVolume = 210,

        /// <summary>
        /// Votes for a song.
        /// </summary>
        VoteForSong = 211,
    }
}