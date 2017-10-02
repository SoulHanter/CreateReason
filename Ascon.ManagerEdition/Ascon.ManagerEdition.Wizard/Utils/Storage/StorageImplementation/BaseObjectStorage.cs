using Ascon.ManagerEdition.Wizard.Utils.Storage.StorageInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.ManagerEdition.Wizard.Models;
using Ascon.Pilot.SDK;
using System.Threading.Tasks;
using Ascon.ManagerEdition.Common.DICommon;
using Ninject;
using System.Collections.ObjectModel;

namespace Ascon.ManagerEdition.Wizard.Utils.Storage.StorageImplementation
{
    public class BaseObjectStorage : IBaseObjectStorage
    {
        private IFileProvider _fileProvider => NinjectCommon.Kernel.Get<IFileProvider>();

        private IObjectModifier _modifer => NinjectCommon.Kernel.Get<IObjectModifier>();

        public Settings.Settings Settings => NinjectCommon.Kernel.Get<Settings.Settings>();

        public IObjectsRepository Repository => NinjectCommon.Kernel.Get<IObjectsRepository>();

        public Task CreateAsync(Guid parentId, params object[] objects)
        {
            return Task.Factory.StartNew(() =>
            {
                if (objects == null || !objects.Any())
                    return;

                CreateRecursive(objects.Select(x => x as ProjectSection).ToList(), parentId);

                _modifer?.Apply();
            });
        }

        public void CreateObject(ProjectSection obj)
        {
            if (Create(obj) != null)
                _modifer?.Apply();
        }

        public void DeleteObject(ProjectSection obj)
        {
            _modifer?.DeleteById(obj.Id);
            _modifer?.Apply();
        }

        public void EditObject(ProjectSection obj)
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

        private void SetAttribute(IObjectBuilder builder, IDictionary<string, object> attributes)
        {
            if (attributes.Any())
            {
                foreach (var attribute in attributes)
                {
                    try
                    {
                        builder.SetAttribute(attribute.Key, attribute.Value.ToString());
                    }
                    catch (Exception) { }
                }
            }
        }

        private void SetFiles(IObjectBuilder builder, List<IFile> files)
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
                    catch (Exception) { }
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
                        if (access.Access.AccessLevel != AccessLevel.None)
                        {
                            builder.SetAccessRights(access.OrgUnitId, access.Access.AccessLevel, access.Access.ValidThrough, access.Access.IsInheritable);
                        }
                        else
                        {
                            builder.RemoveAccessRights(access.OrgUnitId);
                        }
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
                if (Create(child) != null)
                    CreateRecursive(objects, child.Id);
            }
        }

        private IObjectBuilder Create(ProjectSection obj)
        {
            var builder = _modifer?.CreateById(obj.Id, obj.ParentId, Repository.GetType(obj.Type.Id));

            if (builder != null)
            {
                SetAttribute(builder, obj.Attributes);
                SetFiles(builder, obj.Files);
                SetAccess(builder, obj.Access);
            }

            return builder;
        }
    }
}
