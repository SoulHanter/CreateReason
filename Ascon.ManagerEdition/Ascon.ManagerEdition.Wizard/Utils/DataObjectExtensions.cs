using Ascon.ManagerEdition.Common.DICommon;
using Ascon.ManagerEdition.Common.MVVMCommon.Converters;
using Ascon.ManagerEdition.Common.PilotIceCommon.Observers;
using Ascon.ManagerEdition.Wizard.Converter;
using Ascon.ManagerEdition.Wizard.Models;
using Ascon.Pilot.SDK;
using Newtonsoft.Json.Linq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Utils
{
    public static class DataObjectExtensions
    {
        public static ProjectSection MapToProjectSection(this IDataObject source)
        {
            if (source == null)
                return null;

            return new ProjectSection
            {
                Id = source.Id,
                Name = source.DisplayName,
                ParentId = source.ParentId,
                Type = source.Type.MapToType(),
                Attributes = source.Attributes, 
                RelatedSourceFiles = source.RelatedSourceFiles,
                Files = source.Files.ToList(),
                Access = source.Access2.ToList(),
                Children = source.Children.ToList(),
                UserId = source.Creator.Id,
                DataCreate = source.Created
            };
        }

        public static TableRemarksModel MapToRemark(this IDictionary<string, object> source, ProjectSection item, RemarkFile remarkFile)
        {
            TableRemarksModel result = new TableRemarksModel() { Id = item.Id, ParentId = item.ParentId, DataCreate = item.DataCreate, UserId = item.UserId };

            result.FromUser    = source.ContainsKey(remarkFile.FromUser)    ? source[remarkFile.FromUser]?.ToString()    : null;
            result.ToUser      = source.ContainsKey(remarkFile.ToUser)      ? source[remarkFile.ToUser]?.ToString()      : null;
            result.Description = source.ContainsKey(remarkFile.Description) ? source[remarkFile.Description]?.ToString() : null;   
            result.Commit      = source.ContainsKey(remarkFile.Commit)      ? source[remarkFile.Commit]?.ToString()      : null;
            result.Statement   = source.ContainsKey(remarkFile.Statement)   ? int.Parse(source[remarkFile.Statement]?.ToString() ?? "0")  == 1    : false;

            if (source.ContainsKey(remarkFile.Act))
            {
                result.Act = source[remarkFile.Act].ToString().MapToAct();
            }

            if (result.Statement)
            {
                switch (result.Act)
                {
                    case Acts.FINALIZE: result.Color = ColorsRow.GREEN;
                        break;
                    case Acts.DEVELOP: result.Color = ColorsRow.GREEN;
                        break;
                    case Acts.EXCLUDE: result.Color = ColorsRow.RED;
                        break;
                    case Acts.DISMISS: result.Color = ColorsRow.YELLOW;
                        break;
                }
            }
            else
                result.Color = ColorsRow.NONE;            

            result.Enlargement = source.ContainsKey(remarkFile.Enlargement) ?
                                 JObject.Parse(source[remarkFile.Enlargement]?.ToString()).ToObject<Documents>() :
                                 new Documents { DocumentList = new List<Document>() };

            result.Document = source.ContainsKey(remarkFile.Document) ?
                              JObject.Parse(source[remarkFile.Document]?.ToString()).ToObject<Documents>() :
                              new Documents { DocumentList = new List<Document>() };
            return result;
        }

        public static TableRemarksModel ChangeVisible(this TableRemarksModel source, bool result)
        {
            source.Visible = result;
            return source;
        }

        public static Types MapToType(this IType source)
        {
            if (source == null)
                return null;

            return new Types
            {
                Id = source.Id,
                Attributes = source.Attributes,
                Children = source.Children,
                DisplayAttributes = source.DisplayAttributes,
                HasFiles = source.HasFiles,
                IsDeleted = source.IsDeleted,
                IsMountable = source.IsMountable,
                IsProject = source.IsProject,
                IsService = source.IsService,
                Kind = source.Kind,
                Name = source.Name,
                Sort = source.Sort,
                SvgIcon = source.SvgIcon,
                Title = source.Title
            };
        }

    }
}
