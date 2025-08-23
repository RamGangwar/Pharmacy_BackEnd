using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.PurchaseOrders
{
    public class GetPurchaseOrderByIdHandler : IRequestHandler<GetPurchaseOrderByIdQuery, ResponseModel<PurchaseOrderVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetPurchaseOrderByIdHandler> _logger;
        private readonly IMediator _mediator;

        public GetPurchaseOrderByIdHandler(IUnitofWork unitofWork, ILogger<GetPurchaseOrderByIdHandler> logger, IMediator mediator)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ResponseModel<PurchaseOrderVM>> Handle(GetPurchaseOrderByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var depts = (await _unitofWork.PurchaseOrder.GetById(request.PurchaseId)).Adapt<PurchaseOrderVM>();
            return new ResponseModel<PurchaseOrderVM>
            {
                Data = depts,
                Succeeded = depts != null ? true : false,
                Message = depts != null ? "Record Found" : "No Record Found"
            };
        }
    }
}

