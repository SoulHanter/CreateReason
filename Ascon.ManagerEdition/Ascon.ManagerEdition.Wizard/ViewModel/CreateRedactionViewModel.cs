using Ascon.ManagerEdition.Common.DICommon;
using Ascon.ManagerEdition.Common.MVVMCommon.Command;
using Ascon.ManagerEdition.Common.PilotIceCommon.Observers;
using Ascon.ManagerEdition.Wizard.Models;
using Ascon.ManagerEdition.Wizard.Utils;
using Ascon.ManagerEdition.Wizard.Utils.Storage;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ascon.ManagerEdition.Wizard.ViewModel
{
    public class CreateRedactionViewModel: StoragePilotRedaction
    {
        private char _lastChar;
        private string _curentCipherName;
        private string _curentCipherValue;
        private Guid _parentId;

        private ICommand _createdRedaction;
        private Action _close;
        private Action _lock;

        public CreateRedactionViewModel(Action commandLock)
        {
            _lock = commandLock;

            _curentCipherName = Storage.Settings.StartTypes.FirstOrDefault(t => t.Type == _currentObject.Type.Name)
                                                           .AttributeCipher;

            _curentCipherValue = _currentObject.Attributes[_curentCipherName]?.ToString();

            _lastChar = _curentCipherValue[0];

            _parentId = _currentObject.ParentId;

            ActionCreateP = _lastChar.Equals('P');
            ActionCreateA = _lastChar.Equals('А') || _lastChar.Equals('P');
            ActionCreateI = _lastChar.Equals('И') || _lastChar.Equals('А');
        }

        public bool ActionCreateP { get; private set; } = false;
        public bool ActionCreateA { get; private set; } = false;
        public bool ActionCreateI { get; private set; } = false;

        public Action Close
        {
            get { return _close; }
            set
            {
                _close = value;
                NotifyPropertyChanged(nameof(Close));
            }
        }

        public ICommand CreatedRedaction => _createdRedaction ??
            (_createdRedaction = new DelegateCommandAsync<string>(async x => 
            {
                switch (x)
                {
                    case "createP":
                        _curentCipherValue = IncrementChiperValue(_curentCipherValue);
                        break;
                    case "createA":
                        _curentCipherValue = _lastChar.Equals('А') ? IncrementChiperValue(_curentCipherValue) : "А00";
                        break;
                    case "createI":
                        _curentCipherValue = _lastChar.Equals('И') ? IncrementChiperValue(_curentCipherValue) : "И00";
                        break;
                    default:
                        _curentCipherValue = null;
                        break;
                }

                if (_curentCipherValue != null)
                {
                    if (ExistsRemarkFolder(_currentObject))
                    {
                        await AppendObjects(_curentCipherValue);
                        _lock();
                        Close();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Нет основания создания новой редакции!", "Добавьте замечание в таблицу!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                }
            }));     

        private string IncrementChiperValue(string source)
        {
            if (source.Length < 3)
                return null;

            string cipher = source;            
            try
            {
                int intCihper = int.Parse(cipher.Remove(0, 1));
                intCihper++;

                if (intCihper.ToString().Length == 1)
                    return $"{source[0]}0{intCihper}";
                else
                    return $"{source[0]}{intCihper}";

            }
            catch (Exception)
            {
                return null;
            }
        }

        private Task AppendObjects(string curentCipherValue)
        {
            return Task.Factory.StartNew(() =>
            {
                var newObj = CreateNewRedaction(_currentObject, _curentCipherName, curentCipherValue);

                if (newObj == null)
                    return;

                //var attr = Storage.Settings.StartTypes.FirstOrDefault(x => x.Type.Equals(_currentObject.Type.Name)).GuidNextObject ?? null;

                //if (!string.IsNullOrEmpty(attr))
                //    _currentObject.Attributes.Add(attr, newObj.Id);

                //Storage.EditObject(_currentObject);


                NinjectCommon.Kernel.Get<IObjectsLoader>("child").Load(objects =>
                {
                    var _objects = objects.Where(t => t.Id != _currentObject.Id)
                                          .Select(o => o.MapToProjectSection()).ToList();                    
                    Initialize(newObj, _objects);

                }, type => true,
                   _currentObject.Id);
            });
        }

        private void Initialize(ProjectSection obj, List<ProjectSection> objects)
        {            
            var items = objects.ChangedGuid(_currentObject.Id, obj.Id).DeleteAccesses();
            items.Add(obj);
            Storage.CreateAsync(obj.ParentId, items.ToArray());
        }

    }
}
