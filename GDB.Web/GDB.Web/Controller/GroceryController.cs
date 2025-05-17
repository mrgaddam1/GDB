using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroceryController : ControllerBase
    {
        private readonly ILogger<GroceryController> logger;
        private IGroceryRepository groceryRepository { get; set; }
        public GroceryController(IGroceryRepository _groceryRepository, ILogger<GroceryController> _logger)
        {
            groceryRepository = _groceryRepository;
            logger = _logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var groceryData = await groceryRepository.GetAll();
                if (groceryData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(groceryData);
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
        public async Task<IActionResult> Add(GroceryViewModel groceryViewModel)
        {
            try
            {
                if (groceryViewModel == null)
                {
                    return BadRequest("Grocery data is required.");
                }
                var response = await groceryRepository.Add(groceryViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Add), new { id = groceryViewModel.GroceryId }, groceryViewModel);
                    return Ok(status);
                }
                else
                {
                    return BadRequest("Failed to add grocery item.");
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
        public async Task<IActionResult> Update(GroceryViewModel groceryViewModel)
        {
            try
            {
                if (groceryViewModel == null)
                {
                    return BadRequest("Grocery data is required.");
                }
                var response = await groceryRepository.Update(groceryViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Update), new { id = groceryViewModel.GroceryId }, groceryViewModel);
                    return Ok(status);
                }
                else
                {
                    return BadRequest("Failed to update grocery item.");
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
