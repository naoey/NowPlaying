using System;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using NowPlaying.PlayerControllers.Itunes;
using NowPlaying.Models;
using System.Windows.Interop;
using NowPlaying.Exceptions;

namespace NowPlaying
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public String Identifier;

        private IntPtr? taskbar;
        private ItunesController itunesController;

        private Track track;
        protected Track Track
        {
            get => track;
            set
            {
                track = value;

                if (value == null)
                {
                    TrackLabel.Content = @"Nothing playing!";
                    return;
                }

                TrackLabel.Content = $@"🎵 {value.Artist} - {value.Title}";
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            Process process = Process.GetProcessesByName(@"Shell_TrayWnd").FirstOrDefault();
            taskbar = process?.MainWindowHandle;


            if (!taskbar.HasValue)
                return;

            HwndSource hwnd = HwndSource.FromHwnd(taskbar.Value);

            var window = hwnd.RootVisual as Window;

            if (window == null)
                return;

            Top = window.Top;
            Left = window.Left - 50;
            Height = 50;
        }

        protected override void OnInitialized(EventArgs args)
        {
            base.OnInitialized(args);

            try
            {
                itunesController = CreatePlatformController();
                itunesController.TrackChanged += track => Track = track;
                Track = itunesController.GetTrack();
            }
            catch (ItunesNotRunningException e)
            {
                TrackLabel.Content = @"iTunes isn't running!";
            }
            catch (Exception e)
            {
                TrackLabel.Content = @"Uh oh!";
            }
        }

        // TODO: create platform-based controller.
        protected virtual ItunesController CreatePlatformController() => new WindowsItunesController();
    }
}