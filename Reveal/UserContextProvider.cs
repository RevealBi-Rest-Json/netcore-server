using Reveal.Sdk;
namespace RevealSdk.Server.Reveal
{
    public class UserContextProvider : IRVUserContextProvider
    {
        IRVUserContext IRVUserContextProvider.GetUserContext(HttpContext aspnetContext)
        {
            // optionally use this if you are passing it
            var userIdentityName = aspnetContext.User.Identity.Name;

            var userId =
                    !string.IsNullOrEmpty(userIdentityName) ? userIdentityName :
                    !string.IsNullOrEmpty(aspnetContext.Request.Headers["x-header-one"]) ? 
                    aspnetContext.Request.Headers["x-header-one"] : "ALFKI";

            // You can pass a dictionary object w/ additional properties,
            // do a look up, or whatever you need to get more paramters needed for queries
            var props = new Dictionary<string, object>() {                 
                { "CompanyId", "ALFKI" }            
            };
            return new RVUserContext(userId, props);
        }
    }
}