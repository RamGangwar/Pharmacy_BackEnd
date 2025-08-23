using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.SalesHeaders
{
    public class GetSalesHeaderByFilterHandler : IRequestHandler<GetSalesHeaderByFilterQuery, PagingModel<SalesHeaderVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetSalesHeaderByFilterHandler> _logger;

        public GetSalesHeaderByFilterHandler(IUnitofWork unitofWork, ILogger<GetSalesHeaderByFilterHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<PagingModel<SalesHeaderVM>> Handle(GetSalesHeaderByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var result = await _unitofWork.SalesHeader.GetByPaging(request);
            return new PagingModel<SalesHeaderVM>(result, request.PageIndex, request.PageSize, result.Count() > 0 ? result.FirstOrDefault().TotalRecord : 0);
        }
    }
}

