using Ascon.Pilot.SDK;
using Ascon.ProjectWizard.Common.DICommon;
using Ascon.ProjectWizard.Common.MVVMCommon.Messenger;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ascon.ProjectWizard.Common.MVVMCommon.NTPC
{
    public abstract class ViewModelBase : ColleguageBase, INotifyPropertyChanged
    {
        public IMessenger Messenger { get; private set; }

        public IDataObject Current { get; private set; }

        public ViewModelBase()
        {
            Messenger = NinjectCommon.Kernel.Get<IMessenger>();
            Current = NinjectCommon.Kernel.Get<IDataObject>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
