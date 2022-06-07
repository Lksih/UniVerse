using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniVerse.Droid
{
    public static class DataCopying
    {
        public static void CopyDatabaseAsync()
        {
            const string DATABASE_NAME = "universe.db";
            var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), DATABASE_NAME);
            
            if (!File.Exists(dbPath))
            {
                using (var dbAssetStream = Application.Context.Assets.Open(DATABASE_NAME))
                using (var dbFileStream = new FileStream(dbPath, FileMode.OpenOrCreate))
                {
                    var buffer = new byte[1024];

                    int b = buffer.Length;
                    int length;

                    while ((length = dbAssetStream.Read(buffer, 0, b)) > 0)
                    {
                        dbFileStream.Write(buffer, 0, length);
                    }

                    dbFileStream.Flush();
                    dbFileStream.Close();
                    dbAssetStream.Close();
                }
            }
        }
    }
}