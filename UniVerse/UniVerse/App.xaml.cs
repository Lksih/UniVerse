using System;
using System.IO;
using System.Reflection;
using UniVerse.Themes;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace UniVerse
{
    public partial class App : Application
    {
        private static DataBaseAccess database;
        public static DataBaseAccess Database
        {
            get
            {
                if (database == null)
                {
                    database = new DataBaseAccess();
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            ThemeChanger.ChangeTheme(Preferences.Get("DarkMode", false) ? ThemeChanger.Theme.Dark : ThemeChanger.Theme.Light);
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
