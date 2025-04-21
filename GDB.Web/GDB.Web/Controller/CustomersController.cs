using GDB.Web.DataAccess.Implementation;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> logger;
        private ICustomerRepository customerRepository { get; set; }
        public CustomersController(ICustomerRepository _customerRepository, ILogger<CustomersController> _logger)
        {
            customerRepository = _customerRepository;
            logger = _logger;
        }

        [HttpGet]
        [Route("GetAllCustomers")]    
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await customerRepository.GetAll();
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

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] CustomerViewModel customerViewModel)
        {
            try
            {
                if (customerViewModel == null)
                {
                    return BadRequest("Orders data is required.");
                }
                var response = await customerRepository.Add(customerViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Add), new { id = customerViewModel.CustomerId }, customerViewModel);
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
        public async Task<IActionResult> Update([FromBody] CustomerViewModel customerViewModel)
        {
            try
            {
                if (customerViewModel == null)
                {
                    return BadRequest("Orders data is required.");
                }
                var response = await customerRepository.Update(customerViewModel);
                if (response)
                {
                    var status = (new { message = "Customer details are updated successfully", customerViewModel });
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to update Customer");
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
