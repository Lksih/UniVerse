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
    public partial class Menu : TabbedPage
    {
        public Menu()
        {
            InitializeComponent();
            List<Author> authors = App.Database.GetAllAuthors();
            GridList authorsList = new GridList(authors.Count);
            int i = 0, j = 0;
            foreach (var author in authors)
            {
                authorsList.Add(new AuthorCard(author, this).CardFrame, i, j);
                i++;
                if (i == 2)
                {
                    j++;
                    i = 0;
                }
            }
            Authors.Content = authorsList.getGridListLayout();
            List<Tag> tags = App.Database.GetAllTags();
            GridList tagsList = new GridList(tags.Count);
            i = 0;
            j = 0;
            foreach (var tag in tags)
            {
                tagsList.Add(new TagCard(tag, this).CardFrame, i, j);
                i++;
                if (i == 2)
                {
                    j++;
                    i = 0;
                }
            }
            Tags.Content = tagsList.getGridListLayout();
        }
    }
}