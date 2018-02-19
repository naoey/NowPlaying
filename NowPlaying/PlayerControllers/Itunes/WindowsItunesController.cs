using iTunesLib;
using NowPlaying.Exceptions;
using NowPlaying.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Media.Imaging;

namespace NowPlaying.PlayerControllers.Itunes
{
    class WindowsItunesController : ItunesController
    {
        private const string app_name = @"iTunes";

        private iTunesApp itunes;

        private volatile bool playerStopped = true;

        public WindowsItunesController()
        {
            if (Process.GetProcessesByName(app_name).Length == 0)
                throw new ItunesNotRunningException();

            itunes = new iTunesApp();

            // TODO: bad apple ​(╯°□°）╯︵ ┻━┻
            //itunes.OnPlayerStopEvent += _ =>
            //{
            //    playerStopped = true;

            //    new Thread(() =>
            //    {
            //        Thread.Sleep(500);

            //        if (playerStopped)
            //            // If player is still marked as stopped after waiting, then a new track play event wasn't fired,
            //            // which means all playback has currently stopped. Fill in the placeholder.
            //            CurrentTrack = null;
            //    });
            //};

            //itunes.OnPlayerPlayEvent += _ => UpdateTrack();

            //UpdateTrack();
        }

        public override void UpdateTrack()
        {
            var currentTrack = itunes.CurrentTrack;

            if (currentTrack == null)
                return;

            playerStopped = false;

            // TODO: handle artwork
            //string tmpFile = Path.GetTempFileName();
            //currentTrack.Artwork[1].SaveArtworkToFile(tmpFile);

            //BitmapImage artwork = new BitmapImage();

            //artwork.BeginInit();
            //artwork.UriSource = new Uri(tmpFile);
            //artwork.EndInit();

            CurrentTrack = new Track
            {
                Title = currentTrack.Name,
                Artist = currentTrack.Artist,
                //Artwork = artwork,
                Duration = currentTrack.Duration,
                CurrentOffset = 0,
            };
        }

        public override void NextTrack() => itunes.NextTrack();

        public override void Pause() => itunes.Pause();

        public override void Play() => itunes.Play();

        public override void PreviousTrack() => itunes.PreviousTrack();

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            itunes = null;
        }
    }
}
