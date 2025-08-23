using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Supplierss
{
    public class GetSuppliersByFilterHandler : IRequestHandler<GetSuppliersByFilterQuery, PagingModel<SuppliersVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetSuppliersByFilterHandler> _logger;

        public GetSuppliersByFilterHandler(IUnitofWork unitofWork, ILogger<GetSuppliersByFilterHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<PagingModel<SuppliersVM>> Handle(GetSuppliersByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var result = await _unitofWork.Suppliers.GetByPaging(request);
            return new PagingModel<SuppliersVM>(result, request.PageIndex, request.PageSize, result.Count() > 0 ? result.FirstOrDefault().TotalRecord : 0);

        }
    }
}

