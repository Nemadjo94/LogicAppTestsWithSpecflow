using System.Collections.Generic;

namespace LogicApps.Tests.Utilities
{
    public static class EnvironmentVariables
    {
        public static string AzureTenantId = "40758481-7365-442c-ae94-563ed1606218";
        public static string SubscriptionId = "e640c96b-ec13-49ca-944e-764ffbec9966";

        public static readonly Dictionary<string, string> ResourceGroups = new Dictionary<string, string>()
        {
            {
                "Create-Customer",
                "azuredemo"
            }
        };

        public static readonly Dictionary<string, string> HttpTriggerUris = new Dictionary<string, string>()
        {
            {
                "Create-Customer",
                "https://prod-49.westeurope.logic.azure.com:443/workflows/8d1c540e02e24feb8918ff3a9a68e5b7/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=7gDy3hhp1QtkhsGetKY6MJrT_l25D-ACJ77TW0c9J40"
            }
        };
    }
}
