using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Patientss
{
    public class GetPatientsByFilterHandler : IRequestHandler<GetPatientsByFilterQuery, PagingModel<PatientsVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetPatientsByFilterHandler> _logger;

        public GetPatientsByFilterHandler(IUnitofWork unitofWork, ILogger<GetPatientsByFilterHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<PagingModel<PatientsVM>> Handle(GetPatientsByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var result = await _unitofWork.Patients.GetByPaging(request);
            return new PagingModel<PatientsVM>(result, request.PageIndex, request.PageSize, result.Count() > 0 ? result.FirstOrDefault().TotalRecord : 0);
        }
    }
}

