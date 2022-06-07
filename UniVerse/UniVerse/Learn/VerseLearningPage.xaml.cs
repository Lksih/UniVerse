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
        private string[][] Words;
        private string[] Strings;
        private string[] ClosedStrings;
        private int Probability;
        private Verse verse;
        private int CurrentString = 0;
        public VerseLearningPage(Verse verse)
        {
            InitializeComponent();
            this.verse = verse;
            verseLabel.Text = verse.Text;
            closeButton.Text = "Закрыть 1 строку";
            Words = TextSplitter.GetWordsArray(verse.Text);
            Strings = TextSplitter.SplitToStrings(verse.Text);
            ClosedStrings = new string[Strings.Length];
            switch(Preferences.Get("Complicity", "Просто"))
            {
                case "Просто":
                    Probability = 20;
                    break;
                case "Средне":
                    Probability = 40;
                    break;
                case "Сложно":
                    Probability = 60;
                    break;
            }
        }

        private string CloseString(string[] words)
        {
            string res = "";
            Random random = new Random();
            foreach(string word in words)
            {
                if (random.Next(1, 100) <= Probability)
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        res += '*';
                    }
                }
                else
                {
                    res += word;
                }
                res += ' ';
            }
            return res.Substring(0, res.Length - 1);
        }

        private void closeButtonClicked(object sender, EventArgs e)
        {
            if (Strings[CurrentString].Length == 0)
            {
                CurrentString++;
            }
            string closedText = "";
            for (int i = 0; i < CurrentString; i++)
            {
                closedText += ClosedStrings[i] + "\n";
            }
            ClosedStrings[CurrentString] = CloseString(Words[CurrentString]);
            CurrentString++;
            if (CurrentString == Strings.Length)
            {
                closedText += ClosedStrings[CurrentString - 1];
                closeButton.IsEnabled = false;
                verseLabel.Text = closedText;
                return;
            }
            else
            {
                closedText += ClosedStrings[CurrentString - 1] + "\n";
            }
            for (int i = CurrentString; i < Strings.Length - 1; i++)
            {
                closedText += Strings[i] + "\n";
            }
            closedText += Strings[Words.Length - 1];
            closeButton.Text = "Закрыть " + (CurrentString + 1) + " строку";
            verseLabel.Text = closedText;
        }

        private void closeSwitchToggled(object sender, EventArgs e)
        {
            Switch closeSwitch = (Switch)sender;
            if (closeSwitch.IsToggled)
            {
                closeButton.IsEnabled = false;
                string closedText = "";
                Random random = new Random();
                for (int i = 0; i < Words.Length - 1; i++)
                {
                    closedText += CloseString(Words[i]) + "\n";
                }
                closedText += CloseString(Words[Words.Length - 1]);
                verseLabel.Text = closedText;
            }
            else
            {
                CurrentString = 0;
                closeButton.Text = "Закрыть 1 строку";
                closeButton.IsEnabled = true;
                verseLabel.Text = verse.Text;
            }
        }

    }
}