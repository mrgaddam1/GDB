using GDB.Web.DataAccess.Implementation;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> logger;
        private IStoreRepository storeRepository { get; set; }
        public StoreController(IStoreRepository _storeRepository, ILogger<StoreController> _logger)
        {
            storeRepository = _storeRepository;
            logger = _logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var storeData = await storeRepository.GetAll();
                if (storeData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(storeData);
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
        public async Task<IActionResult> Add([FromBody] StoreViewModel storeViewModel)
        {
            try
            {
                if (storeViewModel == null)
                {
                    return BadRequest("Orders data is required.");
                }
                var response = await storeRepository.Add(storeViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Add), new { id = storeViewModel.StoreId }, storeViewModel);
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
        public async Task<IActionResult> Update([FromBody] StoreViewModel storeViewModel)
        {
            try
            {
                if (storeViewModel == null)
                {
                    return BadRequest("Orders data is required.");
                }
                var response = await storeRepository.Update(storeViewModel);
                if (response)
                {
                    var status = (new { message = "Store details are updated successfully", storeViewModel });
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to update Store");
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
