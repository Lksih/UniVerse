using System;
using System.Collections.Generic;
using System.Text;

namespace UniVerse
{
    internal static class TextSplitter
    {
        internal static string[] SplitToStrings(string text)
        {
            return text.Replace("\r", "").Split(new char[] { '\n' });
        }

        internal static string[] SplitStringToWords(string text)
        {
            return text.Split(new char[] { ' ' });
        }

        internal static string[][] GetWordsArray(string text)
        {
            string[] strs = SplitToStrings(text);
            string[][] words = new string[strs.Length][];
            for(int i = 0; i < strs.Length; i++)
            {
                words[i] = SplitStringToWords(strs[i]);
            }
            return words;
        }
    }
}
