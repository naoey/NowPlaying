using NowPlaying.Models;
using System;

namespace NowPlaying.PlayerControllers
{
    public interface IPlayerController : IDisposable
    {
        event Action<Track> TrackChanged;

        Track GetTrack();

        void NextTrack();

        void PreviousTrack();

        void Pause();

        void Play();
    }
}
