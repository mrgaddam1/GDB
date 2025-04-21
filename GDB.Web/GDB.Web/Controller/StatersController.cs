using GDB.Web.DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatersController : ControllerBase
    {
        private readonly ILogger<StatersController> logger;
        private IStaterRepository staterRepository { get; set; }
        public StatersController(IStaterRepository _staterRepository, ILogger<StatersController> _logger)
        {
            staterRepository = _staterRepository;
            logger = _logger;
        }

        [HttpGet]
        [Route("GetAllStaters")]
        public async Task<IActionResult> GetAllStaters()
        {
            try
            {
                var staters = await staterRepository.GetAllStaters();
                if (staters.Count == 0)
                {
                    return NoContent();
                }
                return Ok(staters);
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
        [Route("GetStaterPriceByStater/{staterId}")]
        public async Task<IActionResult> GetStaterPriceByStater(int staterId)
        {
            try
            {
                var statersData = await staterRepository.GetStaterPriceByStater(staterId);
                if (statersData == null)
                {
                    return NotFound();
                }
                return Ok(statersData);
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
