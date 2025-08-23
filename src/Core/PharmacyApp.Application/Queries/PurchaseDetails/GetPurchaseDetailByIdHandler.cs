using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.PurchaseDetails
{
    public class GetPurchaseDetailByIdHandler : IRequestHandler<GetPurchaseDetailByIdQuery, ResponseModel<PurchaseDetailVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetPurchaseDetailByIdHandler> _logger;
        private readonly IMediator _mediator;

        public GetPurchaseDetailByIdHandler(IUnitofWork unitofWork, ILogger<GetPurchaseDetailByIdHandler> logger, IMediator mediator)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ResponseModel<PurchaseDetailVM>> Handle(GetPurchaseDetailByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var depts = (await _unitofWork.PurchaseDetail.GetById(request.DetailId)).Adapt<PurchaseDetailVM>();
            return new ResponseModel<PurchaseDetailVM>
            {
                Data = depts,
                Succeeded = depts != null ? true : false,
                Message = depts != null ? "Record Found" : "No Record Found"
            };
        }
    }
}

