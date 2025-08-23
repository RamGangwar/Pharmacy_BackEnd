using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyApp.Application.Commands.SalesDetails;
using PharmacyApp.Application.Queries.SalesDetails;
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
    public class SalesDetailController : ControllerBase
    {
        private readonly ILogger<SalesDetailController> _logger;
        private readonly IMediator _mediator;
        private readonly IUnitofWork _unitofwork;

        public SalesDetailController(ILogger<SalesDetailController> logger, IMediator mediator, IUnitofWork unitofwork)
        {
            _logger = logger;
            _mediator = mediator;
            _unitofwork = unitofwork;
        }

        [ProducesResponseType(typeof(ResponseModel<SalesDetailVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("AddSalesDetail")]
        public async Task<IActionResult> AddSalesDetail([FromBody] CreateSalesDetailCommand request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(AddSalesDetail), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpPatch]
        [Route("UpdateSalesDetail")]
        public async Task<IActionResult> UpdateSalesDetail([FromBody] UpdateSalesDetailCommand request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(UpdateSalesDetail), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpDelete]
        [Route("DeleteSalesDetail")]
        public async Task<IActionResult> DeleteSalesDetail([FromQuery] DeleteSalesDetailCommand request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(DeleteSalesDetail), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
        [ProducesResponseType(typeof(ResponseModel<SalesDetailVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("GetSalesDetailById")]
        public async Task<IActionResult> GetSalesDetailById([FromQuery] GetSalesDetailByIdQuery request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(GetSalesDetailById), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }

        [ProducesResponseType(typeof(PagingModel<SalesDetailVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("GetSalesDetailList")]
        public async Task<IActionResult> GetSalesDetailList([FromQuery] GetSalesDetailByFilterQuery request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(GetSalesDetailList), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
    }
}
