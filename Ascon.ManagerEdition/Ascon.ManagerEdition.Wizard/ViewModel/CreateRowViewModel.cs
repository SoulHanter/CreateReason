using Ascon.ManagerEdition.Common.DICommon;
using Ascon.ManagerEdition.Common.MVVMCommon.Command;
using Ascon.ManagerEdition.Common.PilotIceCommon.Observers;
using Ascon.ManagerEdition.Wizard.Models;
using Ascon.ManagerEdition.Wizard.Utils;
using Ascon.ManagerEdition.Wizard.Utils.Storage.StorageImplementation;
using Ascon.ManagerEdition.Wizard.Utils.Storage.StorageInterface;
using Ascon.ManagerEdition.Wizard.ViewModel.Template;
using Ascon.Pilot.SDK;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Ascon.ManagerEdition.Wizard.ViewModel
{
    public class CreateRowViewModel: ViewModelBase
    {
        private TableRemarksModel _remark;
        private ObservableCollection<TableRemarksModel> _objects;
        private DocumentViewModel _documents;
        private DocumentViewModel _enlargement;

        private IRemarkStorage _remarkStorage => NinjectCommon.Kernel.Get<IRemarkStorage>();
        private Settings.Settings _settings => NinjectCommon.Kernel.Get<Settings.Settings>();

        public bool IsCreate { get; set; }
        public bool IsEdit { get; set; } = true;

        private ICommand _ok;
        private ICommand _cancel;
        private ICommand _addFile;

        public IEnumerable<string> GetFromUser => _settings.FromUser.Select(x => x.FullName).Distinct();
        public IEnumerable<string> GetToUser => _settings.ToUser.Select(x => x.FullName).Distinct();

        public CreateRowViewModel(ObservableCollection<TableRemarksModel> objects, Guid parentId, int UserId)
        {
            _objects = objects;
            _remark = new TableRemarksModel() { Act = Acts.FINALIZE,
                                                ParentId = parentId,
                                                Color = ColorsRow.NONE,
                                                Number = objects.Count + 1,
                                                UserId = UserId };
            InitializeDocuments();
        }

        public CreateRowViewModel(TableRemarksModel remark, ObservableCollection<TableRemarksModel> objects)
        {            
            _objects = objects;
            _remark = remark.Clone() as TableRemarksModel;
            InitializeDocuments();
        }
        
        public TableRemarksModel Remark
        {
            get => _remark;
            set
            {
                _remark = value;
                NotifyPropertyChanged(nameof(Remark));
            }
        }        

        public DocumentViewModel Documents
        {
            get => _documents;
            set
            {
                _documents = value;
                NotifyPropertyChanged(nameof(Documents));
            }
        }

        public DocumentViewModel Enlargements
        {
            get => _enlargement;
            set
            {
                _enlargement = value;
                NotifyPropertyChanged(nameof(Enlargements));
            }
        }

        public ICommand Ok => _ok ??
            (_ok = new DelegateCommand(() =>
            {
                UpdateDocuments();
                if (string.IsNullOrEmpty(_remark.FromUser) || !_remark.Document.DocumentList.Any())
                    System.Windows.MessageBox.Show("Не заполнены обязательные поля!");
                else
                {
                    if (IsCreate)
                    {
                        _objects.Add(_remark);
                        _remarkStorage.CreateRemark(_remark);

                    }
                    else
                    {
                        var item = _objects.Where(x => x.Id == _remark.Id).First();
                        _objects.Remove(item);
                        _objects.Add(_remark);
                        _remarkStorage.EditRemark(_remark);
                    }
                    NinjectCommon.Kernel.Get<ITabServiceProvider>().CloseTabPage(IsCreate ? "Создать замечание" : "Редактировать замечание");
                }
            }));

        public ICommand Cancel => _cancel ??
            (_cancel = new DelegateCommand(() =>
            {
                NinjectCommon.Kernel.Get<ITabServiceProvider>().CloseTabPage(IsCreate ? "Создать замечание" : "Редактировать замечание");
            }));

        public ICommand AddFile => _addFile ??
            (_addFile = new DelegateCommand<string>(x =>
            {
                var items = NinjectCommon.Kernel.Get<SelectObject>().SelectedItems(x);
                                                                    //.Where(t => t.Type.Name.Equals(_settings.DocumentForRedaction.Type));

                if (!items.Any())
                    return;

                foreach (var item in items)
                {
                    switch (x)
                    {
                        case "Выбрать приложения":
                            AppendItem(item, _enlargement);
                            break;

                        case "Выбрать документы редакции":
                            AppendItem(item, _documents);
                            break;

                        default: continue;
                    }
                }                

            }));

        private void InitializeDocuments()
        {
            _documents = new DocumentViewModel(_remark.Document);
            _enlargement = new DocumentViewModel(_remark.Enlargement);
        }

        private void AppendItem(ProjectSection item, DocumentViewModel collection)
        {
            if (collection.Documents.Where(x => x.Document.Id == item.Id).Any())
                return;
            collection.Documents.Add(item.MapToDocumentModel(collection.Documents));
        }

        private void UpdateDocuments()
        {
            _remark.Document.DocumentList = _documents.Documents.MapToDocuments();
            _remark.Enlargement.DocumentList = _enlargement.Documents.MapToDocuments();            
        }
        
    }
}
