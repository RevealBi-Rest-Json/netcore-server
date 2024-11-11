using Reveal.Sdk;
using Reveal.Sdk.Data;
using Reveal.Sdk.Data.Json;
using Reveal.Sdk.Data.Rest;
using System.Text.RegularExpressions;
namespace RevealSdk.Server.Reveal

{
    // ****
    // https://help.revealbi.io/web/datasources/
    // The DataSource Provider is required.  
    // Set you REST endpoints in the DataSourceProvider to be used in the dashboard
    // If you are using data source items on the client, or you need to set specific queries based 
    // on incoming table requests, you will handle those requests in the ChangeDataSourceItem.
    // ****


    // ****
    // NOTE:  This must beset in the Builder in Program.cs --> .AddDataSourceProvider<DataSourceProvider>()
    // ****
    internal class DataSourceProvider : IRVDataSourceProvider
    {
        public Task<RVDataSourceItem> ChangeDataSourceItemAsync(IRVUserContext userContext, string dashboardId, RVDataSourceItem dataSourceItem)
        {
            // ****
            // Every request for data passes thru changeDataSourceItem
            // You can set query properties based on the incoming requests
            // 
            // This specific code is required to change the data source for the dashboard when there are parameters
            // passed in from the client.  In this case, the user id is passed in and the data source is changed
            // based on the UserContext.userid used in the DataSource.
            // ****

            ChangeDataSourceAsync(userContext, dataSourceItem.DataSource);

            if (dataSourceItem is RVResourceBasedDataSourceItem resItem)
            {
                ChangeDataSourceItemAsync(userContext, dashboardId, (RVDataSourceItem)resItem.ResourceItem);
            }

            return Task.FromResult(dataSourceItem);
        }

        public Task<RVDashboardDataSource> ChangeDataSourceAsync(IRVUserContext userContext, RVDashboardDataSource dataSource)
        {
            // *****
            // Check the request for the incoming ID of the data source from the client request
            // you can also check the incoming dataSource type or user context to set parameters
            // *****
            string customerId = userContext.UserId;

            if (dataSource is RVRESTDataSource restDs)
            {

                if (restDs.Id == "Invoices")
                {
                    // *****
                    // Example of how to use a parameter
                    // Check the format of the customerId to make sure it is valid
                    // You can perform other checks here or in the UserContextProvider to ensure
                    // that your parameters are valid, or that the expect user is making the request                    
                    // *****
                    if (!IsValidCustomerId(customerId))
                        throw new ArgumentException("Invalid CustomerID format. CustomerID must be a 5-character alphanumeric string.");

                    restDs.Url = $"https://northwindcloud.azurewebsites.net/api/invoices/customer/{customerId}";
                    restDs.UseAnonymousAuthentication = true;

                }

                if (restDs.Id == "SalesByCategory")
                {
                    // *****
                    // Example of a JSON endpoint with no parameters
                    // *****
                    restDs.Url = "https://excel2json.io/api/share/6e0f06b3-72d3-4fec-7984-08da43f56bb9/";
                    restDs.UseAnonymousAuthentication = true;
                }

                if (restDs.Id == "CustomerOrders")
                {
                    // *****
                    // Example of how to use a parameter
                    // Check the format of the customerId to make sure it is valid
                    // You can perform other checks here or in the UserContextProvider to ensure
                    // that your parameters are valid, or that the expect user is making the request
                    // *****
                    if (!IsValidCustomerId(customerId))
                        throw new ArgumentException("Invalid CustomerID format. CustomerID must be a 5-character alphanumeric string.");

                    restDs.Url = $"https://northwindcloud.azurewebsites.net/api/customers_orders_min/{customerId}";
                    restDs.UseAnonymousAuthentication = true;
                }
            }

            return  Task.FromResult(dataSource);
        }

        private static bool IsValidCustomerId(string customerId) => Regex.IsMatch(customerId, @"^[A-Za-z0-9]{5}$");
    }
}