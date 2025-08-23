using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.Commands.Inventorys;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.SalesDetails
{
    public class CreateSalesDetailHandler : IRequestHandler<CreateSalesDetailCommand, ResponseModel<SalesDetailVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CreateSalesDetailHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateSalesDetailHandler(IUnitofWork unitofWork, ILogger<CreateSalesDetailHandler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<SalesDetailVM>> Handle(CreateSalesDetailCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation(nameof(Handle), request);
            var response = new ResponseModel<SalesDetailVM>();
           
                var model = request.Adapt<SalesDetail>();
                var result = await _unitofWork.SalesDetail.Add(model);
                if (result > 0)
                {
                    var res = await _unitofWork.SalesHeader.GetById(request.HeaderId);
                    var invcommnd = new CreateInventoryCommand();
                    invcommnd.SourceNumber = res.HeaderNumber;
                    invcommnd.MedicineId = request.MedicineId;
                    invcommnd.Type = "Sale";
                    invcommnd.Quantity = request.Quantity;
                    invcommnd.TransactionDate = DateTime.Now;

                    var invres = await _mediator.Send(invcommnd);

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
    }
}

