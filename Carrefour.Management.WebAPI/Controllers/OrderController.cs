using Carrefour.Management.Application.OrderApplication;
using Carrefour.Management.Application.OrderApplication.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Carrefour.Management.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> CreateNewOrder([FromBody] NewOrderDTO newOrderDTO)
        {
            try
            {
                return Ok(await _orderService.NewOrder(newOrderDTO));
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}