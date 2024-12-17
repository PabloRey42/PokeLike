using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PokeLike.MVVW.View
{
    public class HPToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is double currentHP && values[1] is double maxHP)
            {
                double percentage = (currentHP / maxHP) * 100;

                if (percentage > 50) return Brushes.Green;
                if (percentage > 25) return Brushes.Yellow;
                return Brushes.Red;
            }
            return Brushes.Gray;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
