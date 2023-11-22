using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniVerse.Models;
using Xamarin.Forms;

namespace UniVerse
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            foreach(Verse verse in App.Database.GetLikedVerses())
            {
                favourites.Children.Add(new VerseCard(verse, this).CardFrame);
            }
        }

        private async void StartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu());
        }

        private async void AddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddingNew());
        }

        private async void SettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}