using Ascon.ManagerEdition.Common.DICommon;
using Ascon.ManagerEdition.Wizard.Models;
using Ascon.ManagerEdition.Wizard.Utils.Storage.StorageInterface;
using Ascon.ManagerEdition.Wizard.ViewModel;
using Ascon.Pilot.SDK;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Utils.Storage
{
    public abstract class PilotStorageBase : ViewModelBase
    {
        public IBaseObjectStorage Storage { get; }

        public ProjectSection _folderRemark = null; //=> NinjectCommon.Kernel.Get<List<ProjectSection>>()

        public ProjectSection _currentObject = NinjectCommon.Kernel.Get<ProjectSection>();

        public PilotStorageBase()
        {
            Storage = NinjectCommon.Kernel.Get<IBaseObjectStorage>();
            _folderRemark = NinjectCommon.Kernel.Get<List<ProjectSection>>().FirstOrDefault(x => x.Name == _currentObject.Name &&
                                                                                                 x.Attributes.ContainsKey(Storage.Settings?.RemarkFolder?.AttributeCipher) &&
                                                                                                 x.Attributes[Storage.Settings.RemarkFolder.AttributeCipher].Equals(_currentObject.Id.ToString()));
        }

        public bool ExistsRemarkFolder(ProjectSection currentObject)
        {            
            return _folderRemark?.Children != null ? _folderRemark.Children.Any() : false;
        }      

        public IAccessRecord GetAccessRecord(string orgUnit, AccessLevel access)
        {
            var accessRecord = new AccessRecord();

            var firstOrDefault = Storage.Repository
                                        .GetOrganisationUnits()
                                        .FirstOrDefault(x => x.Title == orgUnit);

            if (firstOrDefault != null)
                accessRecord.OrgUnitId = firstOrDefault.Id;

            accessRecord.Access = new Access() { AccessLevel = access };

            return accessRecord;
        }

        public IAccessRecord GetAccessRecord(IAccessRecord accessRecord, AccessLevel access)
        {
            var _accessRecord = new AccessRecord();

            _accessRecord.InheritanceSource = accessRecord.InheritanceSource;
            _accessRecord.OrgUnitId = accessRecord.OrgUnitId;
            _accessRecord.RecordOwner = accessRecord.RecordOwner;

            _accessRecord.Access = new Access() { AccessLevel = access, IsInheritable = false };

            return _accessRecord;
        }

        public bool TheUserIsGip()
        {
            var gips = Storage.Repository
                              .GetOrganisationUnits().FirstOrDefault(x => x.Title.Equals(Storage.Settings.GroupGipName))?.Children;

            if (gips == null)
                return false;

            return gips.Where(x => Storage.Repository.GetCurrentPerson().Positions.Select(p => p.Position)
                                                                                  .Contains(x)).Any();
        }

        public IPerson CurrentUser()
        {
            return Storage.Repository.GetCurrentPerson();
        }

        public Guid GetIdFromLink(string link)
        {
            Guid id = Guid.Empty;

            if (string.IsNullOrEmpty(link) || link.Length < 47)
                return id;

            link = link.Remove(0, 11);

            if (link.Length > 36)
            {
                link = link.Remove(36);
            }

            id = Guid.Parse(link);

            return id;
        }
    }
}
