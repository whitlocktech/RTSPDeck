using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using RTSPDeck.Models;

namespace RTSPDeck.Services
{
    public static class ConfigService
    {
        private static readonly string AppName = "RTSPDeck";
        private static readonly string CompanyName = "Whitlocktech";

        private static readonly string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string LocalAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        // Dynamically determine the best config directory at runtime
        private static string GetConfigDirectory()
        {
            string appDataDir = !string.IsNullOrEmpty(AppDataPath) ? Path.Combine(AppDataPath, CompanyName, AppName) : null;
            if (IsDirectoryWritable(appDataDir))
                return appDataDir;

            string localAppDataDir = !string.IsNullOrEmpty(LocalAppDataPath) ? Path.Combine(LocalAppDataPath, CompanyName, AppName) : null;
            if (IsDirectoryWritable(localAppDataDir))
                return localAppDataDir;

            // Fallback: use current directory
            return Directory.GetCurrentDirectory();
        }

        private static string ConfigDirectory => GetConfigDirectory();

        private static string ConfigPath => Path.Combine(ConfigDirectory, "config.json");
        public static List<CameraFeed> LoadFeeds()
        {
            try
            {
                EnsureDirectoryExists();

                if (!File.Exists(ConfigPath))
                {
                    File.WriteAllText(ConfigPath, "[]");
                    return new List<CameraFeed>();
                }

                var json = File.ReadAllText(ConfigPath);
                return JsonSerializer.Deserialize<List<CameraFeed>>(json) ?? new List<CameraFeed>();
            }
            catch (Exception ex)
            {
                // Optional: log the error or alert the user
                Console.WriteLine($"Error loading config: {ex.Message}");
                return new List<CameraFeed>();
            }
        }

        public static void SaveFeeds(List<CameraFeed> feeds)
        {
            try
            {
                EnsureDirectoryExists();

                var json = JsonSerializer.Serialize(feeds, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ConfigPath, json);
            }
            catch (Exception ex)
            {
                // Optional: log the error or alert the user
                Console.WriteLine($"Error saving config: {ex.Message}");
            }
        }

        private static void EnsureDirectoryExists()
        {
            if (!Directory.Exists(ConfigDirectory))
            {
                Directory.CreateDirectory(ConfigDirectory);
            }
        }

        // Aliases for compatibility
        public static List<CameraFeed> LoadCameraFeeds() => LoadFeeds();
        public static void SaveCameraFeeds(List<CameraFeed> feeds) => SaveFeeds(feeds);
    }
}
