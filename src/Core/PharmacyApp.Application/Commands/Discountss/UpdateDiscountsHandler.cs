using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;

namespace PharmacyApp.Application.Commands.Discountss
{
    public class UpdateDiscountsHandler : IRequestHandler<UpdateDiscountsCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UpdateDiscountsHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateDiscountsHandler(IUnitofWork unitofWork, ILogger<UpdateDiscountsHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel> Handle(UpdateDiscountsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var deptDuplicate = await _unitofWork.Discounts.GetEntityAsync(a => a.DiscountId != request.DiscountId && a.StartDate == request.StartDate && a.EndDate == request.EndDate && a.MedicineId == request.MedicineId);
            if (deptDuplicate != null)
            {
                return new ResponseModel { Message = "Discounts Already Exists", Succeeded = false };
            }
            var dept = await _unitofWork.Discounts.GetById(request.DiscountId);
            if (dept != null && dept.DiscountId > 0)
            {
                dept.DiscountType = request.DiscountType;
                dept.DiscountValue = request.DiscountValue;
                
                var result = await _unitofWork.Discounts.Update(dept);
                if (result)
                {
                    return new ResponseModel { Message = "Updated Successfully", Succeeded = true };
                }
            }
            return new ResponseModel { Message = "Failed to update", Succeeded = false };
        }
    }
}

