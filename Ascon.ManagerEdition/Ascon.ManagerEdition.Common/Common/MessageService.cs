using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace Ascon.ManagerEdition.Common.Common
{
    public class MessageService : IMessageService
    {
        private readonly string _caption = "Подтверждение";

        public bool Disable { get; set; }

        public bool NotifyUser(string message, bool withConfirm)
        {
            if (Disable)
                return true;

            MessageBoxResult result;

            if (withConfirm)
            {
                result = MessageBox.Show(message, _caption, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            }
            else
            {
                result = MessageBox.Show(message, _caption);
            }

            return result == MessageBoxResult.Yes;
        }
    }
}
