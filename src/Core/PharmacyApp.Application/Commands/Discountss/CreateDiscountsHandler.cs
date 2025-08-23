using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Discountss
{
    public class CreateDiscountsHandler : IRequestHandler<CreateDiscountsCommand, ResponseModel<DiscountsVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CreateDiscountsHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateDiscountsHandler(IUnitofWork unitofWork, ILogger<CreateDiscountsHandler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<DiscountsVM>> Handle(CreateDiscountsCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation(nameof(Handle), request);
            var response = new ResponseModel<DiscountsVM>();
            var dept = await _unitofWork.Discounts.GetEntityAsync(a => a.StartDate == request.StartDate && a.EndDate == request.EndDate && a.MedicineId == request.MedicineId);
            if (dept == null)
            {
                var model = request.Adapt<Discounts>();

                var result = await _unitofWork.Discounts.Add(model);
                if (result > 0)
                {
                    var res = await _unitofWork.Discounts.GetById(result);
                    response.Data = res.Adapt<DiscountsVM>();
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
                response.Message = "Discount Already Exists.";
                return response;
            }

        }
    }
}

