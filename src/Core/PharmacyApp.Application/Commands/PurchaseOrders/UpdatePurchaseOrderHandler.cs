using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;

namespace PharmacyApp.Application.Commands.PurchaseOrders
{
    public class UpdatePurchaseOrderHandler : IRequestHandler<UpdatePurchaseOrderCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UpdatePurchaseOrderHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdatePurchaseOrderHandler(IUnitofWork unitofWork, ILogger<UpdatePurchaseOrderHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel> Handle(UpdatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var deptDuplicate = await _unitofWork.PurchaseOrder.GetEntityAsync(a => a.PurchaseNumber == request.PurchaseNumber && a.PurchaseId != request.PurchaseId);
            if (deptDuplicate != null)
            {
                return new ResponseModel { Message = "Purchase Order Already Exists", Succeeded = false };
            }
            var dept = await _unitofWork.PurchaseOrder.GetById(request.PurchaseId);
            if (dept != null && dept.PurchaseId > 0)
            {
                dept.SupplierId = request.SupplierId;
                dept.TotalAmount = request.TotalAmount;
                var result = await _unitofWork.PurchaseOrder.Update(dept);
                if (result)
                {
                    return new ResponseModel { Message = "Updated Successfully", Succeeded = true };
                }
            }
            return new ResponseModel { Message = "Failed to update", Succeeded = false };
        }
    }
}

