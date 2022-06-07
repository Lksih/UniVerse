using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace UniVerse.Themes
{
    public static class ThemeChanger
    {
        public enum Theme
        {
            Light,
            Dark
        }

        public static void ChangeTheme(Theme theme)
        {
            ICollection<ResourceDictionary> mergeddictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergeddictionaries != null)
            {
                mergeddictionaries.Clear();

                switch (theme)
                {
                    case Theme.Dark:
                        mergeddictionaries.Add(new DarkTheme());
                        break;
                    case Theme.Light:
                    default:
                        mergeddictionaries.Add(new LightTheme());
                        break;
                }
            }
        }
    }
}
