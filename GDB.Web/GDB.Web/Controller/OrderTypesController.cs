using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Implementation;
using GDB.Web.DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTypesController : ControllerBase
    {
        private readonly ILogger<OrderTypesController> logger;
        private IOrderTypesRepository orderTypesRepository { get; set; }
        public OrderTypesController(IOrderTypesRepository _orderTypesRepository, ILogger<OrderTypesController> _logger)
        {
            orderTypesRepository = _orderTypesRepository;
            logger = _logger;
        }

        [HttpGet]
        [Route("GetAllOrderTypes")]
        public async Task<IActionResult> GetAllOrderTypes()
        {
            try
            {
                var orderTypes = await orderTypesRepository.GetAllOrderTypes();
                if (orderTypes.Count == 0)
                {
                    return NoContent();
                }
                return Ok(orderTypes);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing your request.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = ex.Message,
                    Details = ex.StackTrace
                });
            }

        }


        [HttpGet]
        [Route("GetOrderTypeItemPriceByOrderType/{orderTypeId}")]
        public async Task<IActionResult> GetOrderTypeItemPriceByOrderType(int orderTypeId)
        {
            try
            {
                var orderTypesData = await orderTypesRepository.GetSelectedOrderTypeItemPriceOrderType(orderTypeId);
                if (orderTypesData == null)
                {
                    return NotFound();
                }
                return Ok(orderTypesData);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing your request.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = ex.Message,
                    Details = ex.StackTrace
                });
            }
        }
    }
}
