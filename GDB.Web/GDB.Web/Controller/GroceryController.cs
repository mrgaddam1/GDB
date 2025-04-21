using GDB.Web.DataAccess.Interface;
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
    }
}
