using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.PurchaseOrders
{
    public class GetPurchaseOrderByFilterHandler : IRequestHandler<GetPurchaseOrderByFilterQuery, PagingModel<PurchaseOrderVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetPurchaseOrderByFilterHandler> _logger;

        public GetPurchaseOrderByFilterHandler(IUnitofWork unitofWork, ILogger<GetPurchaseOrderByFilterHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<PagingModel<PurchaseOrderVM>> Handle(GetPurchaseOrderByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var result = await _unitofWork.PurchaseOrder.GetByPaging(request);
            return new PagingModel<PurchaseOrderVM>(result, request.PageIndex, request.PageSize, result.Count() > 0 ? result.FirstOrDefault().TotalRecord : 0);
        }
    }
}

