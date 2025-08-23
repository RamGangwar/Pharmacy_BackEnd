using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.SalesDetails
{
    public class DeleteSalesDetailHandler : IRequestHandler<DeleteSalesDetailCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DeleteSalesDetailHandler> _logger;

        public DeleteSalesDetailHandler(IUnitofWork unitofWork, ILogger<DeleteSalesDetailHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel> Handle(DeleteSalesDetailCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var dept = await _unitofWork.SalesDetail.GetById(request.DetailId);
            if (dept != null && dept.DetailId > 0)
            {
                var res = await _unitofWork.SalesDetail.Delete(dept);
                return new ResponseModel { Message = "Delete Successfully", Succeeded = true };
            }
            return new ResponseModel { Message = "SalesDetail Not Found", Succeeded = false };
        }
    }
}

