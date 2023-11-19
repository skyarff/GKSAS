using System;
using System.Configuration;

namespace KP_0_
{
    internal class Tools
    {
        internal static int tabPageIndexOfTables = 0;
        internal static int tabPageIndexOfQueries = 0;


        internal static string[] configValues =
        {
            "Data Source=SERVER;Initial Catalog=DATABASE;Integrated Security=True",
        };

        internal static void ConfigAdd(string key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                try
                {
                    config.AppSettings.Settings[key].Value = value;
                    config.Save(ConfigurationSaveMode.Modified);
                }
                catch
                {
                    AppSettingsSection appSet = config.AppSettings;
                    appSet.Settings.Add(key, value);
                    config.Save(ConfigurationSaveMode.Modified);
                }
            }
            catch { }
        }
        internal static void ConfigRead(string key)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                if (ConfigurationManager.AppSettings[key] != null)
                {
                    configValues[Convert.ToInt32(key)] = ConfigurationManager.AppSettings[key].Replace("\\\\", "\\");
                }
                else ConfigAdd(key, configValues[Convert.ToInt32(key)]);
            }
            catch { }
        }
    }
}
