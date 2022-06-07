using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace UniVerse
{
    internal abstract class Card
    {
        internal Frame CardFrame { get; set; }
        protected StackLayout CardLayout;
    }
}
