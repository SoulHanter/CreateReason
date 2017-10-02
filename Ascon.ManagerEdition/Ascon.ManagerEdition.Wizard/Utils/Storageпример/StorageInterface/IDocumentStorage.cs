using Ascon.ManagerEdition.Wizard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Utils.Storage.StorageClass
{
    public interface IDocumentStorage
    {
        void EditDocument(Acts act, string link);

        void EditDocument(TableRemarksModel item);

        void CreateDocument(ProjectSection doc);
    }
}
