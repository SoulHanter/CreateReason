using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ascon.ManagerEdition.Wizard.Settings
{
    public class SettingsFactory : ISettingsFactory
    {
        private readonly string _settingsPath = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/settings.json";

        public Settings Create()
        {
            return new Settings();
        }

        public Settings Read()
        {
            Settings settings = new Settings();

            if (!File.Exists(_settingsPath))
            {
                using (var file = File.Create(_settingsPath))
                settings.Default();
                Save(settings);
                return settings;
            }
            else
            {
                settings = JObject.Parse(
                    File.ReadAllText(_settingsPath)
                ).ToObject<Settings>();
                return settings;
            }
        }

        public void Save(Settings settings)
        {
            try
            {
                File.WriteAllText(_settingsPath, JObject.FromObject(settings).ToString());
            }
            catch { }
        }
    }
}
