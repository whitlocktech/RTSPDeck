using System.Collections.Generic;
using RTSPDeck.Models;

namespace RTSPDeck.Services
{
    public class CameraManager
    {
        public List<CameraFeed> Feeds { get; private set; }

        public CameraManager()
        {
            Feeds = ConfigService.LoadFeeds(); // ensure ConfigService has this method
        }

        public void AddFeed(CameraFeed feed)
        {
            Feeds.Add(feed);
            ConfigService.SaveFeeds(Feeds);
        }

        public void RemoveFeed(CameraFeed feed)
        {
            Feeds.Remove(feed);
            ConfigService.SaveFeeds(Feeds);
        }

        public void UpdateFeed(CameraFeed oldFeed, CameraFeed newFeed)
        {
            var index = Feeds.IndexOf(oldFeed);
            if (index >= 0)
            {
                Feeds[index] = newFeed;
                ConfigService.SaveFeeds(Feeds);
            }
        }
    }
}
