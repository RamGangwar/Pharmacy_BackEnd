using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyApp.Application.Commands.Roless;
using PharmacyApp.Application.Queries.Roless;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
using System.Net;

namespace PharmacyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IMediator _mediator;
        private readonly IUnitofWork _unitofwork;

        public RoleController(ILogger<RoleController> logger, IMediator mediator, IUnitofWork unitofwork)
        {
            _logger = logger;
            _mediator = mediator;
            _unitofwork = unitofwork;
        }

        [ProducesResponseType(typeof(ResponseModel<RolesVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] CreateRolesCommand request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(AddRole), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpPatch]
        [Route("UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRolesCommand request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(UpdateRole), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpDelete]
        [Route("DeleteRole")]
        public async Task<IActionResult> DeleteRole( [FromQuery] DeleteRolesCommand request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(DeleteRole), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
        [ProducesResponseType(typeof(ResponseModel<RolesVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("GetRoleById")]
        public async Task<IActionResult> GetRoleById([FromQuery] GetRolesByIdQuery request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(GetRoleById), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }

        [ProducesResponseType(typeof(PagingModel<RolesVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("GetRoleList")]
        public async Task<IActionResult> GetRoleList([FromQuery] GetRolesByFilterQuery request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(GetRoleList), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
    }
}
