using RTSPDeck.Models;
using RTSPDeck.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace RTSPDeck
{
    public partial class ManageCamerasWindow : Window
    {
        public ObservableCollection<CameraFeed> CameraFeeds { get; set; }

        public ManageCamerasWindow()
        {
            InitializeComponent();
            CameraFeeds = new ObservableCollection<CameraFeed>(ConfigService.LoadCameraFeeds());
            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Ensure edits are committed before saving
            if (CameraList.CommitEdit(System.Windows.Controls.DataGridEditingUnit.Row, true))
            {
                ConfigService.SaveCameraFeeds(CameraFeeds.ToList());
                DialogResult = true;
                Close();
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is CameraFeed feed)
            {
                if (MessageBox.Show($"Are you sure you want to delete camera '{feed.Name}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    CameraFeeds.Remove(feed);
                }
            }
        }

    }
}
