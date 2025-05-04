using GDB.Web.DataAccess.Implementation;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ILogger<ExpensesController> logger;
        private IExpensesRepository expensesRepository { get; set; }
        public ExpensesController(IExpensesRepository _expensesRepository, ILogger<ExpensesController> _logger)
        {
            expensesRepository = _expensesRepository;
            logger = _logger;
        }

        [HttpGet]
        [Route("GetAllExpenses")]
        public async Task<IActionResult> GetAllExpenses()
        {
            try
            {
                var expenses = await expensesRepository.GetAllExpenses();
                if (expenses.Count == 0)
                {
                    return NoContent();
                }
                return Ok(expenses);
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
        public async Task<IActionResult> Add([FromBody] ExpensesViewModel expensesViewModel)
        {
            try
            {
                if (expensesViewModel == null)
                {
                    return BadRequest("Orders data is required.");
                }
                var response = await expensesRepository.Add(expensesViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Add), new { id = expensesViewModel.ExpensesId }, expensesViewModel);
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
        public async Task<IActionResult> Update([FromBody] ExpensesViewModel expensesViewModel)
        {
            try
            {
                if (expensesViewModel == null)
                {
                    return BadRequest("Expenses data is required.");
                }
                var response = await expensesRepository.Update(expensesViewModel);
                if (response)
                {
                    var status = (new { message = "Expenses details are updated successfully", expensesViewModel });
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to update Expenses Details.");
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
        [Route("GetTotalExpensesByWeekwise")]
        public async Task<IActionResult> GetTotalExpensesByWeekwise()
        {
            try
            {
                var totalExpensesByWeekly = await expensesRepository.GetTotalExpensesByWeekwise();
                if (totalExpensesByWeekly.Count == 0)
                {
                    return NoContent();
                }
                return Ok(totalExpensesByWeekly);
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
        [Route("GetAllExpensesByWeekWise")]
        public async Task<IActionResult> GetAllExpensesByWeekWise()
        {
            try
            {
                var expensesByWeekly = await expensesRepository.GetExpesesReportBy_Weekwise();
                if (expensesByWeekly.Count == 0)
                {
                    return NoContent();
                }
                return Ok(expensesByWeekly);
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
        [Route("GetAllExpensesByBIWeeklyWise")]
        public async Task<IActionResult> GetAllExpensesByBIWeeklyWise()
        {
            try
            {
                var expensesByBIWeekly = await expensesRepository.GetExpesesReportBy_BIWeekly();
                if (expensesByBIWeekly.Count == 0)
                {
                    return NoContent();
                }
                return Ok(expensesByBIWeekly);
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
        [Route("GetAllExpensesByMonthWise")]
        public async Task<IActionResult> GetAllExpensesByMonthWise()
        {
            try
            {
                var expensesByMonthly = await expensesRepository.GetExpesesReportBy_Monthly();
                if (expensesByMonthly.Count == 0)
                {
                    return NoContent();
                }
                return Ok(expensesByMonthly);
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
        [Route("GetAllExpensesByQuarterlyWise")]
        public async Task<IActionResult> GetAllExpensesByQuarterlyWise()
        {
            try
            {
                var expensesByQuarterly = await expensesRepository.GetExpesesReportBy_Quarterly();
                if (expensesByQuarterly.Count == 0)
                {
                    return NoContent();
                }
                return Ok(expensesByQuarterly);
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
        [Route("GetAllExpensesByHalyYearlyWise")]
        public async Task<IActionResult> GetAllExpensesByHalyYearlyWise()
        {
            try
            {
                var expensesByHalyYearly = await expensesRepository.GetExpesesReportBy_HalfYearly();
                if (expensesByHalyYearly.Count == 0)
                {
                    return NoContent();
                }
                return Ok(expensesByHalyYearly);
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
        [Route("GetAllExpensesByYearlyWise")]
        public async Task<IActionResult> GetAllExpensesByYearlyWise()
        {
            try
            {
                var expensesByYearly = await expensesRepository.GetExpesesReportBy_Yearly();
                if (expensesByYearly.Count == 0)
                {
                    return NoContent();
                }
                return Ok(expensesByYearly);
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
