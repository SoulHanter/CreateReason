using Ascon.ManagerEdition.Common.DICommon;
using Ascon.ManagerEdition.Common.PilotIceCommon.Observers;
using Ascon.ManagerEdition.Wizard.Models;
using Ascon.ManagerEdition.Wizard.Utils.Storage;
using Ascon.ManagerEdition.Wizard.ViewModel;
using Ascon.Pilot.SDK;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Utils
{
    public class RemarkRepository : RemarkStorage1
    {
        //private string _nameProject;
        //private Action _close;

        //public ProjectSection parentFolderRemark = null;
        
        //private IObjectsRepository _repository = NinjectCommon.Kernel.Get<IObjectsRepository>();

        //public ProjectSection _currentObject = NinjectCommon.Kernel.Get<IDataObject>().MapToProjectSection();
        //public ObservableCollection<TableRemarksModel> _remarks = new ObservableCollection<TableRemarksModel>();

        //public List<ProjectSection> _documents = new List<ProjectSection>();

        //public string NameProject
        //{
        //    get => _nameProject;
        //    set
        //    {
        //        _nameProject = value;
        //        NotifyPropertyChanged(nameof(NameProject));
        //    }
        ////}
        //public Action Close
        //{
        //    get { return _close; }
        //    set
        //    {
        //        _close = value;
        //        NotifyPropertyChanged(nameof(Close));
        //    }
        //}

        //public void InitializeNameProject()
        //{
        //    NinjectCommon.Kernel.Get<IObjectsLoader>("parent").Load(parent =>
        //    {
        //        if (parent.Any())
        //        {
        //            var project = parent.First();

        //            NinjectCommon.Kernel.Get<IObjectsLoader>("child").Load(objects =>
        //            {
        //                var stage = objects.Where(x => x.Id == _currentObject.ParentId).First();
        //                var project_code = project.Attributes[_settings?.Project?.AttributeCipher ?? ""];

        //                NameProject = $"Проект {{{project_code}}} - {{{stage.Type.Title}}}";

        //                InitializeFolderRemarks(objects.Where(x => x.Id == project.Id ||
        //                                                           x.Id == _currentObject.ParentId ||
        //                                                           x.Id == _currentObject.Id).Select(x => x.MapToProjectSection()).ToList());

        //            }, type => true,
        //            project.Id);
        //        }

        //    }, type => type.Id == _repository.GetType(_settings.Project.Type).Id,
        //       _currentObject.Id);
        //}

        //private void InitializeFolderRemarks(List<ProjectSection> items)
        //{
        //    NinjectCommon.Kernel.Get<SearchObjects>().Search(remarkFolder =>
        //    {
        //        if (remarkFolder.Any())
        //        {
        //            NinjectCommon.Kernel.Get<InfoLoader>().Load(objects =>
        //            {
        //                var folders = objects.Where(o => o.ObjectStateInfo.State == ObjectState.Alive).Select(x => x.MapToProjectSection()).ToList();

        //                parentFolderRemark = SearchOrCreateRemark(items, folders);

        //                //подгружаем замечания
        //                if (parentFolderRemark != null)
        //                    InitializeFileRemarks();

        //            }, remarkFolder.ToArray());
        //        }
        //        else
        //        {
        //            parentFolderRemark = SearchOrCreateRemark(items, new List<ProjectSection>());
        //        }

        //    }, NinjectCommon.Kernel.Get<IObjectsRepository>().GetType(_settings.RemarkFolder.Type).Id);
        //}

        //private void InitializeFileRemarks()
        //{
        //    NinjectCommon.Kernel.Get<IObjectsLoader>("child").Load(objects =>
        //    {
        //        var remarkFiles = objects.Where(o => o.ObjectStateInfo.State == ObjectState.Alive &&
        //                                            o.Type.Name.Equals(_settings.RemarkFile.Type));

        //        if (!remarkFiles.Any())
        //            return;

        //        foreach (var remarkFile in remarkFiles)
        //        {
        //            var obj = remarkFile.Attributes.MapToRemark(remarkFile.Id, remarkFile.ParentId, _settings.RemarkFile);
        //            obj.Number = _remarks.Count + 1;
        //           _remarks.Add(obj);
        //        }

        //    }, type => type.Id == _repository.GetType(_settings.RemarkFile.Type).Id,
        //    parentFolderRemark.Id);
        //}

        //public void InitializeDocument()
        //{
        //    NinjectCommon.Kernel.Get<IObjectsLoader>("child").Load(objects =>
        //    {
        //        _documents.AddRange(objects.Where(x => x.ObjectStateInfo.State == ObjectState.Alive &&
        //                                               x.Type.Name == _settings.DocumentForRedaction.Type).Select(d => d.MapToProjectSection()));

        //    }, type => true,
        //       _currentObject.Id);
        //}

        //private ProjectSection SearchOrCreateRemark(List<ProjectSection> items, List<ProjectSection> remarks)
        //{
        //    List<RemarkFolderObject> remarkObject = new List<RemarkFolderObject>()
        //    {

        //        new RemarkFolderObject{ parentObject = new ProjectSection() { Id = SystemObjectIds.RootObjectId, Name = _settings.NameRootRemarkFolder }, Index = 0, Object = null },
        //        new RemarkFolderObject{ parentObject = items.FirstOrDefault(x => x.Type == _repository.GetType(_settings.Project.Type).Id), Index = 1, Object = null  },
        //        new RemarkFolderObject{ parentObject = items.FirstOrDefault(x => x.Id == _currentObject.ParentId), Index = 2, Object = null },
        //        new RemarkFolderObject{ parentObject = items.FirstOrDefault(x => x.Id == _currentObject.Id), Index = 3, Object = null }
        //    };

        //    foreach (var remark in remarkObject.OrderBy(x => x.Index))
        //    {
        //        var obj = ExistRemark(remark, remarks, remarkObject);
        //        remark.Object = obj;
        //    }

        //    return remarkObject.FirstOrDefault(x => x.Index == 3).Object ?? null;
        //}

        //private ProjectSection ExistRemark(RemarkFolderObject item, List<ProjectSection> items, List<RemarkFolderObject> remarkObject)
        //{
        //    ProjectSection obj = items.FirstOrDefault(x => x.Attributes.ContainsKey(_settings.RemarkFolder.AttributeName) &&
        //                                                   x.Attributes.ContainsKey(_settings.RemarkFolder.AttributeCipher) &&
        //                                                   x.Attributes[_settings.RemarkFolder.AttributeName].Equals(item.parentObject.Name) &&
        //                                                   x.Attributes[_settings.RemarkFolder.AttributeCipher].Equals(item.parentObject.Id.ToString()));
        //    if (obj != null)
        //        return obj;
        //    else
        //    {
        //        var parent = remarkObject.FirstOrDefault(x => x.Index == item.Index - 1);

        //        if (parent != null)
        //        {
        //            obj = NewObject(_repository.GetType(_settings.RemarkFolder.Type).Id, parent.Object.Id, item.parentObject);
        //        }
        //        else
        //        {
        //            obj = NewObject(_repository.GetType(_settings.RemarkFolder.Type).Id, SystemObjectIds.RootObjectId, item.parentObject);
        //        }

        //        return obj;
        //    }
        //}

        //private ProjectSection NewObject(int type, Guid parentId, ProjectSection item)
        //{
        //    Dictionary<string, object> attributes = new Dictionary<string, object>();

        //    ProjectSection obj = new ProjectSection() { Type = type, ParentId = parentId };

        //    if (item != null)
        //    {
        //        attributes.Add(_settings.RemarkFolder.AttributeName, item.Name);
        //        attributes.Add(_settings.RemarkFolder.AttributeCipher, item.Id);
        //        obj.Attributes = attributes;
        //    }

        //    _pilotRepository.CreateAsync(obj);

        //    return obj;
        //}

        //public void EditDocument(Acts act, string link)
        //{
        //    if (string.IsNullOrEmpty(link))
        //        return;

        //    NinjectCommon.Kernel.Get<InfoLoader>().Load(objects =>
        //    {
        //        var doc = objects?.Where(o => o?.ObjectStateInfo?.State == ObjectState.Alive)?.Select(x => x?.MapToProjectSection())?.First();

        //        if (doc == null)
        //            return;

        //        var searchDoc = _documents.FirstOrDefault(x => x.Attributes.ContainsKey(_settings.DocumentForRedaction.AttributeCipher) &&
        //                                                       doc.Attributes.ContainsKey(_settings.DocumentForRedaction.AttributeCipher) &&
        //                                                       x.Attributes[_settings.DocumentForRedaction.AttributeCipher].Equals(doc.
        //                                                         Attributes[_settings.DocumentForRedaction.AttributeCipher]));
        //        if (act == Acts.FINALIZE)
        //        {
        //            if (searchDoc != null)
        //            {
        //                var pilotRepository = NinjectCommon.Kernel.Get<PilotRepository>();

        //                string[] orgUnits = { _settings.GroupGipName, _settings.GroupDesignerName };

        //                foreach (var item in orgUnits)
        //                {
        //                    var access = pilotRepository.GetAccessRecord(item, AccessLevel.Edit);
        //                    pilotRepository.ChangedAccess(searchDoc.Id, access);
        //                }
        //            }
        //        }
        //        else
        //        {

        //            if (searchDoc != null)
        //                DelObj(searchDoc);
        //        }

        //    }, GetIdFromLink(link));
        //}

        //public void EditDocument(TableRemarksModel item)
        //{
        //    if (string.IsNullOrEmpty(item.Document))
        //        return;

        //    NinjectCommon.Kernel.Get<InfoLoader>().Load(objects =>
        //    {
        //        var doc = objects.Where(o => o.ObjectStateInfo.State == ObjectState.Alive).Select(x => x.MapToProjectSection())?.First();

        //        var searchDoc = _documents.FirstOrDefault(x => x.Attributes.ContainsKey(_settings.DocumentForRedaction.AttributeCipher) &&
        //                                                       doc.Attributes.ContainsKey(_settings.DocumentForRedaction.AttributeCipher) &&
        //                                                       x.Attributes[_settings.DocumentForRedaction.AttributeCipher].Equals(doc.
        //                                                         Attributes[_settings.DocumentForRedaction.AttributeCipher]));

        //        if (item.Act == Acts.FINALIZE)
        //        {
        //            if (searchDoc != null)
        //            {
        //                DelObj(searchDoc);
        //                searchDoc.ParentId = _currentObject.Id;

        //            }

        //        }

        //    }, GetIdFromLink(item.Document));

        //}

        //public void CreateDocument(ProjectSection doc)
        //{
        //    AddObj(doc);

        //    var pilotRepository = NinjectCommon.Kernel.Get<PilotRepository>();

        //    string[] orgUnits = { _settings.GroupGipName, _settings.GroupDesignerName };

        //    foreach (var item in orgUnits)
        //    {
        //        var access = pilotRepository.GetAccessRecord(item, AccessLevel.Edit);
        //        pilotRepository.ChangedAccess(doc.Id, access);
        //    }
        //}

        //public bool IsGIP()
        //{
        //    var gips = _repository.GetOrganisationUnits().FirstOrDefault(x => x.Title.Equals(_settings.GroupGipName))?.Children;

        //    if (gips == null)
        //        return false;

        //    return gips.Where(x => _repository.GetCurrentPerson().Positions.Select(p => p.Position)
        //                                                         .Contains(x)).Any();    
        //}

    }
}
