using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.PurchaseDetails
{
    public class GetPurchaseDetailByFilterHandler : IRequestHandler<GetPurchaseDetailByFilterQuery, PagingModel<PurchaseDetailVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetPurchaseDetailByFilterHandler> _logger;

        public GetPurchaseDetailByFilterHandler(IUnitofWork unitofWork, ILogger<GetPurchaseDetailByFilterHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<PagingModel<PurchaseDetailVM>> Handle(GetPurchaseDetailByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var result = await _unitofWork.PurchaseDetail.GetByPaging(request);
            return new PagingModel<PurchaseDetailVM>(result, request.PageIndex, request.PageSize, result.Count() > 0 ? result.FirstOrDefault().TotalRecord : 0);
        }
    }
}

