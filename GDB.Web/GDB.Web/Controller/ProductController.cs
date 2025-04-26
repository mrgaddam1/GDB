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
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private IProductRepository iProductRepository { get; set; }
        public ProductController(IProductRepository _iProductRepository, ILogger<ProductController> _logger)
        {
            iProductRepository = _iProductRepository;
            logger = _logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var productsData = await iProductRepository.GetAll();
                if (productsData.Count == 0)
                {
                    return NoContent();
                }
                return Ok(productsData);
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
        public async Task<IActionResult> Add([FromBody] ProductViewModel productViewModel)
        {
            try
            {
                if (productViewModel == null)
                {
                    return BadRequest("Product data is required.");
                }
                var response = await iProductRepository.Add(productViewModel);
                if (response)
                {
                    var status = CreatedAtAction(nameof(Add), new { id = productViewModel.ProductId }, productViewModel);
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to add Product");
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
        public async Task<IActionResult> Update([FromBody] ProductViewModel productViewModel)
        {
            try
            {
                if (productViewModel == null)
                {
                    return BadRequest("Product data is required.");
                }
                var response = await iProductRepository.Update(productViewModel);
                if (response)
                {
                    var status = (new { message = "Product details are updated successfully", productViewModel });
                    return Ok(status);
                }
                else
                {
                    var status = StatusCode(StatusCodes.Status400BadRequest, "Failed to update Product");
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
