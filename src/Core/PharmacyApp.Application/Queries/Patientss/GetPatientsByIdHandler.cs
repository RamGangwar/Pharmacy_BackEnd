using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.Patientss
{
    public class GetPatientsByIdHandler : IRequestHandler<GetPatientsByIdQuery, ResponseModel<PatientsVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetPatientsByIdHandler> _logger;
        private readonly IMediator _mediator;

        public GetPatientsByIdHandler(IUnitofWork unitofWork, ILogger<GetPatientsByIdHandler> logger, IMediator mediator)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ResponseModel<PatientsVM>> Handle(GetPatientsByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var depts = (await _unitofWork.Patients.GetById(request.PatientId)).Adapt<PatientsVM>();
            return new ResponseModel<PatientsVM>
            {
                Data = depts,
                Succeeded = depts != null ? true : false,
                Message = depts != null ? "Record Found" : "No Record Found"
            };
        }
    }
}

