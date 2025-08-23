using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.PurchaseDetails
{
    public class DeletePurchaseDetailHandler : IRequestHandler<DeletePurchaseDetailCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DeletePurchaseDetailHandler> _logger;

        public DeletePurchaseDetailHandler(IUnitofWork unitofWork, ILogger<DeletePurchaseDetailHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel> Handle(DeletePurchaseDetailCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var dept = await _unitofWork.PurchaseDetail.GetById(request.DetailId);
            if (dept != null && dept.DetailId > 0)
            {
                var res = await _unitofWork.PurchaseDetail.Delete(dept);
                return new ResponseModel { Message = "Delete Successfully", Succeeded = true };
            }
            return new ResponseModel { Message = "Detail Not Found", Succeeded = false };
        }
    }
}

