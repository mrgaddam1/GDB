using GDB.Web.DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderReportsController : ControllerBase
    {
        private readonly ILogger<OrderReportsController> logger;
        private IOrdersRepository ordersRepository { get; set; }
        public OrderReportsController(IOrdersRepository _ordersRepository,
                                 ILogger<OrderReportsController> _logger)
        {
            ordersRepository = _ordersRepository;
            logger = _logger;
        }

         
        [HttpGet]
        [Route("GetAllOrderByWeekly")]
        public async Task<IActionResult> GetAllOrderByWeekly()
        {
            try
            {
                var ordersByWeekly = await ordersRepository.GetAllOrdersWeekly();
                if (ordersByWeekly.Count == 0)
                {
                    return NoContent();
                }
                return Ok(ordersByWeekly);
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
        [Route("GetAllOrderByBIWeekly")]
        public async Task<IActionResult> GetAllOrderByBIWeekly()
        {
            try
            {
                var orderByBiWeekly = await ordersRepository.GetAllOrdersByWeekly();
                if (orderByBiWeekly.Count == 0)
                {
                    return NoContent();
                }
                return Ok(orderByBiWeekly);
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
        [Route("GetAllOrderByMonthly")]
        public async Task<IActionResult> GetAllOrderByMonthly()
        {
            try
            {
                var ordersByMonthly = await ordersRepository.GetAllOrdersByMonthly();
                if (ordersByMonthly.Count == 0)
                {
                    return NoContent();
                }
                return Ok(ordersByMonthly);
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
        [Route("GetAllOrderByQuarterly")]
        public async Task<IActionResult> GetAllOrderByQuarterly()
        {
            try
            {
                var ordersByQuaterly = await ordersRepository.GetAllOrdersByQuarterly();
                if (ordersByQuaterly.Count == 0)
                {
                    return NoContent();
                }
                return Ok(ordersByQuaterly);
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
        [Route("GetAllOrderByHalfYearly")]
        public async Task<IActionResult> GetAllOrderByHalfYearly()
        {
            try
            {
                var orderByHalyYearly = await ordersRepository.GetAllOrdersByHalfYearly();
                if (orderByHalyYearly.Count == 0)
                {
                    return NoContent();
                }
                return Ok(orderByHalyYearly);
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
        [Route("GetAllOrderByYearly")]
        public async Task<IActionResult> GetAllOrderByYearly()
        {
            try
            {
                var ordersYearly = await ordersRepository.GetAllOrdersByYearly();
                if (ordersYearly.Count == 0)
                {
                    return NoContent();
                }
                return Ok(ordersYearly);
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

