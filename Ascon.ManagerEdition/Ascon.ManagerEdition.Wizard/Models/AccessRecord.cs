using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Models
{
    public class AccessRecord : IAccessRecord
    {
        public int OrgUnitId { get; set; }

        public IAccess Access { get; set; }

        public int RecordOwner { get; set; }

        public Guid InheritanceSource { get; set; }
    }
}
