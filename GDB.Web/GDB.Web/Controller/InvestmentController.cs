using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Implementation;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {

        private readonly ILogger<InvestmentController> logger;
        private IInvestmentRepository investmentRepository { get; set; }
        public InvestmentController(IInvestmentRepository _investmentRepository, ILogger<InvestmentController> _logger)
        {
            investmentRepository = _investmentRepository;
            logger = _logger;
        }


        [HttpGet]
        [Route("GetAllInvestmentSubCategories")]
        public async Task<IActionResult> GetAllInvestmentSubCategories()
        {
            try
            {
                var investmentSubCategoriesData = await investmentRepository.GetAllInvestmentSubCategories();
                if (investmentSubCategoriesData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(investmentSubCategoriesData);
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
        [Route("GetAllInvestmentCategories")]
        public async Task<IActionResult> GetAllInvestmentCategories()
        {
            try
            {
                var investmentCategoriesData = await investmentRepository.GetAllInvestmentCategories();
                if (investmentCategoriesData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(investmentCategoriesData);
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
        [Route("GetAllInvestments")]
        public async Task<IActionResult> GetAllInvestments()
        {
            try
            {
                var investments = await investmentRepository.GetAllInvestmentDetails();
                if (investments.Count == 0)
                {
                    return NoContent();
                }
                return Ok(investments);
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
        public async Task<IActionResult> Add([FromBody] InvestmentViewModel investmentViewModel)
        {
            try
            {
                if (investmentViewModel == null)
                {
                    return BadRequest("Investment data is required.");
                }
                var response = await investmentRepository.AddInvestment(investmentViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Add), new { id = investmentViewModel.InvestmentId }, investmentViewModel);
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to add Investment ");
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
        [Route("VerifySecurityCheck")]
        public async Task<IActionResult> VerifySecurityCheck([FromBody] SecurityCheckViewModel securityCheckViewModel)
        {
            var securityCheckResultData = new SecurityCheckViewModel();
            try
            {
                if (securityCheckViewModel == null)
                {
                    return BadRequest("Investment data is required.");
                }
                securityCheckResultData = await investmentRepository.VerifySecurityChecks(securityCheckViewModel);
                if (securityCheckResultData != null)
                {
                    //var status = CreatedAtAction(nameof(Add), new { id = securityCheckViewModel.Security12DigitsPasscode }, securityCheckViewModel);
                    return Ok(securityCheckResultData);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to add Investment ");
                    return BadRequest(null);
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing your request.");
                return Ok(null);
            }
        }


        [HttpPost]
        [Route("AddInvestmentSummary")]
        public async Task<IActionResult> AddInvestmentSummary([FromBody] InvestmentSummaryViewModel investmentSummaryViewModel)
        {
            try
            {
                if (investmentSummaryViewModel == null)
                {
                    return BadRequest("Investment data is required.");
                }
                var response = await investmentRepository.AddInvestmentSummary(investmentSummaryViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Add), new { id = investmentSummaryViewModel.InvestmentSummaryId }, investmentSummaryViewModel);
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to add Investment ");
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
