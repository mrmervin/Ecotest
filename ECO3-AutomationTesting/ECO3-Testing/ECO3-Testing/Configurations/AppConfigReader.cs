using ECO3_Testing.Interfaces;
using ECO3_Testing.Settings;
using System;
using System.Configuration;

namespace ECO3_Testing.Configurations
{
    public class AppConfigReader : IConfig
    {
        public BrowserType GetBrowserType()
        {
            string browser = ConfigurationManager.AppSettings.Get(AppConfigKeys.Browser);
            return (BrowserType)Enum.Parse(typeof(BrowserType), browser);
        }
        public string InternalQaaUrl => ConfigurationManager.AppSettings.Get(AppConfigKeys.InternalQaaUrl);

        public string ExternalQaaUrl => ConfigurationManager.AppSettings.Get(AppConfigKeys.ExternalQaaUrl);

        public string DevInternalUrl => ConfigurationManager.AppSettings.Get(AppConfigKeys.DevInternalUrl);

        public string ExternalUser => ConfigurationManager.AppSettings.Get(AppConfigKeys.ExternalUser);

        public string GetExternalPassword => ConfigurationManager.AppSettings.Get(AppConfigKeys.ExternalUserPassword);

        public string GetInternalPassword => ConfigurationManager.AppSettings.Get(AppConfigKeys.InternalUserPassword);

        public string GetECOSqlConnection => ConfigurationManager.AppSettings.Get(AppConfigKeys.ECOConnectionString);

        public string GetDocumentsSqlConnection => ConfigurationManager.AppSettings.Get(AppConfigKeys.DocumentsConnectionString);

        public string ChromeSettingsUrl => ConfigurationManager.AppSettings.Get(AppConfigKeys.ChromeSettingsUrl);
        
        public string AdvancedUser => ConfigurationManager.AppSettings.Get(AppConfigKeys.AdvancedUser);

        public string StandardUser => ConfigurationManager.AppSettings.Get(AppConfigKeys.StandardUser);

        public string InternalAdminUser => ConfigurationManager.AppSettings.Get(AppConfigKeys.InternalAdminUser);

        public string BasicUser => ConfigurationManager.AppSettings.Get(AppConfigKeys.BasicUser);
    }
}
