using System;
using System.Windows;
using NowPlaying.PlayerControllers.Itunes;
using NowPlaying.Models;
using System.Windows.Interop;
using NowPlaying.Exceptions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;

namespace NowPlaying
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int poll_delay = 1000;

        public String Identifier;

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

                updateWindowWidthIfNeeded();
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            SizeToContent = SizeToContent.Width;

            Loaded += (_, __) => updateTaskbarPosition();
        }

        private void updateTaskbarPosition()
        {
            RECT taskbarRect = getTaskbarSize();

            // FIXME: BAD BAD BAD, find out how to determine size of system tray
            Left = taskbarRect.Right - 1725 - ActualWidth;
            Top = taskbarRect.Top + 2;
        }

        private RECT getTaskbarSize()
        {
            IntPtr usHandle = new WindowInteropHelper(Window.GetWindow(this)).Handle;
            IntPtr taskBarHandle = FindWindow("Shell_traywnd", "");

            RECT taskbarRect;

            GetWindowRect(taskBarHandle, out taskbarRect);

            return taskbarRect;
        }

        private void updateWindowWidthIfNeeded()
        {
            var taskbarRect = getTaskbarSize();

            int x = Math.Abs(taskbarRect.Left - taskbarRect.Right);
            int y = Math.Abs(taskbarRect.Top - taskbarRect.Bottom);

            TrackLabel.Measure(new Size(x, y));

            if (Width == TrackLabel.Width)
                return;

            Width = TrackLabel.Width;

            updateTaskbarPosition();
        }

        protected override void OnInitialized(EventArgs args)
        {
            base.OnInitialized(args);

            try
            {
                itunesController = CreatePlatformController();
                // TODO: bad apple ​(╯°□°）╯︵ ┻━┻
                //itunesController.TrackChanged += track => Track = track;
                Track = itunesController.GetTrack();

                // FIXME: temporary hax while apple sorts out their COM events. Remove once TrackChanged can actually be used again
                Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            Track = itunesController.GetTrack();
                        });

                        Thread.Sleep(poll_delay);
                    }
                });
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

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }
    }
}