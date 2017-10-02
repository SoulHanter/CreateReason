using Ascon.ManagerEdition.Wizard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Utils.Storage.StorageInterface
{
    public interface IRemarkStorage
    {
        void CreateRemark(TableRemarksModel remark);
        void EditRemark(TableRemarksModel remark);
        void DeleteRemark(TableRemarksModel remark);
    }
}
