using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascon.ManagerEdition.Wizard.Settings
{
    public interface ISettingsFactory
    {      
        Settings Create();
        
        Settings Read();

        void Save(Settings settings);
    }
}
