using NowPlaying.Models;
using System;

namespace NowPlaying.PlayerControllers
{
    public interface IPlayerController : IDisposable
    {
        /// <summary>
        /// Fired when the currently playing <see cref="Track"/> changes.
        /// </summary>
        event Action<Track> TrackChanged;

        /// <summary>
        /// Returns the currently playing <see cref="Track"/> if any, otherwise null.
        /// </summary>
        /// <returns></returns>
        Track GetTrack();

        /// <summary>
        /// Plays the next track in this <see cref="IPlayerController"/>, or ends playback if the playlist
        /// is empty.
        /// </summary>
        void NextTrack();

        /// <summary>
        /// Plays the previous track in this <see cref="IPlayerController"/>, or restarts the current track
        /// if it is the first item in the playlist.
        /// </summary>
        void PreviousTrack();

        /// <summary>
        /// Pauses playback.
        /// </summary>
        void Pause();

        /// <summary>
        /// Starts/resumes playback.
        /// </summary>
        void Play();
    }
}
