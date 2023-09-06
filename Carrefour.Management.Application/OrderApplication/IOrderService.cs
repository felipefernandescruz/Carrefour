using Carrefour.Management.Application.OrderApplication.Models.Dto;
using Carrefour.Management.Application.OrderApplication.Reponses;

namespace Carrefour.Management.Application.OrderApplication
{
    public interface IOrderService
    {
        Task<bool> CreditOrder(OrderDTO creditOrderDTO);
        Task<bool> DebitOrder(OrderDTO creditOrderDTO);
        Task<GetBalanceResponse> GetBalance(DateTime? balanceDate);
    }
}
