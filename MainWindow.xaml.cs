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
            manageWindow.ShowDialog();
            _cameraManager = new CameraManager();
            LoadCameraFeeds();
        }

        private void RefreshFeeds_Click(object sender, RoutedEventArgs e)
        {
            _cameraManager = new CameraManager();
            LoadCameraFeeds();
        }

        public MainWindow()
        {
            InitializeComponent();

            Core.Initialize();
            _libVLC = new LibVLC();
            _cameraManager = new CameraManager();

            LoadCameraFeeds();

            if (AutoStartCheckBox != null)
            {
                AutoStartCheckBox.IsChecked = AutoStartService.IsAutoStartEnabled();
                AutoStartCheckBox.Checked += (_, __) => AutoStartService.EnableAutoStart();
                AutoStartCheckBox.Unchecked += (_, __) => AutoStartService.DisableAutoStart();
            }
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

                string rtspUrl = $"rtsp://{feed.Username}:{feed.Password}@{feed.IPAddress}:{feed.Port}/Streaming/Channels/{feed.CameraNumber}01";
                var media = new Media(_libVLC, rtspUrl, FromType.FromLocation);

                var videoView = new LibVLCSharp.WPF.VideoView
                {
                    MediaPlayer = mediaPlayer,
                    Margin = new Thickness(5)
                };

                videoView.Loaded += (_, __) => mediaPlayer.Play(media);

                videoView.Unloaded += (_, __) =>
                {
                    mediaPlayer.Stop();
                    mediaPlayer.Dispose();
                    media.Dispose();
                };

                VideoGrid.Children.Add(videoView);
            }
        }
    }
}
