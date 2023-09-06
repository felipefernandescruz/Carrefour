using Carrefour.Management.Application.OrderApplication;
using Carrefour.Management.Application.OrderApplication.Models.Dto;
using Carrefour.Management.Application.OrderApplication.Reponses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpPost("CreditOrder")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse<bool>>> CreditOrder([FromBody] OrderDTO orderDTO)
        {
            var response = new APIResponse<bool>();
            try
            {
                response.Result = await _orderService.CreditOrder(orderDTO);
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(response);
            }
        }

        [HttpPost("DebitOrder")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse<bool>>> DebitOrder([FromBody] OrderDTO orderDTO)
        {
            var response = new APIResponse<bool>();
            try
            {
                response.Result = await _orderService.DebitOrder(orderDTO);
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(response);
            }
        }

        [HttpGet("balanceDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse<GetBalanceResponse>>> GetBalance(DateTime? balanceDate)
        {
            var response = new APIResponse<GetBalanceResponse>();
            try
            {
                response.Result = await _orderService.GetBalance(balanceDate);
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(response);
            }
        }
    }
}