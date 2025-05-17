using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertiseSourceController : ControllerBase
    {
        private readonly ILogger<AdvertiseSourceController> logger;
        private IAdvertiseSourceRepository advertiseSourceRepository { get; set; }
        public AdvertiseSourceController(IAdvertiseSourceRepository _advertiseSourceRepository, ILogger<AdvertiseSourceController> _logger)
        {
            advertiseSourceRepository = _advertiseSourceRepository;
            logger = _logger;
        }

        [HttpGet]
        [Route("GetAllAdvertiseSources")]
        public async Task<IActionResult> GetAllAdvertiseSources()
        {
            try
            {
                var advertiseSourcesData = await advertiseSourceRepository.GetAllAdvertiseSources();
                if (advertiseSourcesData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(advertiseSourcesData);
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
        public async Task<IActionResult> Add(AdvertiseSourceViewModel advertiseSource)
        {
            try
            {
                var result = await advertiseSourceRepository.Add(advertiseSource);
                if (result)
                {
                    return Ok(new { Message = "Advertise Source added successfully." });
                }
                return BadRequest(new { Message = "Failed to add Advertise Source." });
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
        public async Task<IActionResult> Update(AdvertiseSourceViewModel advertiseSource)
        {
            try
            {
                var result = await advertiseSourceRepository.Update(advertiseSource);
                if (result)
                {
                    return Ok(new { Message = "Advertise Source updated successfully." });
                }
                return BadRequest(new { Message = "Failed to update Advertise Source." });
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
