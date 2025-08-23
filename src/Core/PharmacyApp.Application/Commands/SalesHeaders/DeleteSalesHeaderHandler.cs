using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.SalesHeaders
{
    public class DeleteSalesHeaderHandler : IRequestHandler<DeleteSalesHeaderCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DeleteSalesHeaderHandler> _logger;

        public DeleteSalesHeaderHandler(IUnitofWork unitofWork, ILogger<DeleteSalesHeaderHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel> Handle(DeleteSalesHeaderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var dept = await _unitofWork.SalesHeader.GetById(request.HeaderId);
            if (dept != null && dept.HeaderId > 0)
            {
                var res = await _unitofWork.SalesHeader.Delete(dept);
                return new ResponseModel { Message = "Delete Successfully", Succeeded = true };
            }
            return new ResponseModel { Message = "SalesHeader Not Found", Succeeded = false };
        }
    }
}

