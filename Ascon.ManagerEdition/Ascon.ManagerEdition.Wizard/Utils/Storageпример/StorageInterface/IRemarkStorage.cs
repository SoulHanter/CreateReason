using Ascon.ManagerEdition.Wizard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Utils.Storage.StorageClass
{
    public interface IRemarkStorage
    {
        void CreateRemark(TableRemarksModel obj);

        void EditRemark(TableRemarksModel obj);

        void DeleteRemark(TableRemarksModel obj);

        ColorsRow SetRemarkColor(TableRemarksModel obj);
    }
}
