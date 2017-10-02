using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Common.MVVMCommon.Messenger
{
    public interface IMessenger
    {
        void RegisterObserver(string type, ColleguageBase colleguage, string instanceName = null);

        void UnRegisterObserver(string type, string instanceName = null);

        void SendMessage(string type, object message, string instanceName = null);

        bool Exists(string type, string instanceName = null);
    }
}
