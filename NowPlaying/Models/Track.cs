using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NowPlaying.Models
{
    public class Track
    {
        /// <summary>
        /// The title of the current track.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The artist of the current track.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// The total duration of the track in seconds.
        /// </summary>
        public double Duration { get; set; }

        public BitmapImage Artwork { get; set; }

        /// <summary>
        /// The current offset position of the track.
        /// </summary>
        public double CurrentOffset { get; set; }

        public string TimeInformation => $"{TimeSpan.FromSeconds(CurrentOffset).ToString(@"mm\:ss\")} / {TimeSpan.FromSeconds(Duration).ToString(@"mm\:ss\")}";
    }
}
