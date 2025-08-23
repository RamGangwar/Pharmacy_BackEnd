using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyApp.Application.Commands.PurchaseDetails;
using PharmacyApp.Application.Queries.PurchaseDetails;
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
    public class PurchaseDetailController : ControllerBase
    {
        private readonly ILogger<PurchaseDetailController> _logger;
        private readonly IMediator _mediator;
        private readonly IUnitofWork _unitofwork;

        public PurchaseDetailController(ILogger<PurchaseDetailController> logger, IMediator mediator, IUnitofWork unitofwork)
        {
            _logger = logger;
            _mediator = mediator;
            _unitofwork = unitofwork;
        }

        [ProducesResponseType(typeof(ResponseModel<PurchaseDetailVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("AddPurchaseDetail")]
        public async Task<IActionResult> AddPurchaseDetail([FromBody] CreatePurchaseDetailCommand request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(AddPurchaseDetail), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpPatch]
        [Route("UpdatePurchaseDetail")]
        public async Task<IActionResult> UpdatePurchaseDetail([FromBody] UpdatePurchaseDetailCommand request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(UpdatePurchaseDetail), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpDelete]
        [Route("DeletePurchaseDetail")]
        public async Task<IActionResult> DeletePurchaseDetail([FromQuery] DeletePurchaseDetailCommand request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(DeletePurchaseDetail), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
        [ProducesResponseType(typeof(ResponseModel<PurchaseDetailVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("GetPurchaseDetailById")]
        public async Task<IActionResult> GetPurchaseDetailById([FromQuery] GetPurchaseDetailByIdQuery request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(GetPurchaseDetailById), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }

        [ProducesResponseType(typeof(PagingModel<PurchaseDetailVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("GetPurchaseDetailList")]
        public async Task<IActionResult> GetPurchaseDetailList([FromQuery] GetPurchaseDetailByFilterQuery request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(GetPurchaseDetailList), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
    }
}
