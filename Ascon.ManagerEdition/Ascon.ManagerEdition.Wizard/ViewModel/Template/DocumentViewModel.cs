using Ascon.ManagerEdition.Common.MVVMCommon.Command;
using Ascon.ManagerEdition.Wizard.Models;
using Ascon.ManagerEdition.Wizard.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Ascon.ManagerEdition.Wizard.ViewModel.Template
{
    public class DocumentViewModel : ViewModelBase
    {
        private ObservableCollection<DocumentModel> _documents;

        public DocumentViewModel(Documents documents)
        {
            _documents = documents.DocumentList.Any() ? 
                         documents.MapToDocument() : 
                         new ObservableCollection<DocumentModel>();
        }

        public ObservableCollection<DocumentModel> Documents => _documents;

    }
}
