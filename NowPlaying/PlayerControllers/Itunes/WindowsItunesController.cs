using iTunesLib;
using NowPlaying.Exceptions;
using NowPlaying.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;

namespace NowPlaying.PlayerControllers.Itunes
{
    class WindowsItunesController : ItunesController
    {
        private const string app_name = @"iTunes";

        private iTunesApp itunes;

        public WindowsItunesController()
        {
            if (Process.GetProcessesByName(app_name).Length == 0)
                throw new ItunesNotRunningException();

            itunes = new iTunesApp();
        }

        public override Track GetTrack()
        {
            var currentTrack = itunes.CurrentTrack;

            if (currentTrack == null)
                return null;

            string tmpFile = Path.GetTempFileName();
            currentTrack.Artwork[1].SaveArtworkToFile(tmpFile);

            BitmapImage artwork = new BitmapImage();

            artwork.BeginInit();
            artwork.UriSource = new Uri(tmpFile);
            artwork.EndInit();

            return new Track
            {
                Title = currentTrack.Name,
                Artist = currentTrack.Artist,
                Artwork = artwork,
                Duration = currentTrack.Duration,
                CurrentOffset = 0,
            };
        }

        public override void NextTrack() => itunes.NextTrack();

        public override void Pause() => itunes.Pause();

        public override void Play() => itunes.Play();

        public override void PreviousTrack() => itunes.PreviousTrack();
    }
}
