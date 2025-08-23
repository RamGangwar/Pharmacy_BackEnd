using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Discountss
{
    public class GetDiscountsByFilterHandler : IRequestHandler<GetDiscountsByFilterQuery, PagingModel<DiscountsVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetDiscountsByFilterHandler> _logger;

        public GetDiscountsByFilterHandler(IUnitofWork unitofWork, ILogger<GetDiscountsByFilterHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<PagingModel<DiscountsVM>> Handle(GetDiscountsByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var result = await _unitofWork.Discounts.GetByPaging(request);
            return new PagingModel<DiscountsVM>(result, request.PageIndex, request.PageSize, result.Count() > 0 ? result.FirstOrDefault().TotalRecord : 0);

        }
    }
}

