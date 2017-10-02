using Ascon.ManagerEdition.Common.MVVMCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Ascon.ManagerEdition.Common.MVVMCommon.Converters
{
    public class EnumToItemSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            foreach (var one in Enum.GetValues(parameter as Type))
            {
                if (value.Equals(one))
                    return ((Enum)one).GetDescription();
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            foreach (var one in Enum.GetValues(parameter as Type))
            {
                if (value.ToString() == ((Enum)one).GetDescription())
                {
                    return one;
                }
            }
            return null;
        }

    }
}
