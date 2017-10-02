using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Models
{
    public class Access : IAccess
    {
        public AccessLevel AccessLevel { get; set; } = AccessLevel.None;

        public DateTime ValidThrough { get; set; } = DateTime.MaxValue;

        public bool IsInheritable { get; set; } = true;

        public bool IsInherited { get; set; } = false;
    }
}
