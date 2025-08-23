using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.Inventorys
{
    public class GetInventoryByIdHandler : IRequestHandler<GetInventoryByIdQuery, ResponseModel<InventoryVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetInventoryByIdHandler> _logger;
        private readonly IMediator _mediator;

        public GetInventoryByIdHandler(IUnitofWork unitofWork, ILogger<GetInventoryByIdHandler> logger, IMediator mediator)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ResponseModel<InventoryVM>> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
//            var depts = (await _unitofWork.Inventorys.GetById(request.InventoryId)).Adapt<InventoryVM>();
//            return new ResponseModel<InventoryVM> 
//            { 
//              Data = depts, Succeeded = depts != null ? true : false, Message = depts != null ? "Record Found":"No Record Found" 
//           };
            throw new NotImplementedException();
        }
    }
}

