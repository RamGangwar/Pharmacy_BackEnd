using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.Paymentss
{
    public class DeletePaymentsHandler : IRequestHandler<DeletePaymentsCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DeletePaymentsHandler> _logger;

        public DeletePaymentsHandler(IUnitofWork unitofWork, ILogger<DeletePaymentsHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel> Handle(DeletePaymentsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var dept = await _unitofWork.Payments.GetById(request.PaymentId);
            if (dept != null && dept.PaymentId > 0)
            {
                var res = await _unitofWork.Payments.Delete(dept);
                return new ResponseModel { Message = "Delete Successfully", Succeeded = true };
            }
            return new ResponseModel { Message = "Payments Not Found", Succeeded = false };
        }
    }
}

