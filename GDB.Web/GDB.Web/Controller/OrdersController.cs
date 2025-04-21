using GDB.Web.BLL.Interface;
using GDB.Web.DataAccess.Implementation;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> logger;
        private IOrdersRepository ordersRepository { get; set; } 
        public OrdersController(IOrdersRepository _ordersRepository, ILogger<OrdersController> _logger)
        {
            ordersRepository = _ordersRepository;
            logger = _logger;
        }

        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await ordersRepository.GetAllOrders();
                if(orders.Count == 0)
                {
                    return NoContent();
                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing your request.");
                return StatusCode(StatusCodes.Status500InternalServerError,new
                {
                    Message = ex.Message,
                    Details = ex.StackTrace
                });
            }            
        }

        [HttpGet]
        [Route("GetMaxWeekId")]
        public async Task<IActionResult> GetMaxWeekId()
        {
            int? maxWeekId;
            try
            {
                maxWeekId = await ordersRepository.GetMaxWeekId();
                if (maxWeekId ==null)
                {
                    return NoContent();
                }
                return Ok(maxWeekId);
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
        public async Task<IActionResult> Add([FromBody] OrdersViewModel ordersViewModel)
        {
            try
            {
                if (ordersViewModel == null)
                {
                    return BadRequest("Orders data is required.");
                }
                var response = await ordersRepository.Add(ordersViewModel);
                if(response)
                {
                    var status = CreatedAtAction(nameof(Add), new {id = ordersViewModel.OrderId}, ordersViewModel);
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to add Orders");
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
        public async Task<IActionResult> Update([FromBody] OrdersViewModel ordersViewModel)
        {
            try
            {
                if (ordersViewModel == null)
                {
                    return BadRequest("Orders data is required.");
                }
                var response = await ordersRepository.Update(ordersViewModel);
                if (response)
                {
                    var status = (new {message = "Order details are updated successfully", ordersViewModel });
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to Update Orders");
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

        [HttpGet]
        [Route("GetTotalOrdersByWeekwise")]
        public async Task<IActionResult> GetTotalOrdersByWeekwise()
        {
            try
            {
                var totalOrdersByWeekly = await ordersRepository.GetTotalOrdersByWeekwise();
                if (totalOrdersByWeekly.Count == 0)
                {
                    return NoContent();
                }
                return Ok(totalOrdersByWeekly);
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
  