using GDB.Web.DataAccess.Implementation;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectDebitPaymentController : ControllerBase
    {
        private readonly ILogger<DirectDebitPaymentController> logger;
        private readonly IMonthlyDirectDebitPaymentRepository monthlyDirectDebitPaymentRepository;
        public DirectDebitPaymentController(IMonthlyDirectDebitPaymentRepository _monthlyDirectDebitPaymentRepository,
                                            ILogger<DirectDebitPaymentController> _logger) 
        {
            monthlyDirectDebitPaymentRepository = _monthlyDirectDebitPaymentRepository;
            logger = _logger;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] MonthlyDirectDebitPaymentViewModel  monthlyDirectDebitPaymentViewModel)
        {
            try
            {
                if (monthlyDirectDebitPaymentViewModel == null)              
                    return BadRequest("Monthly DirectDebit Payment  data is required.");
              
                var response = await monthlyDirectDebitPaymentRepository.AddMonthlyDirectDebitPaymentsAsync(monthlyDirectDebitPaymentViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Add), new { id = monthlyDirectDebitPaymentViewModel.DeductionId }, monthlyDirectDebitPaymentViewModel);
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to add Location");
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
        public async Task<IActionResult> Update([FromBody] MonthlyDirectDebitPaymentViewModel monthlyDirectDebitPaymentViewModel)
        {
            try
            {
                if (monthlyDirectDebitPaymentViewModel == null)                
                    return BadRequest("Monthly DirectDebit Payment  data is required.");
               
                var response = await monthlyDirectDebitPaymentRepository.UpdateMonthlyDirectDebitPaymentsAsync(monthlyDirectDebitPaymentViewModel);
                if (response)
                {
                    var status = (new { message = "Location details are updated successfully", monthlyDirectDebitPaymentViewModel });
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
