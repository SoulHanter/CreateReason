using Ascon.ManagerEdition.Wizard.Models;
using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Utils.Storage.StorageInterface
{
    public interface IBaseObjectStorage: ICreator
    {
        Settings.Settings Settings { get; }

        IObjectsRepository Repository { get; }

        void CreateObject(ProjectSection obj);

        void EditObject(ProjectSection obj);

        void DeleteObject(ProjectSection obj);        
    }
}
