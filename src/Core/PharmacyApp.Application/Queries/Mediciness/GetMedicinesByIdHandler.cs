using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.Mediciness
{
    public class GetMedicinesByIdHandler : IRequestHandler<GetMedicinesByIdQuery, ResponseModel<MedicinesVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetMedicinesByIdHandler> _logger;
        private readonly IMediator _mediator;

        public GetMedicinesByIdHandler(IUnitofWork unitofWork, ILogger<GetMedicinesByIdHandler> logger, IMediator mediator)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ResponseModel<MedicinesVM>> Handle(GetMedicinesByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var depts = (await _unitofWork.Medicines.GetById(request.MedicineId)).Adapt<MedicinesVM>();
            return new ResponseModel<MedicinesVM>
            {
                Data = depts,
                Succeeded = depts != null ? true : false,
                Message = depts != null ? "Record Found" : "No Record Found"
            };
        }
    }
}

