using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK;
using Ascon.ManagerEdition.Common.DICommon;
using Ninject;
using Ascon.ManagerEdition.Wizard.Models;
using System.ComponentModel;
using Ascon.ManagerEdition.Common.PilotIceCommon.Observers;

namespace Ascon.ManagerEdition.Wizard.Utils.Storage
{
    public class RemarkStorage1
    {  
        

        public Guid GetIdFromLink(string link)
        {
            Guid id = Guid.Empty;

            if (string.IsNullOrEmpty(link) || link.Length < 47)
                return id;

            link = link.Remove(0, 11);

            if (link.Length > 36)
            {
                link = link.Remove(36);
            }

            id = Guid.Parse(link);

            return id;
        }
        


    }
}
