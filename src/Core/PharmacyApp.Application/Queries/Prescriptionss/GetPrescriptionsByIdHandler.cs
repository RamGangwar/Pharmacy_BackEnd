using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.Prescriptionss
{
    public class GetPrescriptionsByIdHandler : IRequestHandler<GetPrescriptionsByIdQuery, ResponseModel<PrescriptionsVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetPrescriptionsByIdHandler> _logger;
        private readonly IMediator _mediator;

        public GetPrescriptionsByIdHandler(IUnitofWork unitofWork, ILogger<GetPrescriptionsByIdHandler> logger, IMediator mediator)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ResponseModel<PrescriptionsVM>> Handle(GetPrescriptionsByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var depts = (await _unitofWork.Prescriptions.GetById(request.prescription_id)).Adapt<PrescriptionsVM>();
            return new ResponseModel<PrescriptionsVM>
            {
                Data = depts,
                Succeeded = depts != null ? true : false,
                Message = depts != null ? "Record Found" : "No Record Found"
            };
        }
    }
}

