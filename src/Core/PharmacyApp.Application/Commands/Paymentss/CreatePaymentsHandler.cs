using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Paymentss
{
    public class CreatePaymentsHandler : IRequestHandler<CreatePaymentsCommand, ResponseModel<PaymentsVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CreatePaymentsHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreatePaymentsHandler(IUnitofWork unitofWork, ILogger<CreatePaymentsHandler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<PaymentsVM>> Handle(CreatePaymentsCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation(nameof(Handle), request);
            var response = new ResponseModel<PaymentsVM>();
            var dept = await _unitofWork.Payments.GetEntityAsync(a => a.HeaderId == request.HeaderId);
            if (dept == null)
            {
                var model = request.Adapt<Payments>();
                var result = await _unitofWork.Payments.Add(model);
                if (result > 0)
                {
                    var res = await _unitofWork.Payments.GetById(result);
                    response.Data = res.Adapt<PaymentsVM>();
                    response.Succeeded = true;
                    response.Message = "Saved Successfully.";
                    return response;
                }
                else
                {
                    response.Succeeded = false;
                    response.Message = "Failed to save.";
                    return response;
                }
            }
            else
            {
                response.Succeeded = false;
                response.Message = "Payments Already Exists.";
                return response;
            }
        }
    }
}

