using GDB.Web.DataAccess.Implementation;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILogger<LocationsController> logger;
        private ILocationRepository locationRepository { get; set; }
        public LocationsController(ILocationRepository _locationRepository, ILogger<LocationsController> _logger)
        {
            locationRepository = _locationRepository;
            logger = _logger;
        }

        [HttpGet]
        [Route("GetAllLocations")]
        public async Task<IActionResult> GetAllLocations()
        {
            try
            {
                var locationsData = await locationRepository.GetAllLocations();
                if (locationsData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(locationsData);
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
        public async Task<IActionResult> Add([FromBody] LocationViewModel locationViewModel)
        {
            try
            {
                if (locationViewModel == null)
                {
                    return BadRequest("Location data is required.");
                }
                var response = await locationRepository.Add(locationViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Add), new { id = locationViewModel.LocationId }, locationViewModel);
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
        public async Task<IActionResult> Update([FromBody] LocationViewModel locationViewModel)
        {
            try
            {
                if (locationViewModel == null)
                {
                    return BadRequest("Location data is required.");
                }
                var response = await locationRepository.Update(locationViewModel);
                if (response)
                {
                    var status = (new { message = "Location details are updated successfully", locationViewModel });
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
