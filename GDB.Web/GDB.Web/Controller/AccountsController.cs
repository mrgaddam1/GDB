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


    }
}
