using GDB.Web.BLL.Implementation;
using GDB.Web.BLL.Interface;
using GDB.Web.DataAccess.Implementation;
using GDB.Web.DataAccess.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GDB.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MLController : ControllerBase
    {
        private readonly ILogger<MLController> logger;

        private readonly IMLModelRepository mLModelRepository;

        public MLController(IMLModelRepository _mLModelRepository, ILogger<MLController> _logger)
        {
            mLModelRepository = _mLModelRepository;
            logger = _logger;
        }


        [HttpGet]
        [Route("PredictSales")]
        public async Task<IActionResult> PredictSales()
        {
            try
            {
                var prediction = await mLModelRepository.TrainModelAndPredictSales();
                if (prediction.Count == 0)
                {
                    return NoContent();
                }
                return Ok(prediction);
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
        [Route("PredictExpenses")]
        public async Task<IActionResult> PredictExpenses()
        {
            try
            {
                var expensesPrediction = await mLModelRepository.TrainModelAndPredictExpenses();
                if (expensesPrediction.Count == 0)
                {
                    return NoContent();
                }
                return Ok(expensesPrediction);
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
        [Route("GetExistingSalesData")]
        public async Task<IActionResult> GetExistingSalesData()
        {
            try
            {
                var prediction = await mLModelRepository.GetExistingSalesData();
                if (prediction.Count == 0)
                {
                    return NoContent();
                }
                return Ok(prediction);
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
        [Route("GetExistingExpensesData")]
        public async Task<IActionResult> GetExistingExpensesData()
        {
            try
            {
                var expensesPrediction = await mLModelRepository.GetExistingExpensesData();
                if (expensesPrediction.Count == 0)
                {
                    return NoContent();
                }
                return Ok(expensesPrediction);
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
