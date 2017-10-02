using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ascon.ManagerEdition.Common.MVVMCommon.Command
{
    public class DelegateCommandAsync : IAsyncCommand
    {
        private readonly Func<Task> executeMethod;
        private readonly DelegateCommand command;
        private bool isExecuting;
        private bool IsAutomaticRequeryDisabled;

        public DelegateCommandAsync(Func<Task> executeMethod) : this(executeMethod, () => true, false)
        {

        }

        public DelegateCommandAsync(Func<Task> executeMethod, Func<bool> canExecute) : this(executeMethod, canExecute, false)
        {

        }

        public DelegateCommandAsync(Func<Task> executeMethod, Func<bool> canExecute, bool IsAutomaticRequeryDisabled)
        {
            if (executeMethod == null)
            {
                throw new ArgumentNullException("executeMethod");
            }

            this.executeMethod = executeMethod;
            command = new DelegateCommand(() => { }, canExecute, IsAutomaticRequeryDisabled);
            this.IsAutomaticRequeryDisabled = IsAutomaticRequeryDisabled;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                command.CanExecuteChanged += value;
            }

            remove
            {
                command.CanExecuteChanged -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return !isExecuting && command.CanExecute(parameter);
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        public async Task ExecuteAsync(object obj)
        {
            try
            {
                isExecuting = true;
                RaiseCanExecuteChanged();
                await executeMethod();
            }
            finally
            {
                isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            command.RaiseCanExecuteChanged();
        }

    }

    public class DelegateCommandAsync<T> : IAsyncCommand<T>
    {
        private readonly Func<T, Task> executeMethod;
        private readonly DelegateCommand<T> command;
        private bool isExecuting;
        private bool IsAutomaticRequeryDisabled;

        public DelegateCommandAsync(Func<T, Task> executeMethod) : this(executeMethod, (T) => true, false)
        {

        }

        public DelegateCommandAsync(Func<T, Task> executeMethod, Func<T, bool> canExecute) : this(executeMethod, canExecute, false)
        {

        }

        public DelegateCommandAsync(Func<T, Task> executeMethod, Func<T, bool> canExecute, bool IsAutomaticRequeryDisabled)
        {
            if (executeMethod == null)
            {
                throw new ArgumentNullException("executeMethod");
            }

            this.executeMethod = executeMethod;
            command = new DelegateCommand<T>((x) => { }, canExecute, IsAutomaticRequeryDisabled);
            this.IsAutomaticRequeryDisabled = IsAutomaticRequeryDisabled;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                command.CanExecuteChanged += value;
            }
            remove
            {
                command.CanExecuteChanged -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return !isExecuting && command.CanExecute((T)parameter);
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync((T)parameter);
        }

        public async Task ExecuteAsync(T obj)
        {
            try
            {
                isExecuting = true;
                RaiseCanExecuteChanged();
                await executeMethod(obj);
            }
            finally
            {
                isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            command.RaiseCanExecuteChanged();
        }
    }
}
