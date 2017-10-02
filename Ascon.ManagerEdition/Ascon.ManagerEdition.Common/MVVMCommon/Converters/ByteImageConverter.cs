using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using SharpVectors.Runtime;
using System.Windows;

namespace Ascon.ProjectWizard.Common.MVVMCommon.Converters
{
    [ValueConversion(typeof(byte[]), typeof(DrawingImage))]
    public class ByteImageConverter : IValueConverter
    {
        #region IValueConverter Members

        public static DrawingImage Convert(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            DrawingGroup drawing;
            if (!SvgConverterHelper.TryConvert(bytes, out drawing))
                return null;

            var rect = GetDrawingRect(drawing);

            DrawingBrush brush = new DrawingBrush(drawing)
            {
                Stretch = Stretch.None,
                Viewbox = rect,
                ViewboxUnits = BrushMappingMode.Absolute
            };

            DrawingImage image = new DrawingImage(new GeometryDrawing
            {
                Brush = brush,
                Geometry = new RectangleGeometry(rect)
            });

            return image;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(ImageSource))
                throw new InvalidOperationException("The target must be ImageSource or derived types");

            var key = value as byte[];
            if (key != null)
            {
                return Convert(key);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        static Rect GetDrawingRect(DrawingGroup rootGroup)
        {
            var result = GetDrawingRectRecursive(rootGroup);
            if (!result.HasValue)
                throw new InvalidOperationException("Ошибка при попытке получить размеры drawing group");

            return result.Value;
        }

        static Rect? GetDrawingRectRecursive(DrawingGroup group)
        {
            var key = SvgLink.GetKey(group);
            if (key == SvgObject.DrawLayer)
            {
                var geometry = group.ClipGeometry as RectangleGeometry;
                if (geometry != null)
                {
                    return geometry.Bounds;
                }
            }
            foreach (var childGroup in group.Children.OfType<DrawingGroup>())
            {
                var res = GetDrawingRectRecursive(childGroup);
                if (res != null)
                {
                    return res;
                }
            }
            return null;
        }
    }

}
