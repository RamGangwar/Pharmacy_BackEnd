using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.Inventorys
{
    public class DeleteInventoryHandler : IRequestHandler<DeleteInventoryCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DeleteInventoryHandler> _logger;

        public DeleteInventoryHandler(IUnitofWork unitofWork, ILogger<DeleteInventoryHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var dept = await _unitofWork.Inventory.GetById(request.InventoryId);
            if (dept != null && dept.InventoryId > 0)
            {
                var res = await _unitofWork.Inventory.Delete(dept);
                return new ResponseModel { Message = "Delete Successfully", Succeeded = true };
            }
            return new ResponseModel { Message = "Inventory Not Found", Succeeded=false };
        }
    }
}

