using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;

namespace PharmacyApp.Application.Commands.Inventorys
{
    public class UpdateInventoryHandler : IRequestHandler<UpdateInventoryCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UpdateInventoryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateInventoryHandler(IUnitofWork unitofWork, ILogger<UpdateInventoryHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);

            var dept = await _unitofWork.Inventory.GetById(request.InventoryId);
            if (dept != null && dept.InventoryId > 0)
            {
                dept.MedicineId = request.MedicineId;
                dept.Quantity = request.Quantity;
                dept.SourceNumber = request.SourceNumber;
                dept.Type = request.Type;
                var result = await _unitofWork.Inventory.Update(dept);
                if (result)
                {
                    return new ResponseModel { Message = "Updated Successfully", Succeeded = true };
                }
            }
            return new ResponseModel { Message = "Failed to update", Succeeded = false };
        }
    }
}

