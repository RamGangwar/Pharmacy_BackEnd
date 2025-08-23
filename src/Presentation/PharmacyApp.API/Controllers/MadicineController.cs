using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyApp.Application.Commands.Mediciness;
using PharmacyApp.Application.Queries.Mediciness;
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
    public class MadicineController : ControllerBase
    {
        private readonly ILogger<MadicineController> _logger;
        private readonly IMediator _mediator;
        private readonly IUnitofWork _unitofwork;

        public MadicineController(ILogger<MadicineController> logger, IMediator mediator, IUnitofWork unitofwork)
        {
            _logger = logger;
            _mediator = mediator;
            _unitofwork = unitofwork;
        }

        [ProducesResponseType(typeof(ResponseModel<MedicinesVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("AddMedicine")]
        public async Task<IActionResult> AddMedicine([FromBody] CreateMedicinesCommand request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(AddMedicine), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpPatch]
        [Route("UpdateMedicine")]
        public async Task<IActionResult> UpdateMedicine([FromBody] UpdateMedicinesCommand request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(UpdateMedicine), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpDelete]
        [Route("DeleteMedicine")]
        public async Task<IActionResult> DeleteMedicine([FromQuery] DeleteMedicinesCommand request )
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(DeleteMedicine), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
        [ProducesResponseType(typeof(ResponseModel<MedicinesVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("GetMedicineById")]
        public async Task<IActionResult> GetMedicineById([FromQuery] GetMedicinesByIdQuery request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(GetMedicineById), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }

        [ProducesResponseType(typeof(PagingModel<MedicinesVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("GetMedicineList")]
        public async Task<IActionResult> GetMedicineList([FromQuery] GetMedicinesByFilterQuery request)
        {
            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(GetMedicineList), request);
            var emploist = await _mediator.Send(request);
            return Ok(emploist);
        }
    }

}
