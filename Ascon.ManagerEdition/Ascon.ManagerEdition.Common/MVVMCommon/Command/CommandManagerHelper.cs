using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ascon.ManagerEdition.Common.MVVMCommon.Command
{
    public class CommandManagerHelper
    {
        public static void ExecuteWeakReferenceHandlers(List<WeakReference> handlers)
        {
            if(handlers.Count == 0)
            {
                return;
            }

            //remove elements where handlers == null
            handlers = handlers.Where(x => (x.Target as EventHandler) != null).ToList();

            //take snapshot
            EventHandler[] executable = handlers.Where(x => (x.Target as EventHandler) != null)
                                                .Select(x => (x.Target as EventHandler))
                                                .ToArray();
            //Execute!
            for (int i = 0; i < executable.Count(); i++)
            {
                executable[i](null, EventArgs.Empty);
            }
        }

        public static void AddHandlersRequerySuggested(List<WeakReference> handlers)
        {
            if (handlers.Count == 0)
            {
                return;
            }

            foreach (var handler in handlers.Where(x => (x.Target as EventHandler) != null).Select(x => x.Target as EventHandler))
            {
                CommandManager.RequerySuggested += handler;
            }
        }

        public static void RemoveHandlersRequerySuggested(List<WeakReference> handlers)
        {
            if (handlers.Count == 0)
            {
                return;
            }

            foreach (var handler in handlers.Where(x => (x.Target as EventHandler) != null).Select(x => x.Target as EventHandler))
            {
                CommandManager.RequerySuggested -= handler;
            }
        }

        public static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler, int defaultListSize)
        {
            if(handlers == null)
            {
                handlers = (defaultListSize > 0 ? new List<WeakReference>(defaultListSize) : new List<WeakReference>());
            }

            handlers.Add(new WeakReference(handler));
        }

        public static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler)
        {
            AddWeakReferenceHandler(ref handlers, handler, -1);
        }

        public static void RemoveWeakReferenceHandler(List<WeakReference> handlers, EventHandler handler, int defaultListSize = 2)
        {
            //if (handlers.Count == 0)
            //{
            //    return;
            //}

            ////take snapshot
            //var items = handlers.Where(x => (x.Target as EventHandler) == null || (x.Target as EventHandler) == handler);

            //if(items.Count() == 0)
            //{
            //    return;
            //}

            //foreach (var item in items)
            //{
            //    handlers.Remove(item);
            //} 
            if (handlers != null)
            {
                for (int i = handlers.Count - 1; i >= 0; i--)
                {
                    WeakReference reference = handlers[i];
                    EventHandler existingHandler = reference.Target as EventHandler;
                    if ((existingHandler == null) || (existingHandler == handler))
                    {
                        // Clean up old handlers that have been collected
                        // in addition to the handler that is to be removed.
                        handlers.RemoveAt(i);
                    }
                }
            }

        }
    }
}
