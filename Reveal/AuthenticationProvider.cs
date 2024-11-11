using Reveal.Sdk;
using Reveal.Sdk.Data;
using Reveal.Sdk.Data.Rest;

namespace RevealSdk.Server.Reveal
{
    // ****
    // https://help.revealbi.io/web/authentication/ 
    // The Authentication Provider is required to set the credentials used
    // in the DataSourceProvider changeDataSourceAsync to authenticate to your database
    //
    // REST data sources support BearerToken, Basic, and Anonymous authentication.
    // https://help.revealbi.io/web/authentication/#bearer-token-authentication
    // ****


    // ****
    // NOTE:  This must beset in the Builder in Program.cs --> .AddAuthenticationProvider<AuthenticationProvider>()
    // ****
    public class AuthenticationProvider : IRVAuthenticationProvider
    {
        public Task<IRVDataSourceCredential> ResolveCredentialsAsync(IRVUserContext userContext,
            RVDashboardDataSource dataSource)
        {
            IRVDataSourceCredential userCredential = new RVUsernamePasswordDataSourceCredential();
            
            // Check that the incoming request is for the expected data source type
            // You can check the data source type, or any of the information in your UserContext to
            // set credentials
            if (dataSource is RVRESTDataSource)
            {
                // for SQL Server, add a username, password and optional domain
                // note these are just properties, you can set them from configuration, a key vault, a look up to 
                // database, etc.  They are hardcoded here for demo purposes.
                userCredential = new RVUsernamePasswordDataSourceCredential
                    ("username", "password", "domain");
            }
            return Task.FromResult(userCredential);
        }
    }
}