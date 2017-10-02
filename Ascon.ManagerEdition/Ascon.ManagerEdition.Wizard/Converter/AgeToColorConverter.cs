using Ascon.ManagerEdition.Wizard.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace Ascon.ManagerEdition.Wizard.Converter
{
    public class AgeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case ColorsRow.GREEN:
                    return new SolidColorBrush(Colors.LightGreen);
                case ColorsRow.RED:
                    return new SolidColorBrush(Colors.Salmon);
                case ColorsRow.YELLOW:
                    return new SolidColorBrush(Colors.Khaki);
                default:
                    return new SolidColorBrush(Colors.White);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
