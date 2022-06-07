using System;
using System.Collections.Generic;
using System.Text;
using UniVerse.Models;
using Xamarin.Forms;
using System.Diagnostics;
using UniVerse.Test;
using UniVerse.Learn;
using System.Reflection;

namespace UniVerse
{
    internal class VerseCard : Card
    {
        string AuthorName;
        string Tags;
        Verse verse;
        Page ParentPage;

        internal VerseCard(Verse verse, Page page)
        {
            this.verse = verse;
            ParentPage = page;
            AuthorName = App.Database.GetAuthor(verse.AuthorId).Name;
            Tags = "";
            foreach (var verse_tag in App.Database.GetTagsForVerse(verse.ID))
            {
                Tags += App.Database.GetTag(verse_tag.TagId).Name + ", ";
            }
            if (Tags.Length > 2)
            {
                Tags = Tags.Substring(0, Tags.Length - 2);
            }
            CardLayout = new StackLayout { Padding = 5 };
            CardLayout.SetDynamicResource(StackLayout.BackgroundColorProperty, "theme");
            StackLayout header = new StackLayout { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.Fill };
            ImageButton like = new ImageButton
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Source = verse.Favourited == 1 ?
                "LikePressed.png" :
                "LikeNotPressed.png"
            };
            like.SetDynamicResource(StackLayout.BackgroundColorProperty, "theme");
            like.Clicked += likePressed;
            Label NameLabel = new Label { Text = "Название: " + verse.Name, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
            NameLabel.SetDynamicResource(Label.TextColorProperty, "textColor");
            Label AuthorLabel = new Label { Text = "Автор: " + AuthorName, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
            AuthorLabel.SetDynamicResource(Label.TextColorProperty, "textColor");
            Label TagsLabel = new Label { Text = "Тэги: " + Tags, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
            TagsLabel.SetDynamicResource(Label.TextColorProperty, "textColor");
            header.Children.Add(new StackLayout
            {
                Children = {
                    NameLabel,
                    AuthorLabel,
                    TagsLabel }
            });
            header.Children.Add(like);
            Button learnButton = new Button { Text = "Учить" };
            learnButton.Clicked += learnPressed;
            Button testButton = new Button { Text = "Тест" };
            testButton.Clicked += testPressed;
            CardLayout.Children.Add(header);
            CardLayout.Children.Add(learnButton);
            CardLayout.Children.Add(testButton);
            CardFrame = new Frame { BorderColor = Color.Black, Padding = 5 };
            CardFrame.Content = CardLayout;
        }

        public async void learnPressed(object sender, EventArgs e)
        {
            await ParentPage.Navigation.PushAsync(new VerseLearningPage(verse));
        }

        public async void testPressed(object sender, EventArgs e)
        {
            await ParentPage.Navigation.PushAsync(new TestPage(verse));
        }

        private void likePressed(object sender, EventArgs e)
        {
            if (verse.Favourited == 0)
            {
                verse.Favourited = 1;
            }
            else
            {
                verse.Favourited = 0;
            }
            ImageButton like = (ImageButton)sender;
            like.Source = verse.Favourited == 1 ?
                "LikePressed.png" :
                "LikeNotPressed.png";
            App.Database.UpdateLike(verse);
        }
    }
}
