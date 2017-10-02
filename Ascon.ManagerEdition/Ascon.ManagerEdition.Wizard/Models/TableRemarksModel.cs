using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Models
{
    public class TableRemarksModel: ICloneable
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public int UserId { get; set; }
        public DateTime DataCreate { get; set; }

        public int Number { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string Description { get; set; }

        public Documents Enlargement { get; set; } = new Documents { DocumentList = new List<Document>() };
        public Documents Document { get; set; } = new Documents { DocumentList = new List<Document>() };

        public Acts Act { get; set; }
        public string Commit { get; set; }
        public bool Statement { get; set; }
        public ColorsRow Color { get; set; }


        public bool Visible { get; set; } = true;

        public TableRemarksModel()
        {
            Id = Guid.NewGuid();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
