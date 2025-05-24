using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
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

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] StatersViewModel statersViewModel)
        {
            try
            {
                if (statersViewModel == null)
                {
                    return BadRequest("Stater data is required.");
                }
                var response = await staterRepository.Add(statersViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Add), new { id = statersViewModel.StaterId }, statersViewModel);
                    return Ok(status);
                }
                return BadRequest("Failed to add stater.");
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
        public async Task<IActionResult> Update([FromBody] StatersViewModel statersViewModel)
        {
            try
            {
                if (statersViewModel == null)
                {
                    return BadRequest("Stater data is required.");
                }
                var response = await staterRepository.Update(statersViewModel);
                if (response)
                {
                    return Ok("Stater updated successfully.");
                }
                return BadRequest("Failed to update stater.");
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
