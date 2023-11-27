using Microsoft.AspNetCore.Mvc;

namespace SolarPowerPlant.API.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public BaseApiController(IServiceProvider provider)
        {
            _provider = provider;
        }
    }
}
