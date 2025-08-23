using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.SalesDetails
{
    public class GetSalesDetailByFilterHandler : IRequestHandler<GetSalesDetailByFilterQuery, PagingModel<SalesDetailVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetSalesDetailByFilterHandler> _logger;

        public GetSalesDetailByFilterHandler(IUnitofWork unitofWork, ILogger<GetSalesDetailByFilterHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<PagingModel<SalesDetailVM>> Handle(GetSalesDetailByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var result = await _unitofWork.SalesDetail.GetByPaging(request);
            return new PagingModel<SalesDetailVM>(result, request.PageIndex, request.PageSize, result.Count() > 0 ? result.FirstOrDefault().TotalRecord : 0);
        }
    }
}

