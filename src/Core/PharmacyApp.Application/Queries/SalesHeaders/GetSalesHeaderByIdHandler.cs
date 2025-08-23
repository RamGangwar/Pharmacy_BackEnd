using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.SalesHeaders
{
    public class GetSalesHeaderByIdHandler : IRequestHandler<GetSalesHeaderByIdQuery, ResponseModel<SalesHeaderVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetSalesHeaderByIdHandler> _logger;
        private readonly IMediator _mediator;

        public GetSalesHeaderByIdHandler(IUnitofWork unitofWork, ILogger<GetSalesHeaderByIdHandler> logger, IMediator mediator)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ResponseModel<SalesHeaderVM>> Handle(GetSalesHeaderByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
//            var depts = (await _unitofWork.SalesHeaders.GetById(request.SalesHeaderId)).Adapt<SalesHeaderVM>();
//            return new ResponseModel<SalesHeaderVM> 
//            { 
//              Data = depts, Succeeded = depts != null ? true : false, Message = depts != null ? "Record Found":"No Record Found" 
//           };
            throw new NotImplementedException();
        }
    }
}

