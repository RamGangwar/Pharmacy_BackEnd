using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyApp.Application.Commands.Authentication;
using PharmacyApp.Application.Queries.Modules;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.Model.AuthenticationModel;
using System.Net;

namespace PharmacyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly IUnitofWork _unitofwork;

        public HomeController(ILogger<HomeController> logger, IMediator mediator, IUnitofWork unitofwork)
        {
            _logger = logger;
            _mediator = mediator;
            _unitofwork = unitofwork;
        }

        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticationResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(Login), request);
            request.IpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }

        [ProducesResponseType(typeof(AuthenticationResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(ChangePassword), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }

        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticationResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ChangePasswordRequest request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(ChangePassword), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }

        [HttpPost("RefreshToken")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<AuthenticationResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(RefreshToken), request);
            if (!ModelState.IsValid)
                return BadRequest(new AuthenticationResponse { Succeeded = false, Message = "Tokens must be provided" });
            request.IpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var res = await _mediator.Send(request);
            return Ok(res);

        }

        //[AllowAnonymous]
        [HttpGet]
        [Route("ModuleList")]
        public async Task<IActionResult> ModuleList([FromQuery]GetModulesByFilterQuery request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(ModuleList), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
    }
}
