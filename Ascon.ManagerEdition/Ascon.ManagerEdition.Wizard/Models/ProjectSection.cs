using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Models
{
    public class ProjectSection : ICloneable
    {
        public ProjectSection()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid ParentId { get; set; }

        public string Name { get; set; }

        public Types Type { get; set; }

        public ReadOnlyCollection<Guid> RelatedSourceFiles { get; set; }

        public List<IFile> Files { get; set; } = new List<IFile>();

        public List<Guid> Children { get; set; } = new List<Guid>();

        public IDictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

        public List<IAccessRecord> Access { get; set; } = new List<IAccessRecord>();

        public int UserId { get; set; }

        public DateTime DataCreate { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
