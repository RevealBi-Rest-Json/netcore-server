using Reveal.Sdk;
using Reveal.Sdk.Data;
using Reveal.Sdk.Data.Json;
using Reveal.Sdk.Data.Rest;
namespace RevealSdk.Server.Reveal

{
    internal class DataSourceProvider : IRVDataSourceProvider
    {
        public Task<RVDataSourceItem> ChangeDataSourceItemAsync(IRVUserContext userContext, string dashboardId, RVDataSourceItem dataSourceItem)
        {
            if (dataSourceItem is RVJsonDataSourceItem jsonDsi && jsonDsi.ResourceItem is RVRESTDataSourceItem restDsi)
            {
                ChangeDataSourceAsync(userContext, restDsi.DataSource);
            }
            else
            {
                ChangeDataSourceAsync(userContext, dataSourceItem.DataSource);
            }

            return Task.FromResult(dataSourceItem);
        }

        public Task<RVDashboardDataSource> ChangeDataSourceAsync(IRVUserContext userContext, RVDashboardDataSource dataSource)
        {
            
            if (dataSource is RVRESTDataSource restDs)
            {

                if (restDs.Id == "Invoices")
                {
                    restDs.Url = "https://northwindcloud.azurewebsites.net/api/invoices/customer/" + userContext.UserId;
                    restDs.UseAnonymousAuthentication = true;
                }

                if (restDs.Id == "SalesByCategory")
                {
                    restDs.Url = "https://excel2json.io/api/share/6e0f06b3-72d3-4fec-7984-08da43f56bb9/";
                    restDs.UseAnonymousAuthentication = true;
                }

                if (restDs.Id == "CustomerOrders")
                { 
                    restDs.Url = "https://northwindcloud.azurewebsites.net/api/customers_orders_min/" + userContext.UserId;
                    restDs.UseAnonymousAuthentication = true;
                }
            }

            return Task.FromResult(dataSource);
        }
    }
}