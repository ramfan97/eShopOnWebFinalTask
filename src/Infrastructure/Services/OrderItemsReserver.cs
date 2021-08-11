using Azure.Messaging.ServiceBus;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Services
{
    public class OrderItemsReserver : IOrderItemsReserver
    {
        private readonly OrderItemsReserverSettings _orderItemsReserver;

        public OrderItemsReserver(OrderItemsReserverSettings orderItemsReserver)
        {
            _orderItemsReserver = orderItemsReserver;
        }

        public async Task Send(IAggregateRoot entity)
        {
            var client = new ServiceBusClient(_orderItemsReserver.ServiceBusConnectionString);
            var sender = client.CreateSender(_orderItemsReserver.QueueName);

            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            string json = JsonConvert.SerializeObject(entity);

            try
            {
                if(messageBatch.TryAddMessage(new ServiceBusMessage(json)))
                {
                    await sender.SendMessagesAsync(messageBatch);
                }
            }
            finally
            {
                await client.DisposeAsync();
                await sender.DisposeAsync();
            }
            
        }
    }
}
