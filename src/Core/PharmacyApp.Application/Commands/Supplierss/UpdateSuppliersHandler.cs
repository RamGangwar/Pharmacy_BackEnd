using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;

namespace PharmacyApp.Application.Commands.Supplierss
{
    public class UpdateSuppliersHandler : IRequestHandler<UpdateSuppliersCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UpdateSuppliersHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateSuppliersHandler(IUnitofWork unitofWork, ILogger<UpdateSuppliersHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel> Handle(UpdateSuppliersCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var deptDuplicate = await _unitofWork.Suppliers.GetEntityAsync(u => u.CompanyName == request.CompanyName && u.SupplierId != request.SupplierId);
            if (deptDuplicate != null)
            {
                return new ResponseModel { Message = "Suppliers Already Exists", Succeeded = false };
            }
            var dept = await _unitofWork.Suppliers.GetById(request.SupplierId);
            if (dept != null && dept.SupplierId > 0)
            {
                dept.CompanyName = request.CompanyName;
                dept.ContactName = request.ContactName;
                dept.Email = request.Email;
                dept.MobileNo = request.MobileNo;
                dept.Address = request.Address;

                var result = await _unitofWork.Suppliers.Update(dept);
                if (result)
                {
                    return new ResponseModel { Message = "Updated Successfully", Succeeded = true, StatusCode = 1 };
                }
            }
            return new ResponseModel { Message = "Failed to update", Succeeded = false, StatusCode = 0 };
        }
    }
}

