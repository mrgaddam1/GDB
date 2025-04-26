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
    public class SubCategoryController : ControllerBase
    {
        private readonly ILogger<SubCategoryController> logger;
        private ISubCategoryRepository iSubCategoryRepository { get; set; }
        public SubCategoryController(ISubCategoryRepository _iSubCategoryRepository, ILogger<SubCategoryController> _logger)
        {
            iSubCategoryRepository = _iSubCategoryRepository;
            logger = _logger;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var subCategoryData = await iSubCategoryRepository.GetAll();
                if (subCategoryData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(subCategoryData);
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
        public async Task<IActionResult> Add([FromBody] SubCategoryViewModel subCategoryViewModel)
        {
            try
            {
                if (subCategoryViewModel == null)
                {
                    return BadRequest("Subcategory data is required.");
                }
                var response = await iSubCategoryRepository.Add(subCategoryViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Add), new { id = subCategoryViewModel.SubCategoryId }, subCategoryViewModel);
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to add Sub ategory details");
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
        public async Task<IActionResult> Update([FromBody] SubCategoryViewModel subCategoryViewModel)
        {
            try
            {
                if (subCategoryViewModel == null)
                {
                    return BadRequest("SubCategory data is required.");
                }
                var response = await iSubCategoryRepository.Update(subCategoryViewModel);
                if (response)
                {
                    var status = (new { message = "SubCategory details are updated successfully", subCategoryViewModel });
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to update subCategory Data");
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
