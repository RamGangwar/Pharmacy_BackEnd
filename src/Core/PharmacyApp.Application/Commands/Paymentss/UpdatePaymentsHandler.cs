using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;

namespace PharmacyApp.Application.Commands.Paymentss
{
    public class UpdatePaymentsHandler : IRequestHandler<UpdatePaymentsCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UpdatePaymentsHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdatePaymentsHandler(IUnitofWork unitofWork, ILogger<UpdatePaymentsHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel> Handle(UpdatePaymentsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var deptDuplicate = await _unitofWork.Payments.GetEntityAsync(a => a.HeaderId == request.HeaderId && a.PaymentId != request.PaymentId);
            if (deptDuplicate != null)
            {
                return new ResponseModel { Message = "Payments Already Exists", Succeeded = false };
            }
            var dept = await _unitofWork.Payments.GetById(request.PaymentId);
            if (dept != null && dept.PaymentId > 0)
            {
                dept.AmountPaid = request.AmountPaid;
                dept.PaymentMethod = request.PaymentMethod;

                var result = await _unitofWork.Payments.Update(dept);
                if (result)
                {
                    return new ResponseModel { Message = "Updated Successfully", Succeeded = true };
                }
            }
            return new ResponseModel { Message = "Failed to update", Succeeded = false };
        }
    }
}

