using GDB.Web.DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly ILogger<PaymentTypeController> logger;
        private IPaymentTypeRepository paymentTypeRepository { get; set; }
        public PaymentTypeController(IPaymentTypeRepository _paymentTypeRepository, ILogger<PaymentTypeController> _logger)
        {
            paymentTypeRepository = _paymentTypeRepository;
            logger = _logger;
        }

        [HttpGet]
        [Route("GetAllPaymentTypes")]
        public async Task<IActionResult> GetAllPaymentTypes()
        {
            try
            {
                var paymentTypes = await paymentTypeRepository.GetAll();
                if (paymentTypes.Count == 0)
                {
                    return NoContent();
                }
                return Ok(paymentTypes);
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
