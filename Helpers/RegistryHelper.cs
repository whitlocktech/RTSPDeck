using Microsoft.Win32;
using System;
using System.IO;

namespace RTSPDeck.Services
{
    public static class AutoStartService
    {
        private const string AppName = "RTSPDeck";
        private static readonly string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private static readonly string ExecutablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

        public static void EnableAutoStart()
        {
            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKey, writable: true);
            key?.SetValue(AppName, $"\"{ExecutablePath}\"");
        }

        public static void DisableAutoStart()
        {
            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKey, writable: true);
            key?.DeleteValue(AppName, throwOnMissingValue: false);
        }

        public static bool IsAutoStartEnabled()
        {
            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(RunKey, writable: false);
            var value = key?.GetValue(AppName) as string;
            return value == $"\"{ExecutablePath}\"";
        }
    }
}
