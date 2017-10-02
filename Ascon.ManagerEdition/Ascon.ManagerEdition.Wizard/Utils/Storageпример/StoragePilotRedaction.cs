using Ascon.ManagerEdition.Wizard.Models;
using Ascon.ManagerEdition.Wizard.Utils.Storage.StorageClass;
using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascon.ManagerEdition.Wizard.Utils.Storage
{
    public class StoragePilotRedaction : ObjectStorage
    {
        public ProjectSection CreateNewRedaction(ProjectSection currentObject, string cipherName, string cipherValue)
        {
            try
            {
                List<IAccessRecord> accessRecord = new List<IAccessRecord>();

                var obj = currentObject.Clone() as ProjectSection;

                //даем новый id
                obj.Id = Guid.NewGuid();

                //отчищаем атрибуты
                obj.Attributes.Clear();

                //добавляем новый атрибут
                obj.Attributes[cipherName] = cipherValue;                               

                //добавление прав
                var groups = _settings.Subdivisions.Distinct().ToList();
                for (int i = 0; i < groups.Count; i++)
                {
                    accessRecord.Add(GetAccessRecord(groups[i], AccessLevel.View));                    
                }
                obj.Access = accessRecord; 

                CreateObj(obj);
                
                return obj;
            }
            catch (Exception) { return null; }
        }

        public bool ExistsRemark(ProjectSection currentObject)
        {
            var folderRemark = _folderRemark.FirstOrDefault(x => x.Name == currentObject.Name &&
                                                                 x.Attributes.ContainsKey(_settings?.RemarkFolder?.AttributeCipher) &&
                                                                 x.Attributes[_settings.RemarkFolder.AttributeCipher].Equals(currentObject.Id.ToString()));
            return folderRemark?.Children != null ? folderRemark.Children.Any() : false;
        }
    }
}
