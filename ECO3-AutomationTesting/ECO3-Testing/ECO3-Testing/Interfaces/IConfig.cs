using ECO3_Testing.Configurations;
using System.Data.SqlClient;

namespace ECO3_Testing.Interfaces
{
    public interface IConfig
    {
        BrowserType GetBrowserType();

        string InternalQaaUrl { get; }

        string ExternalQaaUrl { get; }

        string DevInternalUrl { get; }

        string ExternalUser { get; }

        string AdvancedUser { get; }

        string StandardUser { get; }

        string InternalAdminUser { get; }

        string BasicUser { get; }
        
        string GetExternalPassword { get; }

        string GetInternalPassword { get; }

        string GetECOSqlConnection { get; }

        string GetDocumentsSqlConnection { get; }

        string ChromeSettingsUrl { get; }
    }
}
