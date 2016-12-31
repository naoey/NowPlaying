using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using iTunesLib;

namespace NowPlaying
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private iTunesApp app;
        private Timer timer;

        public String Identifier;

        public MainWindow()
        {
            InitializeComponent();
            Identifier = "Hue";

            Process[] plist = Process.GetProcessesByName("iTunes");

            if (plist.Length > 0)
            {
                app = new iTunesApp();
            }

            Load();
        }

        public void Load()
        {
            if (app != null)
            {
                string tmpFile = System.IO.Path.GetTempFileName();
                app.CurrentTrack?.Artwork[1].SaveArtworkToFile(tmpFile);
                BitmapImage artwork = new BitmapImage();
                artwork.BeginInit();
                artwork.UriSource = new Uri(tmpFile);
                artwork.EndInit();

                TrackLabel.Content = String.Format("{0} - {1}", app.CurrentTrack?.Artist, app.CurrentTrack?.Name);
                TrackArtwork.Source = artwork;
            }
        }
    }
}