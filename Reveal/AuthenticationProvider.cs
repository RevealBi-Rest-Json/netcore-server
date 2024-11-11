using Reveal.Sdk;
using Reveal.Sdk.Data;

namespace RevealSdk.Server.Reveal
{
    public class AuthenticationProvider : IRVAuthenticationProvider
    {
        public Task<IRVDataSourceCredential> ResolveCredentialsAsync(IRVUserContext userContext,
            RVDashboardDataSource dataSource)
        {
            IRVDataSourceCredential userCredential = new RVUsernamePasswordDataSourceCredential();
            // Change to your username / password
            //if (dataSource is RVRESTDataSource)
            //{
            //    userCredential = new RVUsernamePasswordDataSourceCredential
            //        ("username", "password", "domain");
            //}
            return Task.FromResult(userCredential);
        }
    }
}