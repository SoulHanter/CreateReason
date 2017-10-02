using Ascon.ManagerEdition.Common.Common;
using Ascon.ManagerEdition.Common.MVVMCommon.Messenger;
using Ascon.ManagerEdition.Common.PilotIceCommon.Observers;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Common.DICommon
{
    public class NinjectCommon
    {
        private static IKernel kernel;

        private static object syncRoot = new Object();

        public static IKernel Kernel
        {
            get
            {
                if (kernel == null)
                {
                    lock (syncRoot)
                    {
                        if (kernel == null)
                        {
                            kernel = new StandardKernel();
                            Register(kernel);
                        }
                    }
                }
                return kernel;
            }
        }

        private static void Register(IKernel kernel)
        {
            //Register services here
            kernel.Bind<IMessenger>().To<Messenger>().InSingletonScope();
            kernel.Bind<IObjectsLoader>().To<ObjectLoader>().Named("child");
            kernel.Bind<IObjectsLoader>().To<ParentLoader>().Named("parent");
            kernel.Bind<IMessageService>().To<MessageService>().InSingletonScope();
            kernel.Bind<SearchObjects>().ToSelf().InSingletonScope();
            kernel.Bind<InfoLoader>().ToSelf().InSingletonScope();
        }
    }
}
