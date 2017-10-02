using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using SharpVectors.Renderers.Wpf;
using SharpVectors.Converters;
using System.IO;

namespace Ascon.ProjectWizard.Common.MVVMCommon.Converters
{
    class SvgConverterHelper
    {
        public static bool TryConvert(byte[] bytes, out DrawingGroup drawing)
        {
            drawing = null;
            if (bytes == null || bytes.Length == 0)
                return false;

            using (var stream = new MemoryStream(bytes))
            {
                try
                {
                    var settings = new WpfDrawingSettings
                    {
                        IncludeRuntime = true,
                        TextAsGeometry = false
                    };

                    var converter = new FileSvgReader(settings);
                    drawing = converter.Read(stream);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
