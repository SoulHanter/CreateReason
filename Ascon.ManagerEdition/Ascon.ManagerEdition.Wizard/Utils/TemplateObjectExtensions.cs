using Ascon.ManagerEdition.Wizard.Models;
using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Utils
{
    public static class TemplateObjectExtensions
    {
        public static List<ProjectSection> ChangedGuid(this List<ProjectSection> source, Guid parentId, Guid parentNewId)
        {           
            ChangeGuid(parentId, parentNewId, source);
            return source;
        }

        private static void ChangeGuid(Guid parentId, Guid parentNewId, List<ProjectSection> objects)
        {
            var children = objects.FindAll(x => x.ParentId == parentId);

            foreach (var child in children)
            {
                Guid oldId = child.Id;
                    child.Id = Guid.NewGuid();
                child.ParentId = parentNewId;
                ChangeGuid(oldId, child.Id, objects);
            }
        }

        public static List<ProjectSection> DeleteAccesses(this List<ProjectSection> source)
        {
            if (source.Any())
            {
                foreach (var item in source)
                {
                    item.Access.Clear();
                }
            }

            return source;
        }
        
    }
}
