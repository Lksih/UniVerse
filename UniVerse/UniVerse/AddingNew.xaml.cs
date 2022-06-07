using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UniVerse
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddingNew : ContentPage
    {
        public AddingNew()
        {
            InitializeComponent();
        }

        private async void AddVerse(object sender, EventArgs e)
        {
            if (name.Text != null && author.Text != null && text.Text != null && name.Text != "" && author.Text != "" && text.Text != "")
            {
                string no_spaces;
                try
                {
                    no_spaces = tags.Text.Replace("  ", " ");
                    no_spaces = no_spaces.Replace(", ", ",");
                }
                catch (Exception)
                {
                    no_spaces = "";
                }
                string[] tagsList = no_spaces.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                App.Database.AddVerse(name.Text[name.Text.Length - 1] != '\n' ? name.Text : name.Text.Substring(0, name.Text.Length - 1), text.Text, author.Text, tagsList);
                DependencyService.Get<IMessages>().VerseAddedMessage();
                await Navigation.PopAsync();
            }
            else
            {
                DependencyService.Get<IMessages>().NullVerseFieldsError(name.Text, author.Text, text.Text);
            }
        }
    }
}