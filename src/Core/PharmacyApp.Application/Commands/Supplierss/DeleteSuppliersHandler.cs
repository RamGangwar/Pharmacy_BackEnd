using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.Supplierss
{
    public class DeleteSuppliersHandler : IRequestHandler<DeleteSuppliersCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DeleteSuppliersHandler> _logger;

        public DeleteSuppliersHandler(IUnitofWork unitofWork, ILogger<DeleteSuppliersHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel> Handle(DeleteSuppliersCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var dept = await _unitofWork.Suppliers.GetById(request.SupplierId);
            if (dept != null && dept.SupplierId > 0)
            {
                var res = await _unitofWork.Suppliers.Delete(dept);
                return new ResponseModel { Message = "Delete Successfully", Succeeded = true };
            }
            return new ResponseModel { Message = "Suppliers Not Found", Succeeded=false };
        }
    }
}

