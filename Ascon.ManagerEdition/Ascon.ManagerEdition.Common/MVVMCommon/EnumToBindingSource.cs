using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace Ascon.ManagerEdition.Common.MVVMCommon
{
    public class EnumToBindingSource : MarkupExtension
    {
        private readonly Type _type;

        public EnumToBindingSource(Type type)
        {
            _type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _type.GetMembers()
                .SelectMany(member => member.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>())
                .Select(x => x.Description)
                .ToList();
        }
    }

}
