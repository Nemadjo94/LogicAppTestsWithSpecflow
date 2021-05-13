using LogicApps.Tests.Utilities;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Rest;
using System.Threading.Tasks;

namespace SpecFlowDemo.Utilities
{
    public class TokenGenerator
    {
        private readonly AzureServiceTokenProvider _azureServiceTokenProvider;

        public TokenGenerator(
            AzureServiceTokenProvider azureServiceTokenProvider)
        {
            _azureServiceTokenProvider = azureServiceTokenProvider;
        }

        public async Task<TokenCredentials> GenerateCredentials()
        {
            string token = await GetAuthorizationToken();
            TokenCredentials credentials = new TokenCredentials(token);
            return credentials;
        }

        public async Task<string> GetAuthorizationToken()
        {
            var resource = @"https://management.azure.com/";
            var accessToken = await _azureServiceTokenProvider.GetAccessTokenAsync(resource, tenantId: EnvironmentVariables.AzureTenantId, forceRefresh: true);

            return accessToken;
        }
    }
}
