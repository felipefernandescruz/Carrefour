using AutoMapper;
using Carrefour.Management.Application.Extensions;
using Carrefour.Management.Application.OrderApplication.Models.Dto;
using Carrefour.Management.Repository.Entities;
using Carrefour.Management.Repository.Enum;
using Carrefour.Management.Repository.Repository.IRepository;
using System.Net;

namespace Carrefour.Management.Application.OrderApplication
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<bool> NewOrder(NewOrderDTO orderDTO)
        {

            if (orderDTO == null)
            {
                throw new BadRequestException("A ordem não contem dados");
            }

            await VerifyLastBalance();

            switch (orderDTO.OrderTypeId)
            {
                case (int)OrderTypeEnum.Credit:
                    await _orderRepository.CreateAsync(_mapper.Map<Order>(orderDTO));
                    break;

                case (int)OrderTypeEnum.Debt:
                    await _orderRepository.CreateAsync(_mapper.Map<Order>(orderDTO));
                    break;

                default:
                    throw new BadRequestException("Você inseriu o tipo de ordem inválido");
                    break;
            }
            return true;
        }

        private async Task VerifyLastBalance()
        {
            var balances = await _orderRepository.GetAllAsync(item => item.OrderTypeId == (int)OrderTypeEnum.Balance);

            if (!balances.Any()) { 
                await _orderRepository.CreateAsync(_mapper.Map<Order>(new NewOrderDTO { OrderTypeId = 3, Description="Balance", TotalOrder = 0 }));
            }
            else
            {
                var lastBalanceOrder = balances.OrderBy(item => item.CreatedAt).LastOrDefault();
                if (lastBalanceOrder.CreatedAt.Date != DateTime.Now.Date)
                {
                    var ordersToOpenBalance = await _orderRepository.GetAllAsync(item => lastBalanceOrder.CreatedAt <= item.CreatedAt && item.OrderTypeId != (int)OrderTypeEnum.Balance);
                    var totalDebit = ordersToOpenBalance.Where(item => item.OrderTypeId == (int)OrderTypeEnum.Debt).Sum(item => item.TotalOrder);
                    var totalCredit = ordersToOpenBalance.Where(item => item.OrderTypeId == (int)OrderTypeEnum.Credit).Sum(item => item.TotalOrder);
                    var totalOrder = lastBalanceOrder.TotalOrder + totalCredit - totalDebit;
                    await _orderRepository.CreateAsync(_mapper.Map<Order>(new NewOrderDTO { OrderTypeId = 3, Description = "Balance", TotalOrder = totalOrder }));
                }
            }
        }
    }
}
