using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniVerse.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UniVerse.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        private string[] Strings;
        private string[][] Words;
        private Verse verse;
        private int CurrentString = 0;
        public TestPage(Verse verse)
        {
            InitializeComponent();
            this.verse = verse;
            Strings = TextSplitter.SplitToStrings(verse.Text);
            Words = TextSplitter.GetWordsArray(verse.Text);
            MakingButtons(CurrentString);
        }

        private void MakingButtons(int ind)
        {
            Random random = new Random();
            foreach (string word in Words[ind])
            {
                Button button = new Button
                {
                    Text = word
				};
                button.Clicked += buttonClicked;
                buttonsLayout.Children.Insert(random.Next(0, buttonsLayout.Children.Count), button);
            }
        }

        private void buttonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            verseLabel.Text += button.Text + " ";
            button.IsEnabled = false;
            CheckInput();
        }

        private void CheckInput()
        {
            int TabInd = verseLabel.Text.LastIndexOf('\n');
            string input = verseLabel.Text.Substring(TabInd + 1);
            if (input.Trim() == System.Text.RegularExpressions.Regex.Replace(System.Text.RegularExpressions.Regex.Replace(Strings[CurrentString], @"\W+", " "), @"\s+", " ").Trim())
            {
                verseLabel.Text += "\n";
                buttonsLayout.Children.Clear();
                CurrentString++;
                if (CurrentString == Strings.Length)
                {
                    DependencyService.Get<IMessages>().TestDoneMessage();
                    return;
                }
                while (Strings[CurrentString] == "")
                {
                    verseLabel.Text += "\n";
                    CurrentString++;
                }
                MakingButtons(CurrentString);
            }
            else if (input.Trim().Length == System.Text.RegularExpressions.Regex.Replace(System.Text.RegularExpressions.Regex.Replace(Strings[CurrentString], @"\W+", " "), @"\s+", " ").Trim().Length)
            {
                verseLabel.Text = verseLabel.Text.Substring(0, TabInd + 1);
                foreach (Button button in buttonsLayout.Children)
                {
                    button.IsEnabled = true;
                }
            }
        }
    }
}