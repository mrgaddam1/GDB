using GDB.Web.DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerReportsController : ControllerBase
    {
        private readonly ILogger<CustomerReportsController> logger;
        private ICustomerRepository customerRepository { get; set; }
        public CustomerReportsController(ICustomerRepository _customerRepository,
                                 ILogger<CustomerReportsController> _logger)
        {
            customerRepository = _customerRepository;
            logger = _logger;
        }

        [HttpGet]
        [Route("GetAllCustomerVisits")]
        public async Task<IActionResult> GetAllCustomerVisits()
        {
            try
            {
                var customers = await customerRepository.GetAllCustomerVisits();
                if (customers.Count == 0)
                {
                    return NoContent();
                }
                return Ok(customers);
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
        [Route("GetAllCustomerVisitsWeekly")]
        public async Task<IActionResult> GetAllCustomerVisitsWeekly()
        {
            try
            {
                var customers = await customerRepository.GetAllCustomerVisitsBy_Weekly();
                if (customers.Count == 0)
                {
                    return NoContent();
                }
                return Ok(customers);
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
        [Route("GetAllCustomerVisitsByWeekly")]
        public async Task<IActionResult> GetAllCustomerVisitsByWeekly()
        {
            try
            {
                var customers = await customerRepository.GetAllCustomerVisitsBy_BIWeekly();
                if (customers.Count == 0)
                {
                    return NoContent();
                }
                return Ok(customers);
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
        [Route("GetAllCustomerVisitsByMonthly")]
        public async Task<IActionResult> GetAllCustomerVisitsByMonthly()
        {
            try
            {
                var customers = await customerRepository.GetAllCustomerVisitsBy_Monthly();
                if (customers.Count == 0)
                {
                    return NoContent();
                }
                return Ok(customers);
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
        [Route("GetAllCustomerVisitsQuarterly")]
        public async Task<IActionResult> GetAllCustomerVisitsQuarterly()
        {
            try
            {
                var customers = await customerRepository.GetAllCustomerVisitsBy_Quarterly();
                if (customers.Count == 0)
                {
                    return NoContent();
                }
                return Ok(customers);
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
        [Route("GetAllCustomerVisitsHalfYearly")]
        public async Task<IActionResult> GetAllCustomerVisitsHalfYearly()
        {
            try
            {
                var customers = await customerRepository.GetAllCustomerVisitsBy_HalyYearly();
                if (customers.Count == 0)
                {
                    return NoContent();
                }
                return Ok(customers);
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
        [Route("GetAllCustomerVisitsYearly")]
        public async Task<IActionResult> GetAllCustomerVisitsYearly()
        {
            try
            {
                var customers = await customerRepository.GetAllCustomerVisitsBy_Yearly();
                if (customers.Count == 0)
                {
                    return NoContent();
                }
                return Ok(customers);
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
