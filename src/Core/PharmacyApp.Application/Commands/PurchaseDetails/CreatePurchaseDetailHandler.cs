using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.Commands.Inventorys;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.PurchaseDetails
{
    public class CreatePurchaseDetailHandler : IRequestHandler<CreatePurchaseDetailCommand, ResponseModel<PurchaseDetailVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CreatePurchaseDetailHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreatePurchaseDetailHandler(IUnitofWork unitofWork, ILogger<CreatePurchaseDetailHandler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<PurchaseDetailVM>> Handle(CreatePurchaseDetailCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation(nameof(Handle), request);
            var response = new ResponseModel<PurchaseDetailVM>();
            var dept = await _unitofWork.PurchaseDetail.GetEntityAsync(a => a.PurchaseId == request.PurchaseId && a.MedicineId==request.MedicineId);
            if (dept == null)
            {
                var model = request.Adapt<PurchaseDetail>();
                var result = await _unitofWork.PurchaseDetail.Add(model);
                if (result > 0)
                {
                    var res = await _unitofWork.PurchaseOrder.GetById(request.PurchaseId);
                    var invcommnd = new CreateInventoryCommand();
                    invcommnd.SourceNumber = res.PurchaseNumber;
                    invcommnd.MedicineId = request.MedicineId;
                    invcommnd.Type = "Purchase";
                    invcommnd.Quantity = request.Quantity;
                    invcommnd.TransactionDate = DateTime.Now;

                    var invres = await _mediator.Send(invcommnd);

                    response.Succeeded = true;
                    response.Message = "Saved Successfully.";
                    return response;
                }
                else
                {
                    response.Succeeded = false;
                    response.Message = "Failed to save.";
                    return response;
                }
            }
            else
            {
                response.Succeeded = false;
                response.Message = "Purchase Detail Already Exists.";
                return response;
            }
        }
    }
}

