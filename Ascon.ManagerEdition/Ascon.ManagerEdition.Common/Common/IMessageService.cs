using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Ascon.ManagerEdition.Common.Common
{
    public interface IMessageService
    {
        bool NotifyUser(string message, bool withConfirm);

        bool Disable { get; set; }
    }
}
