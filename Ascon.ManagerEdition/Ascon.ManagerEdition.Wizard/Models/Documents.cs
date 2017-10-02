using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Models
{
    public class Documents
    {
        public List<Document> DocumentList { get; set; }

        public string GetJSon() => JObject.FromObject(this).ToString();
    }
}
