// Helpers/ThemeManager.cs
using Microsoft.Win32;
using System;
using System.Windows;

namespace RTSPDeck.Helpers
{
    public static class ThemeManager
    {
        public static void ApplySystemTheme()
        {
            var useLightTheme = true;

            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                if (key?.GetValue("AppsUseLightTheme") is int value)
                {
                    useLightTheme = value != 0;
                }
            }
            catch { }

            var dict = new ResourceDictionary
            {
                Source = new Uri(useLightTheme
                    ? "Themes/Light.xaml"
                    : "Themes/Dark.xaml", UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }
    }
}
