using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(UniVerse.Droid.Messages))]
namespace UniVerse.Droid
{
    public class Messages : IMessages
    {
        public void VerseAddedMessage()
        {
            Toast.MakeText(Application.Context, "Стих добавлен", ToastLength.Short).Show();
        }

        public void NullVerseFieldsError(string name, string author, string text)
        {
            string res = "Необходимо заполнить поля:";
            if (name == null || name == "") res += " название,";
            if (author == null || author == "") res += " автор,";
            if (text == null || text == "") res += " текст,";
            res = res.Substring(0, res.Length - 1);
            Toast.MakeText(Application.Context, res, ToastLength.Long).Show();
        }

        public void TestDoneMessage()
        {
            Toast.MakeText(Application.Context, "Тест пройден!", ToastLength.Short).Show();
        }
    }
}