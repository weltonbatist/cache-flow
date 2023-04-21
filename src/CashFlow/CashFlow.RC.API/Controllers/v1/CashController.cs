using CashFlow.RC.API.Service.services;
using CashFlow.RC.Common.dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CashFlow.RC.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CashController : ControllerBase
    {
        private readonly ILogger<CashController> _logger;
        private readonly FinancialReleaseService _financialReleaseService;
        public CashController(ILogger<CashController> logger, FinancialReleaseService financialReleaseService)
        {
            _logger = logger;
            _financialReleaseService = financialReleaseService;
        }

        /// <summary>
        /// Insert Cash Flow.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="financialReleaseStock">Data of cash flow</param>
        /// <returns></returns>
        /// <response code="201"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [ProducesResponseType(typeof(int), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<int>> Post(FinancialReleaseStock financialReleaseStock)
        {
            string correlationId = Guid.NewGuid().ToString();
            try
            {
                if(!ModelState.IsValid) 
                   return BadRequest(ModelState);
                if (!financialReleaseStock.IsValid())
                    return BadRequest("[StoreId,CashRegisterId] Fields must be greater than zero. [Date] must be valid and [Amount] cannot be zero");

                var result = await _financialReleaseService.LaunchNewCashFlow(financialReleaseStock, correlationId);

                if (result)
                    return Accepted(new { CorrelationId = correlationId, Key = financialReleaseStock.GetKey(), Date = DateTime.Now });
                return StatusCode(500, new { CorrelationId = correlationId, Error = "Failed to process", Date = DateTime.Now });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Sending the request ended up failing. {ex.Message} ");
                return StatusCode(500, new { CorrelationId = correlationId, Error = "An internal failure has occurred, please try again or contact administrator.", Date = DateTime.Now });
            }
        }
    }
}
