using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniVerse.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UniVerse
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VersesListPage : ContentPage
    {
        internal VersesListPage(Author author)
        {
            InitializeComponent();
            Title = author.Name;
            foreach(var verse in App.Database.GetVersesOfAuthor(author))
            {
                VerseLayout.Children.Add(new VerseCard(verse, this).CardFrame);
            }
        }

        internal VersesListPage(Tag tag)
        {
            InitializeComponent();
            Title = tag.Name;
            foreach (var verse in App.Database.GetVersesOfTag(tag))
            {
                VerseLayout.Children.Add(new VerseCard(verse, this).CardFrame);
            }
        }
    }
}