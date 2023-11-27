using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarPowerPlant.API.Requests.Account;
using SolarPowerPlant.API.Services;
using SolarPowerPlant.Core.Responses.Account;
using Swashbuckle.AspNetCore.Annotations;

namespace SolarPowerPlant.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    [Authorize]
    public class AccountController : BaseApiController
    {
        private readonly AccountService _accountService;
       
        public AccountController(AccountService accountService, IServiceProvider provider) : base(provider)
        {
            _accountService = accountService;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "User signup.", Description = "User signup.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SignupUserResponse>> SignupAsync(SignUserAccountRequest request)
        {
            return Ok(await _accountService.SignUserAccountAsync(request));
        }


        [HttpPost("login")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "User login.", Description = "User login with email and password.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest request)
        {
            return Ok(await _accountService.LoginAsync(request));
        }


        [HttpPost]
        [Route("refresh")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Refresh token.", Description = "Refresh token.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshAsync(RefreshTokenRequest request)
        {
            return Ok(await _accountService.RefreshTokenAsync(request));
        }
    }
}
