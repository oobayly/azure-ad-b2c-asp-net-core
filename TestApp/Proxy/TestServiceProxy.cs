using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Experimental.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Extensions.Options;
using TestApp.Infrastructure;

namespace TestApp.Proxy
{
    public class TestServiceProxy
    {
        private readonly B2CAuthenticationOptions authOptions;
        private readonly TestServiceOptions serviceOptions;
        
        public TestServiceProxy(IOptions<B2CAuthenticationOptions> authOptions, IOptions<TestServiceOptions> serviceOptions)
        {
            this.authOptions = authOptions.Value;
            this.serviceOptions = serviceOptions.Value;
        }

        public async Task<string> GetValuesAsync()
        {
            var client = new HttpClient { BaseAddress = new Uri(serviceOptions.BaseUrl, UriKind.Absolute) };
            client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", await GetAccessTokenAsync());

            return await client.GetStringAsync("api/values");
        }

        private async Task<string> GetAccessTokenAsync()
        {
            try
            {
                var credential = new ClientCredential(authOptions.ClientId, authOptions.ClientSecret);
                var authenticationContext = new AuthenticationContext(authOptions.Authority);
                var result = await authenticationContext.AcquireTokenSilentAsync(new[] { authOptions.ClientId }, credential, UserIdentifier.AnyUser);
                return result.Token;
            }
            catch (AdalSilentTokenAcquisitionException)
            {
                throw new ReauthenticationRequiredException();
            }
        }
    }
}