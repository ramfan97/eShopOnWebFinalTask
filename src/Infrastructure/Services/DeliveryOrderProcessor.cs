using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Services
{
    public class DeliveryOrderProcessor : IDeliveryOrderProcessor
    {
        private readonly DeliveryOrderProcessorSettings _deliveryOrderProcessorSettings;

        public DeliveryOrderProcessor(DeliveryOrderProcessorSettings deliveryOrderProcessorSettings)
        {
            _deliveryOrderProcessorSettings = deliveryOrderProcessorSettings;
        }

        public async Task Send(IAggregateRoot entity)
        {
            string json = JsonConvert.SerializeObject(entity);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                await client.PostAsync(_deliveryOrderProcessorSettings.OrderProcessorUrl, data);
            }
        }
    }
}
