using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarPowerPlant.Core.Interfaces;
using SolarPowerPlant.Core.Responses.TimeSeries;
using SolarPowerPlant.Core.Specifications.TimeSeriesSpecification;
using Swashbuckle.AspNetCore.Annotations;

namespace SolarPowerPlant.API.Controllers
{
    [Route("api/timeseries")]
    [ApiController]
    [Authorize]
    public class TimeSeriesController : ControllerBase
    {
        private readonly ITimeSeriesService _timeSeriesService;
       
        public TimeSeriesController(ITimeSeriesService timeSeriesService)
        {
            _timeSeriesService = timeSeriesService;
        }

        [HttpGet("list")]
        [SwaggerOperation(Summary = "Get time series list.", Description = "Get time series list by solar power plant id.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TimeSeriesListResponse>> GetTimeSeriesListAsync([FromQuery] TimeSeriesSpecParams request)
        {
            return Ok(await _timeSeriesService.GetTimeSeriesListAsync(request));
        }
    }
}
