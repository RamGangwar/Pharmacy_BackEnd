using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Inventorys
{
    public class GetInventoryByFilterHandler : IRequestHandler<GetInventoryByFilterQuery, PagingModel<InventoryVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetInventoryByFilterHandler> _logger;

        public GetInventoryByFilterHandler(IUnitofWork unitofWork, ILogger<GetInventoryByFilterHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<PagingModel<InventoryVM>> Handle(GetInventoryByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var result = await _unitofWork.Inventory.GetByPaging(request);
            return new PagingModel<InventoryVM>(result, request.PageIndex, request.PageSize, result.Count() > 0 ? result.FirstOrDefault().TotalRecord : 0);
            throw new NotImplementedException();
        }
    }
}

