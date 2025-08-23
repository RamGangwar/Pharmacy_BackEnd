using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;

namespace PharmacyApp.Application.Commands.Mediciness
{
    public class UpdateMedicinesHandler : IRequestHandler<UpdateMedicinesCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UpdateMedicinesHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateMedicinesHandler(IUnitofWork unitofWork, ILogger<UpdateMedicinesHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel> Handle(UpdateMedicinesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var deptDuplicate = await _unitofWork.Medicines.GetEntityAsync(u=>u.MedicineName==request.MedicineName && u.MedicineId!=request.MedicineId);
            if (deptDuplicate != null)
            {
                return new ResponseModel { Message = "Medicines Already Exists", Succeeded = false };
            }
            var dept = await _unitofWork.Medicines.GetById(request.MedicineId);
            if (dept != null && dept.MedicineId > 0)
            {
                dept.MedicineName = request.MedicineName;
                dept.GenericName = request.GenericName;
                dept.ExpiryDate = request.ExpiryDate;
                dept.Manufacturer = request.Manufacturer;
                dept.SupplierId = request.SupplierId;
                dept.Price = request.Price;
                dept.Category = request.Category;
                var result = await _unitofWork.Medicines.Update(dept);
                if (result)
                {
                    return new ResponseModel { Message = "Updated Successfully", Succeeded = true };
                }
            }
            return new ResponseModel {Message = "Failed to update",Succeeded=false};
        }
    }
}

