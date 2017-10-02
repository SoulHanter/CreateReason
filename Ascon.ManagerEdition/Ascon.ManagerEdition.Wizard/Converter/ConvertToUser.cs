using Ascon.ManagerEdition.Common.DICommon;
using Ascon.ManagerEdition.Wizard.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Ascon.ManagerEdition.Wizard.Converter
{
    public class ConvertToUser : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var _settings = NinjectCommon.Kernel.Get<Settings.Settings>();
            var users = new List<Users>();

            switch (parameter)
            {
                case "0": users = _settings.ToUser;
                    break;
                default: users = _settings.FromUser;
                    break;
            }
            

            return users.FirstOrDefault(x => value.Equals(x.Name))?.FullName ?? null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var _settings = NinjectCommon.Kernel.Get<Settings.Settings>();
            var users = new List<Users>();

            switch (parameter)
            {
                case "0":
                    users = _settings.ToUser.Distinct().ToList();
                    break;
                default:
                    users = _settings.FromUser.Distinct().ToList();
                    break;
            }

            return users.FirstOrDefault(x => value.Equals(x.Name))?.FullName ?? null;
        }
    }
}
