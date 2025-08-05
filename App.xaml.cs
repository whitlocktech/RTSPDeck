using System;
using System.Windows;
using Microsoft.Win32;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;

namespace RTSPDeck
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            bool isDark = IsSystemInDarkMode();

            var theme = Theme.Create(
                isDark ? BaseTheme.Dark : BaseTheme.Light,
                primary: SwatchHelper.Lookup[MaterialDesignColor.DeepPurple],
                secondary: SwatchHelper.Lookup[MaterialDesignColor.Lime]
            );

            new PaletteHelper().SetTheme(theme);
        }

        private bool IsSystemInDarkMode()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                return key?.GetValue("AppsUseLightTheme") is int value && value == 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
