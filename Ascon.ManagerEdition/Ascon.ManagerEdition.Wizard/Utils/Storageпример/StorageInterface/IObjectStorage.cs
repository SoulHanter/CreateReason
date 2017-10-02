using Ascon.ManagerEdition.Wizard.Models;
using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Utils.Storage.StorageClass
{
    public interface IObjectStorage: ICreator
    {
        void CreateObj(ProjectSection obj);

        void EditObj(ProjectSection obj);

        void DeleteObj(ProjectSection obj);

        void ChangedAccess(Guid id, IAccessRecord accessRecord);

        void LockAccess(ProjectSection currentObject);
        
    }
}
