using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IOrderItemsReserver
    {
        Task Send(IAggregateRoot entity);
    }
}
