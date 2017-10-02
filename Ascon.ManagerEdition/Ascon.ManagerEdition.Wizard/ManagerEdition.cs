using Ascon.ManagerEdition.Common.DICommon;
using Ascon.ManagerEdition.Common.PilotIceCommon.Observers;
using Ascon.ManagerEdition.Wizard.Models;
using Ascon.ManagerEdition.Wizard.Settings;
using Ascon.ManagerEdition.Wizard.Utils;
using Ascon.ManagerEdition.Wizard.Utils.Storage.StorageImplementation;
using Ascon.ManagerEdition.Wizard.Utils.Storage.StorageInterface;
using Ascon.ManagerEdition.Wizard.ViewModel;
using Ascon.ManagerEdition.Wizard.Views;
using Ascon.Pilot.SDK;
using Ascon.Pilot.SDK.Menu;
using Ascon.Pilot.Theme.ColorScheme;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Ascon.ManagerEdition.Wizard
{
    [Export(typeof(IMenu<ObjectsViewContext>))]
    public class ManagerEdition : IMenu<ObjectsViewContext>
    {
        private ProjectSection      _currentObject;
        private IObjectsRepository  _repository;
        private IObjectModifier     _modifier;
        private ISearchService      _search;
        private IFileProvider       _fileProvider;
        private ITabServiceProvider _tabServiceProvider;
        private IPilotDialogService _dialogService;

        private readonly string[] commands     = { "OPEN_REDACTION", "LOCK_REDACTION", "CREATE_REDACTION", "TABLE_REMARKS"};
        private readonly string[] commandsName = { "Редакция", "Блокировка редакци", "Создать редакцию", "Таблица замечаний" };

        [ImportingConstructor]
        public ManagerEdition(IObjectsRepository repository, 
                              IObjectModifier modifier, 
                              IPilotDialogService dialogService, 
                              ISearchService search, 
                              IFileProvider fileProvider,
                              ITabServiceProvider tabServiceProvider)
        {
            var accentColor = (Color)ColorConverter.ConvertFromString(dialogService.AccentColor);
            ColorScheme.Initialize(accentColor);

            _repository = repository;
            _modifier = modifier;
            _search = search;
            _fileProvider = fileProvider;
            _tabServiceProvider = tabServiceProvider;
            _dialogService = dialogService;
            InitializeObjects();
        }

        public void Build(IMenuBuilder builder, ObjectsViewContext context)
        {
            var settings = NinjectCommon.Kernel.Get<Settings.Settings>();
            _currentObject = context?.SelectedObjects
                                     .FirstOrDefault(x => settings.StartTypes.Any(t => x.Type.Name.Equals(t.Type)))?.MapToProjectSection();
            if (_currentObject != null)
            {
                var menu = builder.AddItem(commands[0], 4).WithHeader(commandsName[0]);
                for (int i = 1; i < 4; i++)
                {
                    menu.WithSubmenu().AddItem(commands[i], i - 1).WithHeader(commandsName[i]);
                }
                NinjectCommon.Kernel.Rebind<ProjectSection>().ToMethod(c => _currentObject).InSingletonScope();
                InitializeFolderRemark();                
            }
        }

        public void OnMenuItemClick(string name, ObjectsViewContext context)
        {
            switch (name)
            {
                case "LOCK_REDACTION": LockRedaction();
                    break;
                case "CREATE_REDACTION": CreateRedaction();
                    break;
                case "TABLE_REMARKS": TableRemarks();
                    break;
                default:
                    return;
            }
        }

        private void LockRedaction()
        {
            LockObject(_currentObject.Id.ToString());
        }

        private void CreateRedaction()
        {
            var vm = new CreateRedactionViewModel(() => { LockObject(_currentObject.Id.ToString()); });
            NinjectCommon.Kernel.Rebind<CreateRedactionViewModel>().ToMethod(o => vm).InSingletonScope();
            NinjectCommon.Kernel.Get<CreateRedactionView>().Show();
        }

        private void TableRemarks()
        {
            var settings = NinjectCommon.Kernel.Get<Settings.Settings>();

            string _curentCipherName = settings.StartTypes.FirstOrDefault(t => t.Type == _currentObject.Type.Name)
                                                          .AttributeCipher;

            string _curentCipherValue = _currentObject.Attributes[_curentCipherName].ToString();

            NinjectCommon.Kernel.Rebind<TableRemarksViewModel>().ToSelf().InSingletonScope();
            var view = NinjectCommon.Kernel.Get<TableRemarksView>();

            _tabServiceProvider.OpenTabPage($"Таблица замечаний к редакции {{{_curentCipherValue}}}", view, false);
        }

        private void InitializeObjects()
        {
            NinjectCommon.Kernel.Inject(this);

            NinjectCommon.Kernel.Bind<IObjectsRepository>().ToMethod(c => _repository).InSingletonScope();

            NinjectCommon.Kernel.Bind<IObjectModifier>().ToMethod(c => _modifier).InSingletonScope();

            NinjectCommon.Kernel.Bind<ISettingsFactory>().To<SettingsFactory>().InSingletonScope();

            NinjectCommon.Kernel.Bind<ISearchService>().ToMethod(c => _search).InSingletonScope();

            NinjectCommon.Kernel.Bind<IFileProvider>().ToMethod(c => _fileProvider).InSingletonScope();

            NinjectCommon.Kernel.Bind<ITabServiceProvider>().ToMethod(c => _tabServiceProvider).InSingletonScope();

            NinjectCommon.Kernel.Bind<IPilotDialogService>().ToMethod(c => _dialogService).InSingletonScope();

            NinjectCommon.Kernel.Bind<Settings.Settings>().ToMethod(o => NinjectCommon.Kernel.Get<ISettingsFactory>().Read());

            NinjectCommon.Kernel.Bind<IBaseObjectStorage>().To<BaseObjectStorage>().InSingletonScope();
        }

        private void InitializeFolderRemark()
        {
            NinjectCommon.Kernel.Get<SearchObjects>().Search(remarkFolder =>
            {
                if (remarkFolder.Any())
                {
                    NinjectCommon.Kernel.Get<InfoLoader>().Load(objects =>
                    {
                        var _folderRemark = objects.Where(o => o.ObjectStateInfo.State == ObjectState.Alive).Select(x => x.MapToProjectSection()).ToList();
                        NinjectCommon.Kernel.Rebind<List<ProjectSection>>().ToMethod(c => _folderRemark ?? new List<ProjectSection>()).InSingletonScope();
                    }, remarkFolder.ToArray());
                }
            }, _repository.GetType(NinjectCommon.Kernel.Get<Settings.Settings>().RemarkFolder.Type).Id, _currentObject.Name);
        }

        private void LockObject(string id)
        {
            string path = Assembly.GetExecutingAssembly().Location;
            string console = Path.Combine(Path.GetDirectoryName(path), @"ConsoleRemoveAllAccess");

            var settings = NinjectCommon.Kernel.Get<Settings.Settings>();
            var admin = settings.PersonIsAdmin;

            try
            {
                Process changedAccess = new Process();
                changedAccess.StartInfo.FileName = console; 
                // окно консоли скрытое, для отладки сделать Normal и 6-ой параметр 1
                changedAccess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                changedAccess.StartInfo.Arguments = "\"" + admin.ServerName + "\"" + " " +
                    "\"" + admin.BaseName + "\"" + " " +
                    "\"" + admin.Login + "\"" + " " +
                    "\"" + admin.Password + "\"" + " " +
                    "\"" + id + "\"" +
                    " 0";  // аргумент будет ли ждать нажатия в окне консоли (1-ждать, 0-не ждать, закрывать окно консоли)
                           // 1 - не использовать вместе с ProcessWindowStyle.Hidden !!!!!!!!!!!
                changedAccess.Start();
                changedAccess.WaitForExit();  // ждем 
            }
            catch (Exception e) { string a = e.Message; }
        }
    }
}
