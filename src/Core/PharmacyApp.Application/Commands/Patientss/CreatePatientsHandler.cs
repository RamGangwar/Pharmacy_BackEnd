using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Patientss
{
    public class CreatePatientsHandler : IRequestHandler<CreatePatientsCommand, ResponseModel<PatientsVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CreatePatientsHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreatePatientsHandler(IUnitofWork unitofWork, ILogger<CreatePatientsHandler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<PatientsVM>> Handle(CreatePatientsCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation(nameof(Handle), request);
            var response = new ResponseModel<PatientsVM>();
            var dept = await _unitofWork.Patients.GetEntityAsync(u => u.MobileNo == request.MobileNo);
            if (dept == null)
            {
                var model = request.Adapt<Patients>();
                model.Reg_Date = DateTime.Now;
                var result = await _unitofWork.Patients.Add(model);
                if (result > 0)
                {
                    var res = await _unitofWork.Patients.GetById(result);
                    response.Data = res.Adapt<PatientsVM>();
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
            }
            else
            {
                response.Succeeded = false;
                response.Message = "Patients Already Exists.";
                return response;
            }
            return response;
        }
    }
}

