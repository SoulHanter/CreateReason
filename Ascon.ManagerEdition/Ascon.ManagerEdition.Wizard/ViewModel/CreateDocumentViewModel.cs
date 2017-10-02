using Ascon.ManagerEdition.Common.DICommon;
using Ascon.ManagerEdition.Common.MVVMCommon.Command;
using Ascon.ManagerEdition.Wizard.Models;
using Ascon.ManagerEdition.Wizard.Utils;
using Ascon.Pilot.SDK;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Ascon.ManagerEdition.Wizard.ViewModel
{
    public class CreateDocumentViewModel: ViewModelBase
    {
        private Action _close;
        private Action<ProjectSection> _command;

        private string _type;
        private string _name;

        private ProjectSection _doc;

        private ICommand _ok;
        private ICommand _cancel;

        public CreateDocumentViewModel(ProjectSection doc, Action<ProjectSection> command)
        {
            _doc = doc.Clone() as ProjectSection;
            _command = command;
        }

        public Action Close
        {
            get { return _close; }
            set
            {
                _close = value;
                NotifyPropertyChanged(nameof(Close));
            }
        }

        public string[] Types { get; private set; } =
        {
            "Документ"
        };

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                NotifyPropertyChanged(nameof(Type));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public ICommand Ok => _ok ??
            (_ok = new DelegateCommand(() =>
            {
                var repository = NinjectCommon.Kernel.Get<IObjectsRepository>();
                var _settings = NinjectCommon.Kernel.Get<Settings.Settings>();

                _doc.Attributes.Add(_settings.DocumentForRedaction.AttributeCipher, _name);
                _doc.Type = repository.GetType(_settings.DocumentForRedaction.Type).MapToType();

                _command(_doc);

                Close();
            }));

        public ICommand Cancel => _cancel ??
             (_cancel = new DelegateCommand(() =>
             {
                 Close();
             }));


    }
}
