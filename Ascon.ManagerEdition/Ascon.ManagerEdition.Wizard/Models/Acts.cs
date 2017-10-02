using Ascon.ManagerEdition.Common.MVVMCommon.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Models
{
    [TypeConverter(typeof(EnumToItemSourceConverter))]
    public enum Acts
    {
        [Description("Доработать")]
        FINALIZE,
        [Description("Разработать")]
        DEVELOP,
        [Description("Исключить")]
        EXCLUDE,
        [Description("Отклонить")]
        DISMISS
    }
}
