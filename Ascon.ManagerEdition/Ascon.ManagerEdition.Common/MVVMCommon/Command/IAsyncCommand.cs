using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ascon.ManagerEdition.Common.MVVMCommon.Command
{
    public interface IAsyncCommand : IAsyncCommand<object>
    {

    }

    public interface IAsyncCommand<T> : ICommand
    {
        Task ExecuteAsync(T obj);
    }
}
