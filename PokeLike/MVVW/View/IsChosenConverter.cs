using System;
using System.Globalization;
using System.Windows.Data;
using PokeLike.Model;
using System.Collections.Generic;

namespace PokeLike.MVVW.View
{
    public class IsChosenConverter : IValueConverter
    {
        public static Dictionary<int, bool> ChosenMonsters = new Dictionary<int, bool>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var monster = value as Monster;
            if (monster == null) return false;

            return ChosenMonsters.ContainsKey(monster.Id) && ChosenMonsters[monster.Id];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
