using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ascon.ManagerEdition.Common.MVVMCommon.Command
{
    public class DelegateCommand : ICommand
    {
        private readonly Action executeMethod;
        private readonly Func<bool> canExecute;
        private bool isAutomaticRequeryDisabled;
        private List<WeakReference> canExecuteChangedHandlers;

        public DelegateCommand(Action executeMethod):this(executeMethod, () => true, false)
        {

        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecute) :this(executeMethod,canExecute,false)
        {

        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecute, bool isAutomaticRequeryDisabled)
        {
            if(executeMethod == null)
            {
                throw new ArgumentNullException("executeMethod");
            }

            this.executeMethod = executeMethod;
            this.canExecute = canExecute;
            this.isAutomaticRequeryDisabled = isAutomaticRequeryDisabled;
            canExecuteChangedHandlers = new List<WeakReference>();
        }


        public bool IsAutomaticRequeryDisabled
        {
            get { return isAutomaticRequeryDisabled; }
            set
            {
                if(isAutomaticRequeryDisabled != value)
                {
                    if (value)
                    {
                        CommandManagerHelper.RemoveHandlersRequerySuggested(canExecuteChangedHandlers);
                    }
                    else
                    {
                        CommandManagerHelper.AddHandlersRequerySuggested(canExecuteChangedHandlers);
                    }

                    isAutomaticRequeryDisabled = value;
                }
            }
        }


        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (!isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested += value;
                }

                CommandManagerHelper.AddWeakReferenceHandler(ref canExecuteChangedHandlers,value,2);
            }

            remove
            {
                if (!isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested -= value;
                }

                CommandManagerHelper.RemoveWeakReferenceHandler(canExecuteChangedHandlers, value);
            }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute();
        }

        public void Execute(object parameter)
        {
            executeMethod?.Invoke();
        }

        protected virtual void OnCanExecuteChanged()
        {
            CommandManagerHelper.ExecuteWeakReferenceHandlers(canExecuteChangedHandlers);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> executeMethod;
        private readonly Func<T, bool> canExecute;
        private bool isAutomaticRequeryDisabled;
        private List<WeakReference> canExecuteChangedHandlers;

        public DelegateCommand(Action<T> executeMethod):this(executeMethod,(t)=>true,false)
        {

        }

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecute):this(executeMethod,canExecute,false)
        {

        }

        public DelegateCommand(Action<T> executeMethod, Func<T,bool> canExecute, bool isAutomaticRequeryDisabled)
        {
            if(executeMethod == null)
            {
                throw new ArgumentNullException("executeMethod");
            }

            this.executeMethod = executeMethod;
            this.canExecute = canExecute;
            this.isAutomaticRequeryDisabled = isAutomaticRequeryDisabled;
            canExecuteChangedHandlers = new List<WeakReference>();
        }

        public bool IsAutomaticRequeryDisabled
        {
            get { return isAutomaticRequeryDisabled; }
            set
            {
                if (isAutomaticRequeryDisabled != value)
                {
                    if (value)
                    {
                        CommandManagerHelper.RemoveHandlersRequerySuggested(canExecuteChangedHandlers);
                    }
                    else
                    {
                        CommandManagerHelper.AddHandlersRequerySuggested(canExecuteChangedHandlers);
                    }

                    isAutomaticRequeryDisabled = value;
                }
            }
        }


        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (!isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested += value;
                }

                CommandManagerHelper.AddWeakReferenceHandler(ref canExecuteChangedHandlers, value, 2);
            }

            remove
            {
                if (!isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested -= value;
                }

                CommandManagerHelper.RemoveWeakReferenceHandler(canExecuteChangedHandlers, value);
            }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            executeMethod?.Invoke((T)parameter);
        }

        protected virtual void OnCanExecuteChanged()
        {
            CommandManagerHelper.ExecuteWeakReferenceHandlers(canExecuteChangedHandlers);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }
    }

}
