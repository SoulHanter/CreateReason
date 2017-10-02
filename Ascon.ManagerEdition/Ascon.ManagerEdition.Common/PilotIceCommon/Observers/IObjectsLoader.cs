using System;
using System.Collections.Generic;
using Ascon.Pilot.SDK;

namespace Ascon.ManagerEdition.Common.PilotIceCommon.Observers
{
    public interface IObjectsLoader : IObserver<IDataObject>
    {
        void Load(Action<IList<IDataObject>> onLoadedAction, Func<IType, bool> typesFilter, params Guid[] ids);
    }
}
