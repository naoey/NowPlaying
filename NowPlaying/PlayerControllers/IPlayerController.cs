using NowPlaying.Models;
using System;

namespace NowPlaying.PlayerControllers
{
    public interface IPlayerController : IDisposable
    {
        Track GetTrack();

        void NextTrack();

        void PreviousTrack();

        void Pause();

        void Play();
    }
}
