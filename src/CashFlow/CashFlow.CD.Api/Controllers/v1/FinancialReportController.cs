using CashFlow.CD.Api.model;
using CashFlow.CD.API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CashFlow.RC.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class FinancialReportController : ControllerBase
    {
        private readonly ILogger<FinancialReportController> _logger;
        private readonly IFinancialReportService _financialReportService;
        public FinancialReportController(
            ILogger<FinancialReportController> logger, 
            IFinancialReportService financialReportService)
        {
            _logger = logger;
            _financialReportService = financialReportService;
        }

        /// <summary>
        /// Get Report Cash Flow.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="day">day of report</param>
        /// /// <param name="month">month of report flow</param>
        /// /// <param name="year">year of report flow</param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="204"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [ProducesResponseType(typeof(FinancialReportStock), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{day}/{month}/{year}")]
        public async Task<ActionResult<FinancialReportStock>> Get(int? day, int? month, int? year)
        {
            //Day month Year
            string correlationId = Guid.NewGuid().ToString();
            try
            {
                if (day == null || month == null || year == null)
                    return BadRequest("Day, Month and Year are mandatory parameters");

                if(day <= 0 || day > 31)
                    return BadRequest("Day is invalid");

                if (month <= 0 || month > 12)
                    return BadRequest("Month is invalid");

                if (year <= 0 || year > DateTime.Now.Year)
                    return BadRequest("Year is invalid");

                DateOnly dateOnly = new DateOnly((int)year, (int)month, (int)day);
                var financialReportConsolidateDaily = await _financialReportService.GetReport(dateOnly);

                if (financialReportConsolidateDaily is null)
                    return NoContent();

                var report = FinancialReportStock.Convert(financialReportConsolidateDaily);
                return Ok(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Sending the request ended up failing. {ex.Message} ");
                return StatusCode(500, new { CorrelationId = correlationId, Error = "An internal failure has occurred, please try again or contact administrator.", Date = DateTime.Now });
            }
        }
    }
}
