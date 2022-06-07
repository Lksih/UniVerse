using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace UniVerse
{
    internal class GridList
    {
        Grid GList = new Grid();
        int RowCount;
        int ColumnCount = 2;

        internal GridList(int count)
        {
            RowCount = count % 2 == 0 ? count / 2 : (count + 1) / 2;
            for (int i = 0; i < RowCount; i++)
            {
                GList.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            }
            for (int i = 0; i < ColumnCount; i++)
            {
                GList.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
        }

        internal void Add(View view, int left, int top)
        {
            GList.Children.Add(view, left, top);
        }

        internal Grid getGridListLayout()
        {
            return GList;
        }
    }
}
