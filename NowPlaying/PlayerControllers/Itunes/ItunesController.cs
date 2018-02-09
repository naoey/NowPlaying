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

        public Track GetTrack() => CurrentTrack;

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
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ItunesController() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
