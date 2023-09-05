using Carrefour.Management.Application.OrderApplication.Models.Dto;

namespace Carrefour.Management.Application.OrderApplication
{
    public interface IOrderService
    {
        public Task<bool> NewOrder(NewOrderDTO cliente);
    }
}
