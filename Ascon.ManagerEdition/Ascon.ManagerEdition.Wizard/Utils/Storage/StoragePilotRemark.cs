using Ascon.ManagerEdition.Common.DICommon;
using Ascon.ManagerEdition.Common.PilotIceCommon.Observers;
using Ascon.ManagerEdition.Wizard.Models;
using Ascon.Pilot.SDK;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Utils.Storage
{
    public class StoragePilotRemark : PilotStorageBase
    {
        private string _projectName = null;

        public List<ProjectSection> documents = new List<ProjectSection>();
        public ObservableCollection<TableRemarksModel> remarks = new ObservableCollection<TableRemarksModel>();

        public ProjectSection parentFolderRemark = null;

        public string ProjectName
        {
            get => _projectName;
            private set
            {
                _projectName = value;
                NotifyPropertyChanged(nameof(ProjectName));
            }
        }
        
        public List<TableRemarksModel> SortRemark(params object[] parametrs)
        {
            var param = parametrs.Select(x => x?.ToString()).ToArray();

            if (!param.Where(x => x != null).Any() || !remarks.Any())
                return remarks.Select(x => x.ChangeVisible(true)).ToList();            

            IEnumerable<TableRemarksModel> result =
                from remark in remarks
                where (param[0] != null && remark.FromUser.Equals(param[0]) || param[0] == null) &&
                      (param[1] != null && remark.ToUser.Equals(param[1]) || param[1] == null)
                select remark.ChangeVisible(true);

            var items = remarks.Select(x => x.ChangeVisible(false)).ToList();

            items.RemoveAll(x => result.Select(id => id.Id).Contains(x.Id));
            items.AddRange(result);

            return items;
        }

        public StoragePilotRemark()
        {
            InitializeNameProject();
            InitializeDocument();
        }

        private void InitializeDocument()
        {
            NinjectCommon.Kernel.Get<IObjectsLoader>("child").Load(objects =>
            {
               documents.AddRange(objects.Where(x => x.ObjectStateInfo.State == ObjectState.Alive &&
                                                     x.Type.Name == Storage.Settings?.DocumentForRedaction?.Type).Select(d => d.MapToProjectSection()));

            }, type => true,
               _currentObject.Id);
        }

        private void InitializeNameProject()
        {
            NinjectCommon.Kernel.Get<IObjectsLoader>("parent").Load(parent =>
            {
                if (parent.Any())
                {
                    var project = parent.First();

                    NinjectCommon.Kernel.Get<IObjectsLoader>("child").Load(objects =>
                    {
                        var stage = objects.Where(x => x.Id == _currentObject.ParentId).First();

                        var project_code = project.Attributes.ContainsKey(Storage.Settings?.Project?.AttributeCipher ?? "") ?
                                           project.Attributes[Storage.Settings.Project.AttributeCipher] :
                                           "";

                        ProjectName = $"Проект {{{project_code}}} - {{{stage.Type.Title}}}";

                        if (!ExistsRemarkFolder(_currentObject))
                        {
                            InitializeFolderRemarks(objects.Where(x => x.Id == project.Id ||
                                                                       x.Id == _currentObject.ParentId ||
                                                                       x.Id == _currentObject.Id).Select(x => x.MapToProjectSection()).ToList());
                        }
                        else
                        {
                            parentFolderRemark = _folderRemark;
                            InitializeFileRemarks(parentFolderRemark.Children.ToArray());
                        }


                    }, type => true,
                    project.Id);
                }

            }, type => type.Id == Storage.Repository.GetType(Storage.Settings?.Project?.Type).Id,
               _currentObject.Id);
        }

        private void InitializeFolderRemarks(List<ProjectSection> items)
        {
            NinjectCommon.Kernel.Get<SearchObjects>().Search(remarkFolder =>
            {
                if (remarkFolder.Any())
                {
                    NinjectCommon.Kernel.Get<InfoLoader>().Load(objects =>
                    {
                        var folders = objects.Where(o => o.ObjectStateInfo.State == ObjectState.Alive).Select(x => x.MapToProjectSection()).ToList();

                        parentFolderRemark = SearchOrCreateRemark(items, folders);

                        //подгружаем замечания
                        if (parentFolderRemark != null)
                            InitializeFileRemarks(parentFolderRemark.Children.ToArray());

                    }, remarkFolder.ToArray());
                }
                else
                {
                    parentFolderRemark = SearchOrCreateRemark(items, new List<ProjectSection>());
                }

            }, NinjectCommon.Kernel.Get<IObjectsRepository>().GetType(Storage.Settings?.RemarkFolder?.Type).Id, "");
        }

        private void InitializeFileRemarks(params Guid[] children)
        {
            if (!children.Any())
                return;

            NinjectCommon.Kernel.Get<InfoLoader>().Load(objects =>
            {
                var items = objects.Where(o => o.ObjectStateInfo.State == ObjectState.Alive &&
                                               o.Type.Name.Equals(Storage.Settings?.RemarkFile?.Type)).Select(x => x.MapToProjectSection()).ToList();
                if (!items.Any())
                    return;

                foreach (var item in items)
                {
                    var obj = item.Attributes.MapToRemark(item, Storage.Settings?.RemarkFile);
                    obj.Number = remarks.Count + 1;
                    remarks.Add(obj);
                }      
            }, children);
        }

        private ProjectSection SearchOrCreateRemark(List<ProjectSection> items, List<ProjectSection> remarks)
        {
            List<RemarkFolderObject> remarkObject = new List<RemarkFolderObject>()
            {
                new RemarkFolderObject{ parentObject = new ProjectSection() { Id = SystemObjectIds.RootObjectId, Name = Storage.Settings?.NameRootRemarkFolder }, Index = 0, Object = null },
                new RemarkFolderObject{ parentObject = items.FirstOrDefault(x => x.Type.Id == Storage.Repository.GetType(Storage.Settings?.Project.Type).Id), Index = 1, Object = null  },
                new RemarkFolderObject{ parentObject = items.FirstOrDefault(x => x.Id == _currentObject.ParentId), Index = 2, Object = null },
                new RemarkFolderObject{ parentObject = items.FirstOrDefault(x => x.Id == _currentObject.Id), Index = 3, Object = null }
            };

            foreach (var remark in remarkObject.OrderBy(x => x.Index))
            {
                var obj = ExistRemark(remark, remarks, remarkObject);
                remark.Object = obj;
            }

            return remarkObject.FirstOrDefault(x => x.Index == 3).Object;
        }

        private ProjectSection ExistRemark(RemarkFolderObject item, List<ProjectSection> items, List<RemarkFolderObject> remarkObject)
        {
            ProjectSection obj = items.FirstOrDefault(x => x.Attributes.ContainsKey(Storage.Settings?.RemarkFolder?.AttributeName) &&
                                                           x.Attributes.ContainsKey(Storage.Settings?.RemarkFolder?.AttributeCipher) &&
                                                           x.Attributes[Storage.Settings.RemarkFolder.AttributeName].Equals(item.parentObject.Name) &&
                                                           x.Attributes[Storage.Settings.RemarkFolder.AttributeCipher].Equals(item.parentObject.Id.ToString()));
            if (obj != null)
                return obj;

            else
            {
                var parent = remarkObject.FirstOrDefault(x => x.Index == item.Index - 1);

                if (parent != null)
                {
                    obj = NewObject(Storage.Repository.GetType(Storage.Settings?.RemarkFolder?.Type).MapToType(), parent.Object.Id, item.parentObject);
                }
                else
                {
                    obj = NewObject(Storage.Repository.GetType(Storage.Settings?.RemarkFolder?.Type).MapToType(), SystemObjectIds.RootObjectId, item.parentObject);
                }

                return obj;
            }
        }

        private ProjectSection NewObject(Types type, Guid parentId, ProjectSection item)
        {
            Dictionary<string, object> attributes = new Dictionary<string, object>();

            ProjectSection obj = new ProjectSection() { Type = type, ParentId = parentId };

            if (item != null)
            {
                attributes.Add(Storage.Settings?.RemarkFolder?.AttributeName, item.Name);
                attributes.Add(Storage.Settings?.RemarkFolder?.AttributeCipher, item.Id);
                obj.Attributes = attributes;
            }

            Storage.CreateObject(obj);

            return obj;
        }

    }
}
