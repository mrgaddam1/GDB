using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
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

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] VendorViewModel vendorViewModel)
        {
            try
            {
                if (vendorViewModel == null)
                {
                    return BadRequest("Vendor data is required.");
                }
                var response = await vendorRepository.Add(vendorViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Add), new { id = vendorViewModel.VendorId }, vendorViewModel);
                    return Ok(status);
                }
                return BadRequest("Unable to add vendor.");
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
        public async Task<IActionResult> Update([FromBody] VendorViewModel vendorViewModel)
        {
            try
            {
                if (vendorViewModel == null)
                {
                    return BadRequest("Vendor data is required.");
                }
                var response = await vendorRepository.Update(vendorViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Update), new { id = vendorViewModel.VendorId }, vendorViewModel);
                    return Ok(status);
                }
                return BadRequest("Unable to update vendor.");
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
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Vendor ID is required.");
                }
                var response = await vendorRepository.Delete(id);
                if (response)
                {
                    return Ok("Vendor deleted successfully.");
                }
                return BadRequest("Unable to delete vendor.");
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
