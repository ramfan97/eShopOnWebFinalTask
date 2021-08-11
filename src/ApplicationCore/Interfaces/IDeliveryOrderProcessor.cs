using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IDeliveryOrderProcessor
    {
        Task Send(IAggregateRoot entity);
    }
}
