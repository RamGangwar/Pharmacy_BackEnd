using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Inventorys
{
    public class CreateInventoryHandler : IRequestHandler<CreateInventoryCommand, ResponseModel<InventoryVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CreateInventoryHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateInventoryHandler(IUnitofWork unitofWork, ILogger<CreateInventoryHandler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<InventoryVM>> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation(nameof(Handle), request);
            var response = new ResponseModel<InventoryVM>();
            if (request != null)
            {
                var model = request.Adapt<Inventory>();
                model.Quantity = model.Type == "Sale" ? -model.Quantity : model.Quantity;
                var result = await _unitofWork.Inventory.Add(model);
                if (result > 0)
                {
                    var res = await _unitofWork.Inventory.GetById(result);
                    response.Data = res.Adapt<InventoryVM>();
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
                response.Message = "Invalid details";
                return response;
            }

        }
    }
}

