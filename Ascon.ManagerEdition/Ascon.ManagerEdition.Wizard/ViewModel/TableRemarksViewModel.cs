using Ascon.ManagerEdition.Common.DICommon;
using Ascon.ManagerEdition.Common.MVVMCommon.Command;
using Ascon.ManagerEdition.Wizard.Models;
using Ascon.ManagerEdition.Wizard.Utils;
using Ascon.ManagerEdition.Wizard.Utils.Storage;
using Ascon.ManagerEdition.Wizard.Utils.Storage.StorageImplementation;
using Ascon.ManagerEdition.Wizard.Utils.Storage.StorageInterface;
using Ascon.ManagerEdition.Wizard.Views;
using Ascon.Pilot.SDK;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ascon.ManagerEdition.Wizard.ViewModel
{
    public class TableRemarksViewModel : StoragePilotRemark
    {
        private object _currentItem;
        private string _fromUser;
        private string _toUser;
        private IRemarkStorage _remarkStorage;

        #region Command
        private ICommand _createRow;
        private ICommand _editRow;
        private ICommand _deleteRow;
        private ICommand _sort;
        private ICommand _clear;
        #endregion

        public IEnumerable<string> GetFromUser => Storage.Settings.FromUser.Select(x => x.FullName).Distinct();
        public IEnumerable<string> GetToUser => Storage.Settings.ToUser.Select(x => x.FullName).Distinct();

        public string FromUser
        {
            get => _fromUser;
            set
            {
                _fromUser = value;
                NotifyPropertyChanged(nameof(FromUser));
            }
        }

        public string ToUser
        {
            get => _toUser;
            set
            {
                _toUser = value;
                NotifyPropertyChanged(nameof(ToUser));
            }
        }        

        public TableRemarksViewModel()
        {
            _remarkStorage = new RemarkStorage(Storage);
            NinjectCommon.Kernel.Rebind<IRemarkStorage>().ToMethod(o => _remarkStorage).InSingletonScope();           
        }           

        public ObservableCollection<TableRemarksModel> Remarks
        {
            get => remarks;
            set
            {
                remarks = value;
                NotifyPropertyChanged(nameof(Remarks));
            }
        }

        public object SelectedItem
        {
            get => _currentItem;
            set
            {
                _currentItem = value;
                NotifyPropertyChanged(nameof(SelectedItem));
            }
        }

        public ICommand CreateRow => _createRow ??
            (_createRow = new DelegateCommand(() =>
            {
                var vm = new CreateRowViewModel(remarks, parentFolderRemark.Id, CurrentUser().Id) { IsCreate = true };
                NinjectCommon.Kernel.Rebind<CreateRowViewModel>().ToMethod(o => vm).InSingletonScope();

                var view = NinjectCommon.Kernel.Get<CreateRowView>();
                var tabControl = NinjectCommon.Kernel.Get<ITabServiceProvider>();
                tabControl.OpenTabPage("Создать замечание", view, false);
            }));

        public ICommand EditRow => _editRow ??
            (_editRow = new DelegateCommand(() =>
            {
                if (SelectedItem != null && (SelectedItem as TableRemarksModel).UserId == CurrentUser().Id)
                {
                    var vm = new CreateRowViewModel(SelectedItem as TableRemarksModel, remarks)
                    { IsCreate = false, IsEdit = !(SelectedItem as TableRemarksModel).Statement };
                    NinjectCommon.Kernel.Rebind<CreateRowViewModel>().ToMethod(o => vm).InSingletonScope();

                    var view = NinjectCommon.Kernel.Get<CreateRowView>();
                    var tabControl = NinjectCommon.Kernel.Get<ITabServiceProvider>();
                    tabControl.OpenTabPage("Редактировать замечание", view, false);
                }
            }));

        public ICommand DeleteRow => _deleteRow ??
            (_deleteRow = new DelegateCommand(() =>
            {
                if (SelectedItem != null && (SelectedItem as TableRemarksModel).UserId == CurrentUser().Id)
                {
                    _remarkStorage.DeleteRemark(SelectedItem as TableRemarksModel);
                    remarks.Remove(SelectedItem as TableRemarksModel);
                }
            }));

        public ICommand Sort => _sort ??
            (_sort = new DelegateCommand<bool>(x =>
            {
                if (!x)
                {
                    FromUser = null;
                    ToUser = null;
                }
                Remarks = new ObservableCollection<TableRemarksModel>(SortRemark(_fromUser, _toUser).OrderBy(g => g.Number));
            }));

        public ICommand Clear => _clear ??
            (_clear = new DelegateCommand<string>(x =>
            {
                if (x.Equals("FromUser"))
                    FromUser = null;
                if (x.Equals("ToUser"))
                    ToUser = null;
            }));

      


        



        

        //public ICommand Confirm => _confirm ??
        //    (_confirm = new DelegateCommand(() =>
        //    {
        //        if (remarks.Any() && TheUserIsGip())
        //        {
        //            foreach (var item in remarks.Where(x => !x.Statement))
        //            {
        //                //ComfirmEdit(item);
        //                //EditRemark(item);
        //            }
        //            Remarks = new ObservableCollection<TableRemarksModel>(_remarks);
        //        }
        //    }));   


        //private void ComfirmEdit(TableRemarksModel remark)
        //{
        //    if (remark.Statement)
        //        return;

        //    remark.Statement = true;

        //    switch (remark.Act)
        //    {
        //        case Acts.FINALIZE: EditDocument(remark.Act, remark.Document);
        //            break;
        //        case Acts.DEVELOP:
        //            {
        //                ProjectSection doc = new ProjectSection() { ParentId = _currentObject.Id };
        //                var vm = new CreateDocumentViewModel(doc, x => { CreateDocument(x); });
        //                NinjectCommon.Kernel.Rebind<CreateDocumentViewModel>().ToMethod(o => vm).InSingletonScope();
        //                NinjectCommon.Kernel.Get<CreateDocumentView>().Show();                        

        //            }
        //            break;
        //        case Acts.EXCLUDE: EditDocument(remark.Act, remark.Document);
        //            break;
        //        default:
        //            break;
        //    }
        //    remark.Color = GetColor(remark);
        //}

        //private void CommandDelete(TableRemarksModel remark)
        //{
        //    if (System.Windows.MessageBox.Show("Вы действительно хотите удалить замечание?", "Удалить замечание?",
        //                                       System.Windows.MessageBoxButton.YesNo,
        //                                       System.Windows.MessageBoxImage.Warning) == System.Windows.MessageBoxResult.No)
        //        return;

        //    switch (remark.Act)
        //    {
        //        case Acts.FINALIZE:
        //            break;
        //        case Acts.DEVELOP:
        //            DeleteRemark(remark);
        //            _remarks.Remove(remark);
        //            break;
        //        case Acts.EXCLUDE:
        //            break;
        //        case Acts.DISMISS:
        //            DeleteRemark(remark);
        //            _remarks.Remove(remark);
        //            break;
        //        default:
        //            break;
        //    }            
        //}



    }


}
