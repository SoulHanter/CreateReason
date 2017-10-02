using Ascon.ManagerEdition.Wizard.Utils.Storage.StorageClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.ManagerEdition.Wizard.Models;
using Ascon.Pilot.SDK;
using Ascon.ManagerEdition.Common.DICommon;
using Ninject;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Ascon.ManagerEdition.Wizard.ViewModel;

namespace Ascon.ManagerEdition.Wizard.Utils.Storage.StorageClass
{
    public class ObjectStorage : ViewModelBase, IObjectStorage
    {
        private IFileProvider _fileProvider => NinjectCommon.Kernel.Get<IFileProvider>();

        public IObjectsRepository _repository => NinjectCommon.Kernel.Get<IObjectsRepository>();

        public IObjectModifier _modifer => NinjectCommon.Kernel.Get<IObjectModifier>();

        public Settings.Settings _settings => NinjectCommon.Kernel.Get<Settings.Settings>();

        public List<ProjectSection> _folderRemark => NinjectCommon.Kernel.Get<List<ProjectSection>>();

        public void CreateObj(ProjectSection obj)
        {
            var builder = _modifer?.CreateById(obj.Id, obj.ParentId, _repository.GetType(obj.Type.Id));
            
            if (builder != null)
            {
                SetAttribute(builder, obj.Attributes);
                SetFiles(builder, obj.Files);
                SetAccess(builder, obj.Access);
            }

            _modifer?.Apply();
        }

        public bool CreateObject(ProjectSection obj)
        {
            var builder = _modifer?.CreateById(obj.Id, obj.ParentId, _repository.GetType(obj.Type.Id));

            if (builder != null)
            {
                SetAttribute(builder, obj.Attributes);
                SetFiles(builder, obj.Files);
                SetAccess(builder, obj.Access);
            }

            return builder != null;
        }

        public void DeleteObj(ProjectSection obj)
        {
            _modifer?.DeleteById(obj.Id);
            _modifer?.Apply();
        }

        public void EditObj(ProjectSection obj)
        {
            var builder = _modifer?.EditById(obj.Id);

            if (builder != null)
            {
                SetAttribute(builder, obj.Attributes);
                SetFiles(builder, obj.Files);
                SetAccess(builder, obj.Access);
            }
            _modifer?.Apply();
        }

        public void ChangedAccess(Guid id, IAccessRecord accessRecord)
        {
            try
            {
                var builder = _modifer.EditById(id);
                builder.SetAccessRights(accessRecord.OrgUnitId, accessRecord.Access.AccessLevel, accessRecord.Access.ValidThrough, accessRecord.Access.IsInheritable);
                _modifer.Apply();
            }
            catch { }
        }

        public Task CreateAsync(List<ProjectSection> objects, Guid parentId)
        {
            return Task.Factory.StartNew(() =>
            {
                if (objects == null || !objects.Any())
                    return;

                CreateRecursive(objects, parentId);

                _modifer?.Apply();
            });
        }

        public IAccessRecord GetAccessRecord(string orgUnit, AccessLevel access)
        {
            var accessRecord = new AccessRecord();

            var firstOrDefault = _repository
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

        public void LockAccess(ProjectSection currentObject)
        {
            var settings = NinjectCommon.Kernel.Get<Settings.Settings>();

            //ограничение прав
            List<IAccessRecord> accessRecord;
            
            accessRecord = currentObject.Access.Where(x => x.Access.IsInherited).ToList();

            for (int i = 0; i < accessRecord.Count; i++)
            {
                if (accessRecord[i].InheritanceSource != SystemObjectIds.RootObjectId)
                {
                    _modifer.EditById(accessRecord[i].InheritanceSource).RemoveAccessRights(accessRecord[i].OrgUnitId);
                    var access = GetAccessRecord(accessRecord[i], accessRecord[i].Access.AccessLevel);
                    ChangedAccess(accessRecord[i].InheritanceSource, access);
                }
            }


            accessRecord = currentObject.Access.Where(x => !x.Access.IsInherited &&
                                                           !_repository.GetCurrentPerson()
                                                                       .Positions
                                                                       .Select(p => p.Position)
                                                                       .Contains(x.OrgUnitId)).ToList();

            //удаляем лишнее
            for (int i = 0; i < accessRecord.Count; i++)
            {
                _modifer.EditById(currentObject.Id).RemoveAccessRights(accessRecord[i].OrgUnitId);
            }

            //добавление прав
            var types = settings.Subdivisions.Distinct().ToList();
            for (int i = 0; i < types.Count; i++)
            {
                var access = GetAccessRecord(types[i], AccessLevel.View);
                ChangedAccess(currentObject.Id, access);
            }

            accessRecord = currentObject.Access.Where(x => _repository.GetCurrentPerson()
                                                                      .Positions
                                                                      .Select(p => p.Position)
                                                                      .Contains(x.OrgUnitId)).ToList();
            //удаляем у текущего
            for (int i = 0; i < accessRecord.Count; i++)
            {
                _modifer.EditById(currentObject.Id).RemoveAccessRights(accessRecord[i].OrgUnitId);
            }
        }        

        private void SetAttribute(IObjectBuilder builder, IDictionary<string, object> attributes)
        {
            if (attributes.Any())
            {
                foreach (var attribute in attributes)
                {
                    builder.SetAttribute(attribute.Key, attribute.Value.ToString());
                }
            }
        }

        private void SetFiles(IObjectBuilder builder, ReadOnlyCollection<IFile> files)
        {
            if (files.Any())
            {
                foreach (var file in files)
                {
                    try
                    {
                        var stream = _fileProvider.OpenRead(file);
                        builder.AddFile(file.Name, stream, file.Created, file.Accessed, file.Modified);
                    }
                    catch (Exception e) { var a = e.Message; }
                }
            }
        }

        private void SetAccess(IObjectBuilder builder, List<IAccessRecord> accesses)
        {
            if (accesses.Any())
            {
                foreach (var access in accesses)
                {
                    try
                    {
                        builder.SetAccessRights(access.OrgUnitId, access.Access.AccessLevel, access.Access.ValidThrough, access.Access.IsInheritable);
                    }
                    catch (Exception) { }
                }
            }
        }

        private void CreateRecursive(List<ProjectSection> objects, Guid parentId)
        {
            var children = objects.FindAll(o => o.ParentId == parentId);

            if (!children.Any())
                return;

            foreach (var child in children)
            {
                if (CreateObject(child))
                    CreateRecursive(objects, child.Id);
            }
        }
    }
}
