using AutoMapper;
using Carrefour.Management.Application.Extensions;
using Carrefour.Management.Application.OrderApplication.Models.Dto;
using Carrefour.Management.Application.OrderApplication.Reponses;
using Carrefour.Management.Repository.Entities;
using Carrefour.Management.Repository.Enum;
using Carrefour.Management.Repository.Repository.IRepository;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Carrefour.Management.Application.OrderApplication
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<OrderDTO> _validator;
        public OrderService(IOrderRepository orderRepository, IMapper mapper, IValidator<OrderDTO> validator)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<GetBalanceResponse> GetBalance(DateTime? balanceDate)
        {
            if (!balanceDate.HasValue)
                balanceDate = DateTime.Now;

            var orders = await _orderRepository.GetAllAsync(item => item.CreatedAt.Date == balanceDate.Value.Date);
            if (!orders.Any())
            {
                orders = await _orderRepository.GetAllAsync(item => item.CreatedAt.Date < balanceDate.Value.Date);
                if (orders.Any())
                    return new GetBalanceResponse(); ;

                orders = orders.OrderBy(item => item.CreatedAt).Where(item => item.CreatedAt.Date == orders.LastOrDefault().CreatedAt.Date).ToList();
            }

            var balance = orders.FirstOrDefault(item => item.OrderTypeId == (int)OrderTypeEnum.Balance);
            var response = CalculateOrders(orders, balance.TotalOrder);
            return response;
        }

        public async Task<bool> CreditOrder(OrderDTO orderDTO)
        {
            var validator = await _validator.ValidateAsync(orderDTO);
            if (validator.IsValid is false)
                throw new BadRequestException(validator.Errors);

            if (orderDTO == null)
                throw new BadRequestException("A ordem não contem dados");

            await StartBalance();
            var order = _mapper.Map<Order>(orderDTO);
            order.OrderTypeId = (int)OrderTypeEnum.Credit;
            await _orderRepository.CreateAsync(order);

            return true;
        }

        public async Task<bool> DebitOrder(OrderDTO orderDTO)
        {
            var validator = await _validator.ValidateAsync(orderDTO);
            if (validator.IsValid is false)
                throw new BadRequestException(validator.Errors);

            if (orderDTO == null)
                throw new BadRequestException("A ordem não contem dados");

            await StartBalance();
            var order = _mapper.Map<Order>(orderDTO);
            order.OrderTypeId = (int)OrderTypeEnum.Debt;
            await _orderRepository.CreateAsync(order);

            return true;
        }

        private async Task StartBalance()
        {
            var lastBalanceOrder = await _orderRepository.GetLastAsync(item => item.OrderTypeId == (int)OrderTypeEnum.Balance);

            if (lastBalanceOrder == null)
            {
                var order = _mapper.Map<Order>(new OrderDTO { Description = "Balance", TotalOrder = 0 });
                order.OrderTypeId = (int)OrderTypeEnum.Balance;
                await _orderRepository.CreateAsync(order);
            }
            else
            {
                if (lastBalanceOrder.CreatedAt.Date != DateTime.Now.Date)
                {
                    var ordersToOpenBalance = await _orderRepository.GetAllAsync(item => lastBalanceOrder.CreatedAt <= item.CreatedAt && item.OrderTypeId != (int)OrderTypeEnum.Balance);
                    var totalOrder = CalculateOrders(ordersToOpenBalance, lastBalanceOrder.TotalOrder);
                    var order = _mapper.Map<Order>(new OrderDTO { Description = "Balance", TotalOrder = totalOrder.EndBalance });
                    order.OrderTypeId = (int)OrderTypeEnum.Balance;
                    await _orderRepository.CreateAsync(order);
                }
            }
        }

        private GetBalanceResponse CalculateOrders(List<Order>? ordersToOpenBalance, double startBalanceOrder)
        {
            var result = new GetBalanceResponse();
            result.StartBalance = startBalanceOrder;
            result.TotalDebit = ordersToOpenBalance.Where(item => item.OrderTypeId == (int)OrderTypeEnum.Debt).Sum(item => item.TotalOrder);
            result.TotalCredit = ordersToOpenBalance.Where(item => item.OrderTypeId == (int)OrderTypeEnum.Credit).Sum(item => item.TotalOrder);
            result.EndBalance = result.StartBalance + result.TotalCredit - result.TotalDebit;

            return result;
        }
    }
}
