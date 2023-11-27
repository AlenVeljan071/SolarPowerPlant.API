using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarPowerPlant.API.Requests.PowerPlant;
using SolarPowerPlant.API.Services;
using SolarPowerPlant.Core.Responses;
using SolarPowerPlant.Core.Responses.PowerPlant;
using SolarPowerPlant.Core.Specifications.SolarPowerPlantSpecification;
using Swashbuckle.AspNetCore.Annotations;

namespace SolarPowerPlant.API.Controllers
{
    [Route("api/solarpowerplant")]
    [ApiController]
    [Authorize]
    public class SolarPowerPlantController : BaseApiController
    {
        private readonly SolarPowerPlantService _solarService;

        public SolarPowerPlantController(SolarPowerPlantService solarService, IServiceProvider provider) : base(provider)
        {
            _solarService = solarService;
        }

        [HttpPost("create")]
        [SwaggerOperation(Summary = "Create solar power plant.", Description = "Create solar power plant.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateResponse>> CreateAsync(SolarPowerPlantRequest request)
        {
            return Ok(await _solarService.CreateSolarPowerPlantAsync(request));
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update solar power plant.", Description = "Update solar power plant.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCommentAsync(UpdatePowerPlantRequest request)
        {
            return Ok(await _solarService.UpdateSolarPowerPlantAsync(request));

        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete solar power plant.", Description = "Delete solar power plant.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSolarPowerPlantAsync([FromRoute] int id)
        {
            var response = await _solarService.DeleteSolarPowerPlantByIdAsync(id);
            return NoContent();
        }


        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get solar power plant by id.", Description = "Get solar power plant by id.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SolarPowerPlantResponse>> GetSolarPowerPlantByIdAsync([FromRoute] int id)
        {
            return Ok(await _solarService.GetSolarPowerPlantByIdAsync(id));
        }

        [HttpGet("list")]
        [SwaggerOperation(Summary = "Get solar power plant list.", Description = "Get solar power plant list.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SolarPowerPlantListResponse>> GetSolarPowerPlantListAsync([FromQuery] SolarPowerPlantSpecParams request)
        {
            return Ok(await _solarService.GetSolarPowerPlantListAsync(request));
        }
      

    }
}
