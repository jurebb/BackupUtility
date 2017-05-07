using System.Configuration;

namespace BackupUtilityLib
{
    class BackupSources
    {
        #region fields
        private static string[] _sourceDir = new string[]
        {
            GetConfig("sourceDir1"),
            GetConfig("sourceDir2"),
            GetConfig("sourceDir3")
        };
        private static string[] _schedulers = new string[]
        {
            GetConfig("scheduler1"),
            GetConfig("scheduler2"),
            GetConfig("scheduler3")
        };
        private static int _numOfSources = int.Parse(GetConfig("numOfSources"));
        #endregion

        #region properties
        public static string[] SourceDirs
        {
            get { return _sourceDir;  }
        }

        public static string[] Schedulers
        {
            get { return _schedulers; }
        }

        public static int NumOfSources
        {
            get { return _numOfSources; }
        }
        #endregion

        public static string GetConfig(string mySettingName)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            ClientSettingsSection configSection = config.SectionGroups[@"userSettings"].Sections["AppUI.Properties.Settings"] as ClientSettingsSection;     //nadji sekciju
            string setting = configSection.Settings.Get(mySettingName).Value.ValueXml.InnerText;
            return setting;
        }
    }
}
