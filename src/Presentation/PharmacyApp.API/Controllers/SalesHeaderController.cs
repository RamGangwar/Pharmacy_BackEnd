using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyApp.Application.Commands.SalesHeaders;
using PharmacyApp.Application.Queries.SalesHeaders;
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
    public class SalesHeaderController : ControllerBase
    {
        private readonly ILogger<SalesHeaderController> _logger;
        private readonly IMediator _mediator;
        private readonly IUnitofWork _unitofwork;

        public SalesHeaderController(ILogger<SalesHeaderController> logger, IMediator mediator, IUnitofWork unitofwork)
        {
            _logger = logger;
            _mediator = mediator;
            _unitofwork = unitofwork;
        }

        [ProducesResponseType(typeof(ResponseModel<SalesHeaderVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("AddSalesHeader")]
        public async Task<IActionResult> AddSalesHeader([FromBody] CreateSalesHeaderCommand request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(AddSalesHeader), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpPatch]
        [Route("UpdateSalesHeader")]
        public async Task<IActionResult> UpdateSalesHeader([FromBody] UpdateSalesHeaderCommand request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(UpdateSalesHeader), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpDelete]
        [Route("DeleteSalesHeader")]
        public async Task<IActionResult> DeleteSalesHeader([FromQuery] DeleteSalesHeaderCommand request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(DeleteSalesHeader), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
        [ProducesResponseType(typeof(ResponseModel<SalesHeaderVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("GetSalesHeaderById")]
        public async Task<IActionResult> GetSalesHeaderById([FromQuery] GetSalesHeaderByIdQuery request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(GetSalesHeaderById), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }

        [ProducesResponseType(typeof(PagingModel<SalesHeaderVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("GetSalesHeaderList")]
        public async Task<IActionResult> GetSalesHeaderList([FromQuery] GetSalesHeaderByFilterQuery request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(GetSalesHeaderList), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
    }
}
