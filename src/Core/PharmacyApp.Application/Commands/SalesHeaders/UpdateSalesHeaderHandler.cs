using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;

namespace PharmacyApp.Application.Commands.SalesHeaders
{
    public class UpdateSalesHeaderHandler : IRequestHandler<UpdateSalesHeaderCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UpdateSalesHeaderHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateSalesHeaderHandler(IUnitofWork unitofWork, ILogger<UpdateSalesHeaderHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel> Handle(UpdateSalesHeaderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var deptDuplicate = await _unitofWork.SalesHeader.GetEntityAsync(a => a.HeaderNumber == request.HeaderNUmber && a.HeaderId != request.HeaderId);
            if (deptDuplicate != null)
            {
                return new ResponseModel { Message = "SalesHeader Already Exists", Succeeded = false };
            }
            var dept = await _unitofWork.SalesHeader.GetById(request.HeaderId);
            if (dept != null && dept.HeaderId > 0)
            {
                dept.PatientId = request.PatientId;
                dept.TotalAmount = request.TotalAmount;
                dept.Discount = request.Discount;
                var result = await _unitofWork.SalesHeader.Update(dept);
                if (result)
                {
                    return new ResponseModel { Message = "Updated Successfully", Succeeded = true };
                }
            }
            return new ResponseModel { Message = "Failed to update", Succeeded = false };
        }
    }
}

