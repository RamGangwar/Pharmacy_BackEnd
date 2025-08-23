using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.PurchaseOrders
{
    public class DeletePurchaseOrderHandler : IRequestHandler<DeletePurchaseOrderCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DeletePurchaseOrderHandler> _logger;

        public DeletePurchaseOrderHandler(IUnitofWork unitofWork, ILogger<DeletePurchaseOrderHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel> Handle(DeletePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var dept = await _unitofWork.PurchaseOrder.GetById(request.PurchaseId);
            if (dept != null && dept.PurchaseId > 0)
            {
                var res = await _unitofWork.PurchaseOrder.Delete(dept);
                return new ResponseModel { Message = "Delete Successfully", Succeeded = true };
            }
            return new ResponseModel { Message = "PurchaseOrder Not Found", Succeeded=false };
        }
    }
}

