using System;
using System.Collections.Generic;
using System.Text;
using UniVerse.Models;
using Xamarin.Forms;

namespace UniVerse
{
    internal class AuthorCard : Card
    {
        Author CurrentAuthor;
        internal Button toList;
        Page ParentPage;

        internal AuthorCard(Author author, Page page)
        {
            CurrentAuthor = author;
            ParentPage = page;
            toList = new Button { Text = "Перейти", VerticalOptions = LayoutOptions.EndAndExpand };
            toList.Clicked += ToListClicked;
            Label AuthorLabel = new Label
            {
                Text = CurrentAuthor.Name,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            AuthorLabel.SetDynamicResource(Label.TextColorProperty, "textColor");
            CardLayout = new StackLayout
            {
                Children = {
                    AuthorLabel,
                    toList }
            };
            CardLayout.SetDynamicResource(StackLayout.BackgroundColorProperty, "theme");
            CardFrame = new Frame { BorderColor = Color.Black, Padding = 5 };
            CardFrame.Content = CardLayout;
        }

        private async void ToListClicked(object sender, EventArgs e)
        {
            await ParentPage.Navigation.PushAsync(new VersesListPage(CurrentAuthor));
        }
    }
}