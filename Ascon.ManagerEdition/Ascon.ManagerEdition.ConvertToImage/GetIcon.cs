using Ascon.ManagerEdition.ConvertToImage.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Ascon.ManagerEdition.ConvertToImage
{
    public static class GetIcon
    {
        private static string _path = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}";
        private static string _file = "IconBase.json";

        public static BitmapImage Value(string name)
        {
            var fullPath = Path.Combine(_path, _file);
            if (!File.Exists(fullPath))
                return null;

            var icons = JObject.Parse(File.ReadAllText(fullPath)).ToObject<ImageList>();
            var icon = icons.Images.FirstOrDefault(x => x.Title == name);
            

            using (MemoryStream ms = new MemoryStream(icon?.Icon))
            {
                var src = new BitmapImage();
                src.BeginInit();
                src.StreamSource = ms;
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();
                return src;
            }
        }
    }
}
