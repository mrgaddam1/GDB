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
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> logger;
        private ICategoryRepository iCategoryRepository { get; set; }
        public CategoryController(ICategoryRepository _iCategoryRepository, ILogger<CategoryController> _logger)
        {
            iCategoryRepository = _iCategoryRepository;
            logger = _logger;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categoriesData = await iCategoryRepository.GetAll();
                if (categoriesData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(categoriesData);
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
        public async Task<IActionResult> Add([FromBody] CategoryViewModel categoryViewModel)
        {
            try
            {
                if (categoryViewModel == null)
                {
                    return BadRequest("Location data is required.");
                }
                var response = await iCategoryRepository.Add(categoryViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Add), new { id = categoryViewModel.CategoryId }, categoryViewModel);
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to add Category");
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
        public async Task<IActionResult> Update([FromBody] CategoryViewModel categoryViewModel)
        {
            try
            {
                if (categoryViewModel == null)
                {
                    return BadRequest("Category data is required.");
                }
                var response = await iCategoryRepository.Update(categoryViewModel);
                if (response)
                {
                    var status = (new { message = "Category details are updated successfully", categoryViewModel });
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to update Category");
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
