using Ascon.ManagerEdition.Wizard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascon.ManagerEdition.Wizard.Utils.Storage.StorageInterface
{
    public interface ICreator
    {
        Task CreateAsync(Guid parentId, params object[] objects);
    }
}
