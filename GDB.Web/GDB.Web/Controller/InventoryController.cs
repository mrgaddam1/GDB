using GDB.Web.DataAccess.Implementation;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using GDB.Web.Shared.Inventory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> logger;
        private IInventoryRepository iInventoryRepository { get; set; }
        public InventoryController(IInventoryRepository _iInventoryRepository, ILogger<InventoryController> _logger)
        {
            iInventoryRepository = _iInventoryRepository;
            logger = _logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var inventoryData = await iInventoryRepository.GetAll();
                if (inventoryData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(inventoryData);
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
        [Route("GetAllInventoryHistory")]
        public async Task<IActionResult> GetAllInventoryHistory()
        {
            try
            {
                var inventoryData = await iInventoryRepository.GetAllInventoryHistory();
                if (inventoryData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(inventoryData);
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
        public async Task<IActionResult> Add([FromBody] InventoryViewModel inventoryViewModel)
        {
            try
            {
                if (inventoryViewModel == null)
                {
                    return BadRequest("Inventory data is required.");
                }
                var response = await iInventoryRepository.Add(inventoryViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Add), new { id = inventoryViewModel.InventoryId }, inventoryViewModel);
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to add Inventory Data");
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
        public async Task<IActionResult> Update([FromBody] InventoryViewModel inventoryViewModel)
        {
            try
            {
                if (inventoryViewModel == null)
                {
                    return BadRequest("Inventory data is required.");
                }
                var response = await iInventoryRepository.Update(inventoryViewModel);
                if (response)
                {
                    var status = (new { message = "Inventory details are updated successfully.", inventoryViewModel });
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to update Inventory");
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
