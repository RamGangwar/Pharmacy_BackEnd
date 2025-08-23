using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.Mediciness
{
    public class DeleteMedicinesHandler : IRequestHandler<DeleteMedicinesCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DeleteMedicinesHandler> _logger;

        public DeleteMedicinesHandler(IUnitofWork unitofWork, ILogger<DeleteMedicinesHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel> Handle(DeleteMedicinesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var dept = await _unitofWork.Medicines.GetById(request.MedicineId);
            if (dept != null && dept.MedicineId > 0)
            {
                var res = await _unitofWork.Medicines.Delete(dept);
                return new ResponseModel { Message = "Delete Successfully", Succeeded = true };
            }
            return new ResponseModel { Message = "Medicines Not Found", Succeeded=false };
        }
    }
}

