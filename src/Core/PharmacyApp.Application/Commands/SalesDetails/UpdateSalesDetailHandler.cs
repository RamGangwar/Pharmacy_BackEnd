using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;

namespace PharmacyApp.Application.Commands.SalesDetails
{
    public class UpdateSalesDetailHandler : IRequestHandler<UpdateSalesDetailCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UpdateSalesDetailHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateSalesDetailHandler(IUnitofWork unitofWork, ILogger<UpdateSalesDetailHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel> Handle(UpdateSalesDetailCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
           
            var dept = await _unitofWork.SalesDetail.GetById(request.DetailId);
            if (dept != null && dept.DetailId > 0)
            {
                dept.MedicineId = request.MedicineId;
                dept.Quantity = request.Quantity;
                dept.Price = request.Price;
                var result = await _unitofWork.SalesDetail.Update(dept);
                if (result)
                {
                    return new ResponseModel { Message = "Updated Successfully", Succeeded = true };
                }
            }
            return new ResponseModel {Message = "Failed to update",Succeeded=false};
        }
    }
}

