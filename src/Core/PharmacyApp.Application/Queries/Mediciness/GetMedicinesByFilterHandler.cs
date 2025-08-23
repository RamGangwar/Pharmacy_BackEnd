using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Mediciness
{
    public class GetMedicinesByFilterHandler : IRequestHandler<GetMedicinesByFilterQuery, PagingModel<MedicinesVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetMedicinesByFilterHandler> _logger;

        public GetMedicinesByFilterHandler(IUnitofWork unitofWork, ILogger<GetMedicinesByFilterHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<PagingModel<MedicinesVM>> Handle(GetMedicinesByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var result = await _unitofWork.Medicines.GetByPaging(request);
            return new PagingModel<MedicinesVM>(result, request.PageIndex, request.PageSize, result.Count() > 0 ? result.FirstOrDefault().TotalRecord : 0);
        }
    }
}

