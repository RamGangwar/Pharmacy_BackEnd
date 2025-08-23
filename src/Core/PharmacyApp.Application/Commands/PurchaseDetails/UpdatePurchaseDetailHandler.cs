using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;

namespace PharmacyApp.Application.Commands.PurchaseDetails
{
    public class UpdatePurchaseDetailHandler : IRequestHandler<UpdatePurchaseDetailCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UpdatePurchaseDetailHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdatePurchaseDetailHandler(IUnitofWork unitofWork, ILogger<UpdatePurchaseDetailHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel> Handle(UpdatePurchaseDetailCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var deptDuplicate = await _unitofWork.PurchaseDetail.GetEntityAsync(a => a.PurchaseId == request.PurchaseId && a.MedicineId == request.MedicineId && a.PurchaseId != request.DetailId);
            if (deptDuplicate != null)
            {
                return new ResponseModel { Message = "PurchaseDetail Already Exists", Succeeded = false };
            }
            var dept = await _unitofWork.PurchaseDetail.GetById(request.DetailId);
            if (dept != null && dept.DetailId > 0)
            {
                dept.MedicineId = request.MedicineId;
                dept.Quantity = request.Quantity;
                dept.Price = request.Price;
                var result = await _unitofWork.PurchaseDetail.Update(dept);
                if (result)
                {
                    return new ResponseModel { Message = "Updated Successfully", Succeeded = true };
                }
            }
            return new ResponseModel { Message = "Failed to update", Succeeded = false };
        }
    }
}

