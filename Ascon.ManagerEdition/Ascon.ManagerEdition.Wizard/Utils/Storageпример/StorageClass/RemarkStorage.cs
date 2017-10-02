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

namespace Ascon.ManagerEdition.Wizard.Utils.Storage.StorageClass
{
    public class RemarkStorage : IRemarkStorage, INotifyPropertyChanged
    {
        private IObjectModifier _modifer => NinjectCommon.Kernel.Get<IObjectModifier>();

        private IObjectsRepository _repository => NinjectCommon.Kernel.Get<IObjectsRepository>();

        public Settings.Settings _settings = NinjectCommon.Kernel.Get<Settings.Settings>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void CreateRemark(TableRemarksModel obj)
        {
            var builder = _modifer?.CreateById(obj.Id, obj.ParentId, _repository.GetType(_settings.RemarkFile.Type));
            if (builder != null)
            {
                AppendAttribute(obj, builder);
                builder.SetAttribute(_settings.RemarkFile.CurrentUser, _repository.GetCurrentPerson().DisplayName);
                builder.SetAttribute(_settings.RemarkFile.DataCreate, DateTime.Now);
            }
            _modifer?.Apply();
        }

        public void DeleteRemark(TableRemarksModel obj)
        {
            _modifer?.DeleteById(obj.Id);
            _modifer?.Apply();
        }

        public void EditRemark(TableRemarksModel obj)
        {
            var builder = _modifer?.EditById(obj.Id);
            if (builder != null)
            {
                AppendAttribute(obj, builder);
            }
            _modifer?.Apply();
        }

        public ColorsRow SetRemarkColor(TableRemarksModel obj)
        {
            if (obj.Statement)
            {
                switch (obj.Act)
                {
                    case Acts.FINALIZE:
                        return ColorsRow.GREEN;
                    case Acts.DEVELOP:
                        return ColorsRow.GREEN;
                    case Acts.EXCLUDE:
                        return ColorsRow.RED;
                    case Acts.DISMISS:
                        return ColorsRow.YELLOW;
                    default:
                        return ColorsRow.NONE;
                }
            }
            else
                return ColorsRow.NONE;
        }

        private void AppendAttribute(TableRemarksModel obj, IObjectBuilder builder)
        {
            var attributes = _settings.RemarkFile;

            try
            {
                builder.SetAttribute(attributes.FromUser, obj.FromUser ?? "");
                builder.SetAttribute(attributes.ToUser, obj.ToUser ?? "");
                builder.SetAttribute(attributes.Description, obj.Description ?? "");
                builder.SetAttribute(attributes.Enlargement, obj.Enlargement ?? "");
                builder.SetAttribute(attributes.EnlargementName, obj.EnlargementName ?? "");
                builder.SetAttribute(attributes.Document, obj.Document ?? "");
                builder.SetAttribute(attributes.DocumentName, obj.DocumentName ?? "");
                builder.SetAttribute(attributes.Act, obj.Act.ToString() ?? "");
                builder.SetAttribute(attributes.Commit, obj.Commit ?? "");
                builder.SetAttribute(attributes.Statement, obj.Statement ? 1 : 0);
            }
            catch (Exception)
            {
            }
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
