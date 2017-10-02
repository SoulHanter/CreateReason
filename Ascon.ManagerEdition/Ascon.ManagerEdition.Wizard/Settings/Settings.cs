using Ascon.ManagerEdition.Wizard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascon.ManagerEdition.Wizard.Settings
{
    public class Settings
    {
        //стартовые объекты
        public List<PilotObjects> StartTypes { get; set; } = new List<PilotObjects>();

        //группа выдачи прав на просмотр
        public List<string> Subdivisions { get; set; } = new List<string>();

        //от кого
        public List<Users> FromUser { get; set; } = new List<Users>();

        //кому
        public List<Users> ToUser { get; set; } = new List<Users>();

        //объект проект
        public PilotObjects Project { get; set; } = new PilotObjects
        {
            Type = "Project",
            AttributeCipher = "Project_code",
            AttributeName = "Project_name"
        };

        //объект папка замечаний
        public PilotObjects RemarkFolder { get; set; } = new PilotObjects
        {
            Type = "FolderZ",
            AttributeCipher = "GuidRed",
            AttributeName = "Name"
        };

        //замечания
        public RemarkFile RemarkFile { get; set; } = new RemarkFile
        {
            Type = "Z",
            FromUser = "FromUser",
            ToUser = "ToUser",
            Description = "Description",
            Enlargement = "Enlargement",
            Document = "Document",
            Act = "Act",
            Commit = "Commit",
            Statement = "Statement",
            DataCreate = "DataCreate",
            CurrentUser = "CurrentUser"
        };

        //документ редакции
        public PilotObjects DocumentForRedaction { get; set; } = new PilotObjects
        {
            Type = "Document",
            AttributeName = "Name",
            AttributeCipher = "code"
        };

        public string GroupGipName { get; set; } = "Бюро ГИПов";

        public string GroupDesignerName { get; set; } = "СПП Архитектуры";

        public string NameRootRemarkFolder { get; set; } = "Папка с замечаниями";

        public Admin PersonIsAdmin { get; set; } = new Admin
        {
            ServerName = @"http://timur:5545",
            BaseName = "LUKOIL",
            Login = "Timur",
            Password = "G7hisXjAG0sv8kB/BqaUiQ=="
        };

        public void Default()
        {
            this.StartTypes = new List<PilotObjects>
            {
                new PilotObjects() {Type = "RedPD", AttributeCipher = "code", AttributeName = null, GuidNextObject = "nextobject" },
                new PilotObjects() {Type = "RedRD", AttributeCipher = "code", AttributeName = null, GuidNextObject = "nextobject" },
            };
            this.Subdivisions = new List<string>
            {
                "Бюро ГИПов",
                "СПП Архитектуры"
            };
            this.FromUser = new List<Users>
            {
                new Users {Name = "ГИП", FullName = "Задание ГИПа"},
                new Users {Name = "ЭХЗ", FullName = "Электрохимзащита от коррозии"},
                new Users {Name = "А", FullName = "Автоматизация"}
            };
            this.ToUser = new List<Users>
            {
                new Users {Name = "ГИП", FullName = "Задание ГИПа"},
                new Users {Name = "ЭХЗ", FullName = "Электрохимзащита от коррозии"},
                new Users {Name = "А", FullName = "Автоматизация"}
            };
        }
    }
}
