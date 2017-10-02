using Ascon.ManagerEdition.Wizard.Models;
using Ascon.ManagerEdition.Wizard.Utils.Storage.StorageInterface;
using System;
using System.Collections.Generic;

namespace Ascon.ManagerEdition.Wizard.Utils.Storage.StorageImplementation
{
    public class RemarkStorage : IRemarkStorage
    {
        private IBaseObjectStorage _storage { get; set; }

        public RemarkStorage(IBaseObjectStorage storage)
        {
            _storage = storage;
        }

        public void CreateRemark(TableRemarksModel remark)
        {
            var attribute = AppendAttribute(remark);
            attribute.Add(_storage.Settings?.RemarkFile?.CurrentUser, _storage.Repository.GetCurrentPerson().DisplayName);
            attribute.Add(_storage.Settings?.RemarkFile?.DataCreate, DateTime.Now);

            _storage.CreateObject(new ProjectSection
            {
                Id = remark.Id,
                ParentId = remark.ParentId,
                Type = _storage.Repository.GetType(_storage.Settings.RemarkFile.Type).MapToType(),
                Attributes = attribute,
                Access = new List<Pilot.SDK.IAccessRecord>()      
            });   
        }

        public void DeleteRemark(TableRemarksModel remark)
        {
            _storage.DeleteObject(new ProjectSection { Id = remark.Id } );
        }

        public void EditRemark(TableRemarksModel remark)
        {
            _storage.EditObject(new ProjectSection
            {
                Id = remark.Id,
                ParentId = remark.ParentId,
                Type = _storage.Repository.GetType(_storage.Settings.RemarkFile.Type).MapToType(),
                Attributes = AppendAttribute(remark)
            });
        }

        private Dictionary<string, object> AppendAttribute(TableRemarksModel obj)
        {
            var result = new Dictionary<string, object>();

            try
            {
                var attributes = _storage.Settings.RemarkFile;
                result.Add(attributes.FromUser, obj.FromUser ?? "");
                result.Add(attributes.ToUser, obj.ToUser ?? "");
                result.Add(attributes.Description, obj.Description ?? "");
                result.Add(attributes.Enlargement, obj.Enlargement.GetJSon() ?? "");
                result.Add(attributes.Document, obj.Document.GetJSon() ?? "");
                result.Add(attributes.Act, obj.Act.ToString() ?? "");
                result.Add(attributes.Commit, obj.Commit ?? "");
                result.Add(attributes.Statement, obj.Statement ? 1 : 0);
            }
            catch (Exception) { }

            return result;
        }
    }
}
