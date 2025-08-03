using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using RTSPDeck.Models;

namespace RTSPDeck.Services
{
    public static class ConfigService
    {
        private static readonly string ConfigPath = "config.json";

        public static List<CameraFeed> LoadFeeds()
        {
            if (!File.Exists(ConfigPath))
            {
                File.WriteAllText(ConfigPath, "[]");
                return new List<CameraFeed>();
            }

            var json = File.ReadAllText(ConfigPath);
            return JsonSerializer.Deserialize<List<CameraFeed>>(json) ?? new List<CameraFeed>();
        }

        public static void SaveFeeds(List<CameraFeed> feeds)
        {
            var json = JsonSerializer.Serialize(feeds, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigPath, json);
        }

        // Aliases for compatibility with other parts of the app
        public static List<CameraFeed> LoadCameraFeeds() => LoadFeeds();
        public static void SaveCameraFeeds(List<CameraFeed> feeds) => SaveFeeds(feeds);
    }
}
