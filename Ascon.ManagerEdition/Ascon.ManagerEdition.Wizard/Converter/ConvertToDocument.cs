using Ascon.ManagerEdition.Wizard.Models;
using Ascon.ManagerEdition.Wizard.ViewModel.Template;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Ascon.ManagerEdition.Wizard.Converter
{
    public class ConvertToDocument : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<DocumentModel> result = new ObservableCollection<DocumentModel>();
            var docs = value as Documents;

            if (docs.DocumentList.Any())
            {
                foreach (var doc in docs.DocumentList)
                {
                    result.Add(new DocumentModel(doc, result));
                }
            }        
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
