using Ascon.ManagerEdition.Common.DICommon;
using Ascon.ManagerEdition.Common.MVVMCommon.Command;
using Ascon.ManagerEdition.Wizard.Models;
using Ascon.Pilot.SDK;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Ascon.ManagerEdition.Wizard.ViewModel.Template
{
    public class DocumentModel
    {
        private Document _document;
        private ICommand _delete;
        private ICommand _postLink;
        private ObservableCollection<DocumentModel> _documents;

        public DocumentModel(Document document, ObservableCollection<DocumentModel> documents)
        {
            _document = document;
            _documents = documents;
        }

        public Document Document => _document;    

        public ICommand Delete => _delete ??
            (_delete = new DelegateCommand(() =>
            {
                var currentDoc = _documents.FirstOrDefault(x => x._document.Id == _document.Id);
                _documents.Remove(currentDoc);
            }));

        public ICommand PostLink => _postLink ??
            (_postLink = new DelegateCommand<Uri>(x =>
            {
                if (string.IsNullOrEmpty(x.ToString()) || x.ToString().IndexOf(@"piloturi://") != 0 )
                    return;

                var id = x.ToString().Substring(11, 36);

                var _tab = NinjectCommon.Kernel.Get<ITabServiceProvider>();
                _tab.ShowElement(new Guid(id));

            }));
    }
}
