using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.Discountss
{
    public class GetDiscountsByIdHandler : IRequestHandler<GetDiscountsByIdQuery, ResponseModel<DiscountsVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetDiscountsByIdHandler> _logger;
        private readonly IMediator _mediator;

        public GetDiscountsByIdHandler(IUnitofWork unitofWork, ILogger<GetDiscountsByIdHandler> logger, IMediator mediator)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ResponseModel<DiscountsVM>> Handle(GetDiscountsByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var depts = (await _unitofWork.Discounts.GetById(request.DiscountId)).Adapt<DiscountsVM>();
            return new ResponseModel<DiscountsVM>
            {
                Data = depts,
                Succeeded = depts != null ? true : false,
                Message = depts != null ? "Record Found" : "No Record Found"
            };
        }
    }
}

