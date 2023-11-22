using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniVerse.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UniVerse.Learn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerseLearningPage : ContentPage
    {
        private string[] Strings;
        private double Probability;
        private Verse verse;
        private int CurrentString = 0;
        private bool OnFinish = false;

        public VerseLearningPage(Verse verse)
        {
            InitializeComponent();
            this.verse = verse;
            closeButton.Text = "Закрыть 1 строку";
            Strings = TextSplitter.SplitToStrings(verse.Text);
            verseLabel.Text = String.Join("\n", Strings);
            switch(Preferences.Get("Complicity", "Просто"))
            {
                case "Просто":
                    Probability = 0.2;
                    break;
                case "Средне":
                    Probability = 0.4;
                    break;
                case "Сложно":
                    Probability = 0.6;
                    break;
            }
        }

        private string CloseString(string[] words)
        {
            Random random = new Random();
            int[] words_to_change = Enumerable.Range(0, words.Length).OrderBy(t => random.Next()).Take((int)(words.Length * Probability + 0.5) > 0 ? (int)(words.Length * Probability + 0.5) : 1).ToArray();
            foreach (int i in words_to_change)
            {
                words[i] = new string('*', words[i].Length);
            }
            return String.Join(" ", words);
        }

        private void closeButtonClicked(object sender, EventArgs e)
        {
            while (Strings[CurrentString].Length == 0)
            {
                CurrentString++;
            }
            Strings[CurrentString] = CloseString(TextSplitter.SplitStringToWords(Strings[CurrentString]));
            CurrentString++;
            if (CurrentString == Strings.Length)
            {
                closeButton.IsEnabled = false;
                OnFinish = true;
                closeSwitch.IsToggled = true;
                verseLabel.Text = String.Join("\n", Strings);
                return;
            }
            closeButton.Text = "Закрыть " + (CurrentString + 1) + " строку";
			verseLabel.Text = String.Join("\n", Strings);
		}

        private void closeSwitchToggled(object sender, EventArgs e)
        {
            Switch closeSwitch = (Switch)sender;
            if (closeSwitch.IsToggled)
            {
                if (!OnFinish)
                {
                    closeButton.IsEnabled = false;
                    Random random = new Random();
                    for (int i = CurrentString; i < Strings.Length; i++)
                    {
                        Strings[i] = CloseString(TextSplitter.SplitStringToWords(Strings[i]));
                    }
                    verseLabel.Text = String.Join("\n", Strings);
                }
            }
            else
            {
                OnFinish = false;
                CurrentString = 0;
                closeButton.Text = "Закрыть 1 строку";
                closeButton.IsEnabled = true;
                Strings = TextSplitter.SplitToStrings(verse.Text);
                verseLabel.Text = String.Join("\n", Strings);
            }
        }
    }
}