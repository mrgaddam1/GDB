using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Implementation;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
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
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] OrderTypeViewModel orderTypesViewModel)
        {
            try
            {
                if (orderTypesViewModel == null)
                {
                    return BadRequest("Order Type Data is required.");
                }
                var response = await orderTypesRepository.Add(orderTypesViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Add), new { id = orderTypesViewModel.OrderTypeId }, orderTypesViewModel);
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to add Order Type Data");
                    return BadRequest(status);
                }

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

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] OrderTypeViewModel orderTypesViewModel)
        {
            try
            {
                if (orderTypesViewModel == null)
                {
                    return BadRequest("Order Type data is required.");
                }
                var response = await orderTypesRepository.Update(orderTypesViewModel);
                if (response)
                {
                    var status = (new { message = "Order Type details are updated successfully", orderTypesViewModel });
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to update Location");
                    return BadRequest(status);
                }

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
