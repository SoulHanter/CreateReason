using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Common.MVVMCommon.Messenger
{
    public class Messenger : IMessenger
    {
        private IList<ColleguageItem> observers;

        public Messenger()
        {
            observers = new List<ColleguageItem>();
        }

        public bool Exists(string type, string instanceName = null)
        {
            if (observers.Any(o => o.InstanceName == instanceName && o.Type == type))
                return true;
            return false;
        }

        public void RegisterObserver(string type, ColleguageBase colleguage, string instanceName = null)
        {
            if (type != null)
            {
                if (observers.Any(o => o.InstanceName == instanceName && o.Type == type))
                    UnRegisterObserver(type,instanceName);

                observers.Add(new ColleguageItem { Type = type.ToString(), Colleguage = colleguage, InstanceName = instanceName });
            }
        }

        public void SendMessage(string type, object message, string instanceName = null)
        {
            if (type != null && observers.Count != 0)
            {
                if (!observers.Any(o => o.InstanceName == instanceName && o.Type == type))
                    //throw new ArgumentException("Element not exists");
                    return;

                observers.First(o => o.InstanceName == instanceName && o.Type == type)?
                        .Colleguage
                        .Notify(message);
            }
        }

        public void UnRegisterObserver(string type, string instanceName = null)
        {
            if (type != null)
            {
                if (!observers.Any(o => o.InstanceName == instanceName && o.Type == type))
                    throw new ArgumentException("Element not exists");

                observers.Remove(observers.First(o => o.InstanceName == instanceName && o.Type == type));
            }
        }
    }

    public class ColleguageItem
    {
        public string Type { get; set; }

        public string InstanceName { get; set; }

        public ColleguageBase Colleguage { get; set; }
    }
}
