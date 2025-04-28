using GDB.Web.DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly ILogger<VendorController> logger;
        private IVendorRepository vendorRepository { get; set; }
        public VendorController(IVendorRepository _vendorRepository, ILogger<VendorController> _logger)
        {
            vendorRepository = _vendorRepository;
            logger = _logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var vendors = await vendorRepository.GetAll();
                if (vendors.Count == 0)
                {
                    return NoContent();
                }
                return Ok(vendors);
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
