using Ascon.ManagerEdition.Wizard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Converter
{
    public static class StringToAct
    {
        public static Acts MapToAct(this string act)
        {
            switch (act)
            {
                case "FINALIZE": return Acts.FINALIZE;
                case "DEVELOP":  return Acts.DEVELOP;
                case "EXCLUDE":  return Acts.EXCLUDE;
                case "DISMISS":  return Acts.DISMISS;
                default: return Acts.DEVELOP;
            }
        }
    }
}
