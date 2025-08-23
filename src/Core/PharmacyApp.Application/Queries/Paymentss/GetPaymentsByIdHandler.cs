using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.Paymentss
{
    public class GetPaymentsByIdHandler : IRequestHandler<GetPaymentsByIdQuery, ResponseModel<PaymentsVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetPaymentsByIdHandler> _logger;
        private readonly IMediator _mediator;

        public GetPaymentsByIdHandler(IUnitofWork unitofWork, ILogger<GetPaymentsByIdHandler> logger, IMediator mediator)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ResponseModel<PaymentsVM>> Handle(GetPaymentsByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var depts = (await _unitofWork.Payments.GetById(request.PaymentId)).Adapt<PaymentsVM>();
            return new ResponseModel<PaymentsVM>
            {
                Data = depts,
                Succeeded = depts != null ? true : false,
                Message = depts != null ? "Record Found" : "No Record Found"
            };
            throw new NotImplementedException();
        }
    }
}

