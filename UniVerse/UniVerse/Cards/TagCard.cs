using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using UniVerse.Models;

namespace UniVerse
{
    internal class TagCard : Card
    {
        Tag CurrentTag;
        internal Button toList;
        Page ParentPage;

        internal TagCard(Tag tag, Page page)
        {
            CurrentTag = tag;
            ParentPage = page;
            toList = new Button { Text = "Перейти", VerticalOptions = LayoutOptions.EndAndExpand };
            toList.Clicked += ToListClicked;
            Label TagLabel = new Label
            {
                Text = CurrentTag.Name,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            TagLabel.SetDynamicResource(Label.TextColorProperty, "textColor");
            CardLayout = new StackLayout
            {
                Children = {
                    TagLabel,
                    toList }
            };
            CardLayout.SetDynamicResource(StackLayout.BackgroundColorProperty, "theme");
            CardFrame = new Frame { BorderColor = Color.Black, Padding = 5 };
            CardFrame.Content = CardLayout;
        }

        private async void ToListClicked(object sender, EventArgs e)
        {
            await ParentPage.Navigation.PushAsync(new VersesListPage(CurrentTag));
        }
    }
}
