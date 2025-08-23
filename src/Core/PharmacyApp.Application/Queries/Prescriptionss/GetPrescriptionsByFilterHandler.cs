using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Prescriptionss
{
    public class GetPrescriptionsByFilterHandler : IRequestHandler<GetPrescriptionsByFilterQuery, PagingModel<PrescriptionsVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetPrescriptionsByFilterHandler> _logger;

        public GetPrescriptionsByFilterHandler(IUnitofWork unitofWork, ILogger<GetPrescriptionsByFilterHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<PagingModel<PrescriptionsVM>> Handle(GetPrescriptionsByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var result = await _unitofWork.Prescriptions.GetByPaging(request);
            return new PagingModel<PrescriptionsVM>(result, request.PageIndex, request.PageSize, result.Count() > 0 ? result.FirstOrDefault().TotalRecord : 0);
        }
    }
}

