using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ascon.ManagerEdition.Wizard.Models
{
    public class Types : IType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public int Sort { get; set; }

        public bool HasFiles { get; set; }

        public ReadOnlyCollection<int> Children { get; set; }

        public ReadOnlyCollection<IAttribute> Attributes { get; set; }

        public IEnumerable<IAttribute> DisplayAttributes { get; set; }        

        public bool IsDeleted { get; set; }

        public bool IsService { get; set; }

        public bool IsProject { get; set; }

        public byte[] SvgIcon { get; set; }

        public bool IsMountable { get; set; }

        public TypeKind Kind { get; set; }
    }
}
