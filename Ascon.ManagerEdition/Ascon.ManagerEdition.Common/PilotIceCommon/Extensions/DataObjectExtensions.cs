using System;
using System.Collections.Generic;
using System.Linq;
using Ascon.Pilot.SDK;
using Ascon.ProjectWizard.Common.DICommon;
using Ninject;

namespace Ascon.ProjectWizard.Common.PilotIceCommon.Extensions
{
    public static class DataObjectExtensions
    {
        #region Private
        private static IObjectsRepository _repository = NinjectCommon.Kernel.Get<IObjectsRepository>();
        private static IFileProvider _fileProvider = NinjectCommon.Kernel.Get<IFileProvider>();

        private static IEnumerable<IType> TypeAnalizes(IType type)
        {
            List<IType> types = new List<IType>();

            types.Add(type);
            types.Add(_repository.GetTypes().FirstOrDefault(x => x.Name.Contains("Shortcut")));

            var ts = type.Children.Select(x => _repository?.GetType(x)).Where(x => types.All(t => t.Id != x.Id)).ToList();

            types.AddRange(ts);

            foreach (var _type in ts)
            {
                types.AddRange(TypeAnalizes(_type));
            }

            return types.Where(x => x.Name != "document_pdf");
        }

        #endregion

        public static IList<IType> DependentTypes(this IDataObject source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var repo = NinjectCommon.Kernel.Get<IObjectsRepository>();
            //Alternative Distinct
            return TypeAnalizes(source.Type).GroupBy(x => x.Name).Select(x => x.First()).ToList();
        }
    }
}
