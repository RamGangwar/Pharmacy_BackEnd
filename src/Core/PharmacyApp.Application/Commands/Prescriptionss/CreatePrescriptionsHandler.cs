using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Prescriptionss
{
    public class CreatePrescriptionsHandler : IRequestHandler<CreatePrescriptionsCommand, ResponseModel<PrescriptionsVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CreatePrescriptionsHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreatePrescriptionsHandler(IUnitofWork unitofWork, ILogger<CreatePrescriptionsHandler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<PrescriptionsVM>> Handle(CreatePrescriptionsCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation(nameof(Handle), request);
            var response = new ResponseModel<PrescriptionsVM>();
            //var dept = await _unitofWork.Prescriptions.GetEntityAsync(a => a.PrescriptionsName == request.PrescriptionsName);
            //if (dept == null)
            //{
            var model = request.Adapt<Prescriptions>();
            var result = await _unitofWork.Prescriptions.Add(model);
            if (result > 0)
            {
                var res = await _unitofWork.Prescriptions.GetById(result);
                response.Data = res.Adapt<PrescriptionsVM>();
                response.Succeeded = true;
                response.Message = "Saved Successfully.";
                return response;
            }
            else
            {
                response.Succeeded = false;
                response.Message = "Failed to save.";
                return response;
            }
            //}
            //else
            //{
            //    response.Succeeded = false;
            //    response.Message = "Prescriptions Already Exists.";
            //    return response;
            //}
        }
    }
}

