using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.SalesDetails
{
    public class GetSalesDetailByIdHandler : IRequestHandler<GetSalesDetailByIdQuery, ResponseModel<SalesDetailVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetSalesDetailByIdHandler> _logger;
        private readonly IMediator _mediator;

        public GetSalesDetailByIdHandler(IUnitofWork unitofWork, ILogger<GetSalesDetailByIdHandler> logger, IMediator mediator)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ResponseModel<SalesDetailVM>> Handle(GetSalesDetailByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var depts = (await _unitofWork.SalesDetail.GetById(request.DetailId)).Adapt<SalesDetailVM>();
            return new ResponseModel<SalesDetailVM>
            {
                Data = depts,
                Succeeded = depts != null ? true : false,
                Message = depts != null ? "Record Found" : "No Record Found"
            };
        }
    }
}

