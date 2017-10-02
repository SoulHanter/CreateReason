using Ascon.ManagerEdition.Wizard.Models;
using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascon.ManagerEdition.Wizard.Utils.Storage
{
    public class StoragePilotRedaction: PilotStorageBase
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
                var groups = Storage.Settings.Subdivisions.Distinct().ToList();
                for (int i = 0; i < groups.Count; i++)
                {
                    accessRecord.Add(GetAccessRecord(groups[i], AccessLevel.View));
                }
                obj.Access = accessRecord;

                //Storage.CreateAsync(obj.ParentId, obj).Wait();                

                return obj;
            }
            catch (Exception) { return null; }
        }
    }
}
