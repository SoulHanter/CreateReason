using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Models
{
    public class RemarkFile
    {
        public string Type { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string Description { get; set; }
        public string Enlargement { get; set; }
        public string EnlargementName { get; set; }
        public string Document { get; set; }
        public string DocumentName { get; set; }
        public string Act { get; set; }
        public string Commit { get; set; }
        public string Statement { get; set; }
        public string DataCreate { get; set; }
        public string CurrentUser { get; set; }
    }
}
