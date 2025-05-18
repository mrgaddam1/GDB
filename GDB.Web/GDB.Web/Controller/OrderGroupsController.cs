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
    public class OrderGroupsController : ControllerBase
    {
        private readonly ILogger<OrderGroupsController> logger;
        private IOrderGroupsRepository orderGroupsRepository { get; set; } 
        public OrderGroupsController(IOrderGroupsRepository _orderGroupsRepository, ILogger<OrderGroupsController> _logger)
        {
            orderGroupsRepository = _orderGroupsRepository;
            logger = _logger;
        }

        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await orderGroupsRepository.GetAllOrders();
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

        

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] OrderGroupsViewModel orderGroupsViewModel)
        {
            try
            {
                if (orderGroupsViewModel == null)
                {
                    return BadRequest("Order Groups data is required.");
                }
                var response = await orderGroupsRepository.Add(orderGroupsViewModel);
                if(response)
                {
                    var status = CreatedAtAction(nameof(Add), new {id = orderGroupsViewModel.OrderGroupId}, orderGroupsViewModel);
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to add Order Group");
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
        public async Task<IActionResult> Update([FromBody] OrderGroupsViewModel orderGroupsViewModel)
        {
            try
            {
                if (orderGroupsViewModel == null)
                {
                    return BadRequest("Order Group data is required.");
                }
                var response = await orderGroupsRepository.Update(orderGroupsViewModel);
                if (response)
                {
                    var status = (new {message = "Order Group details are updated successfully", orderGroupsViewModel });
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to Update Order Groups");
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
  