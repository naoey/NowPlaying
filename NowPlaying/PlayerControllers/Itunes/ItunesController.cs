using System;
using NowPlaying.Models;

namespace NowPlaying.PlayerControllers.Itunes
{
    public abstract class ItunesController : IPlayerController
    {
        private Track currentTrack;
        protected Track CurrentTrack
        {
            get => currentTrack;
            set
            {
                if (value == currentTrack)
                    return;

                currentTrack = value;

                TrackChanged?.Invoke(value);
            }
        }

        public ItunesController()
        {
        }

        /// <summary>
        /// Retreives the currently playing <see cref="Track"/> from iTunes.
        /// </summary>
        public abstract void UpdateTrack();

        public Track GetTrack()
        {
            UpdateTrack();

            return CurrentTrack;
        }

        public abstract void NextTrack();

        public abstract void Pause();

        public abstract void Play();

        public abstract void PreviousTrack();

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        public event Action<Track> TrackChanged;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }
        
        ~ItunesController()
        {
            Dispose(false);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
