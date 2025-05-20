using GDB.Web.DataAccess.Implementation;
using GDB.Web.DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> logger;
        private IAccountRepository accountRepository { get; set; }
        public AccountsController(IAccountRepository _accountRepository, ILogger<AccountsController> _logger)
        {
            accountRepository = _accountRepository;
            logger = _logger;
        }


        [HttpGet]
        [Route("GetAllAccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                var accountsData = await accountRepository.GetAllAccounts();
                if (accountsData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(accountsData);
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
        [Route("GetAllAccountsBy_Yearly")]
        public async Task<IActionResult> GetAllAccountsBy_Yearly()
        {
            try
            {
                var accountsData = await accountRepository.GetAllAccountsBy_Yearly();
                if (accountsData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(accountsData);
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
        [Route("GetAllAccountsBy_HalfYearly")]
        public async Task<IActionResult> GetAllAccountsBy_HalfYearly()
        {
            try
            {
                var accountsData = await accountRepository.GetAllAccountsBy_HalfYearly();
                if (accountsData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(accountsData);
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
        [Route("GetAllAccountsBy_Quarterly")]
        public async Task<IActionResult> GetAllAccountsBy_Quarterly()
        {
            try
            {
                var accountsData = await accountRepository.GetAllAccountsBy_Quarterly();
                if (accountsData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(accountsData);
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
        [Route("GetAllAccountsBy_LastMonth")]
        public async Task<IActionResult> GetAllAccountsBy_LastMonth()
        {
            try
            {
                var accountsData = await accountRepository.GetAllAccountsBy_LastMonth();
                if (accountsData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(accountsData);
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
        [Route("GetAllAccountsBy_BIWeekly")]
        public async Task<IActionResult> GetAllAccountsBy_BIWeekly()
        {
            try
            {
                var accountsData = await accountRepository.GetAllAccountsBy_BIWeekly();
                if (accountsData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(accountsData);
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
        [Route("GetAllAccountsBy_Weekly")]
        public async Task<IActionResult> GetAllAccountsBy_Weekly()
        {
            try
            {
                var accountsData = await accountRepository.GetAllAccountsBy_Weekly();
                if (accountsData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(accountsData);
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
