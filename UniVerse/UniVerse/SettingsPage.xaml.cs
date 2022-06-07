using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UniVerse.Themes;

namespace UniVerse
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            Developers.Text = "Создатели:\nВоробьев Илья\nГомаско Денис\nКотов Иван";
            switch (Preferences.Get("Complicity", "Просто"))
            {
                case "Просто":
                    ComplicityPicker.SelectedIndex = 0;
                    break;
                case "Средне":
                    ComplicityPicker.SelectedIndex = 1;
                    break;
                case "Сложно":
                    ComplicityPicker.SelectedIndex = 2;
                    break;
            }
            Switch DarkModeSwitch = new Switch
            {
                IsToggled = Preferences.Get("DarkMode", false),
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            DarkModeSwitch.Toggled += DarkClicked;
            DarkModeStack.Children.Add(DarkModeSwitch);
        }

        private void DarkClicked(object sender, EventArgs e)
        {
            bool darkModeEnabled = Preferences.Get("DarkMode", false);
            if (darkModeEnabled)
            {
                ThemeChanger.ChangeTheme(ThemeChanger.Theme.Light);
            }
            else
            {
                ThemeChanger.ChangeTheme(ThemeChanger.Theme.Dark);
            }
            Preferences.Set("DarkMode", !darkModeEnabled);
        }

        private void ComplicitySelected(object sender, EventArgs e)
        {
            Preferences.Set("Complicity", ComplicityPicker.Items[ComplicityPicker.SelectedIndex]);
        }
    }
}