using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Paymentss
{
    public class GetPaymentsByFilterHandler : IRequestHandler<GetPaymentsByFilterQuery, PagingModel<PaymentsVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetPaymentsByFilterHandler> _logger;

        public GetPaymentsByFilterHandler(IUnitofWork unitofWork, ILogger<GetPaymentsByFilterHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<PagingModel<PaymentsVM>> Handle(GetPaymentsByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var result = await _unitofWork.Payments.GetByPaging(request);
            return new PagingModel<PaymentsVM>(result, request.PageIndex, request.PageSize, result.Count() > 0 ? result.FirstOrDefault().TotalRecord : 0);
        }
    }
}

