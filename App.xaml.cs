using System;
using System.Windows;
using Microsoft.Win32;

namespace RTSPDeck
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            bool isDarkTheme = IsDarkThemeEnabled();
            var themeDict = new ResourceDictionary
            {
                Source = new Uri($"Themes/{(isDarkTheme ? "Dark" : "Light")}.xaml", UriKind.Relative)
            };
            Resources.MergedDictionaries.Add(themeDict);
        }

        private bool IsDarkThemeEnabled()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                if (key != null)
                {
                    var value = key.GetValue("AppsUseLightTheme");
                    return value is int intValue && intValue == 0;
                }
            }
            catch { }
            return false;
        }
    }
}
