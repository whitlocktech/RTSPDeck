using System;
using System.Windows;
using LibVLCSharp.Shared;
using RTSPDeck.Models;
using RTSPDeck.Services;
using System.Windows.Controls;

namespace RTSPDeck
{
    public partial class MainWindow : Window
    {
        private LibVLC _libVLC;
        private CameraManager _cameraManager;

        private void OpenManageCameras_Click(object sender, RoutedEventArgs e)
        {
            var manageWindow = new ManageCamerasWindow();
            manageWindow.ShowDialog(); // Wait for it to close
            _cameraManager = new CameraManager(); // Reload the feeds
            LoadCameraFeeds();
        }

        private void RefreshFeeds_Click(object sender, RoutedEventArgs e)
        {
            _cameraManager = new CameraManager(); // Reload data
            LoadCameraFeeds();
        }

        public MainWindow()
        {
            InitializeComponent();

            Core.Initialize(); // Initialize VLC
            _libVLC = new LibVLC();
            _cameraManager = new CameraManager();

            LoadCameraFeeds();
        }

        private void LoadCameraFeeds()
        {
            VideoGrid.Children.Clear();

            var feeds = _cameraManager.Feeds;
            int count = feeds.Count;

            if (count == 0)
            {
                MessageBox.Show("No camera feeds configured. Use Manage Cameras to add them.", "RTSPDeck", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            int columns = (int)Math.Ceiling(Math.Sqrt(count));
            int rows = (int)Math.Ceiling((double)count / columns);

            VideoGrid.Columns = columns;
            VideoGrid.Rows = rows;

            foreach (var feed in feeds)
            {
                var mediaPlayer = new MediaPlayer(_libVLC);

                mediaPlayer.EncounteredError += (s, e) =>
                {
                    MessageBox.Show($"Playback error for feed: {feed.Name}");
                };

                mediaPlayer.Playing += (s, e) =>
                {
                    Console.WriteLine($"Feed '{feed.Name}' is playing.");
                };

                string rtspUrl = $"rtsp://{feed.Username}:{feed.Password}@{feed.IPAddress}:{feed.Port}/Streaming/Channels/{feed.CameraNumber}01";

                var media = new Media(_libVLC, rtspUrl, FromType.FromLocation);
                mediaPlayer.Play(media);

                var videoView = new LibVLCSharp.WPF.VideoView
                {
                    MediaPlayer = mediaPlayer,
                    Margin = new Thickness(5)
                };

                VideoGrid.Children.Add(videoView);
            }
        }
    }
}
