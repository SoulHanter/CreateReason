using Ascon.ManagerEdition.Wizard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascon.ManagerEdition.Wizard.Utils.Storage.StorageClass
{
    public interface ICreator
    {
        Task CreateAsync(List<ProjectSection> objects, Guid parentId);
    }
}
