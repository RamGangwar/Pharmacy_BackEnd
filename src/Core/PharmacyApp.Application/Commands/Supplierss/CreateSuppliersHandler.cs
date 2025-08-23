using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.OtherRepository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Supplierss
{
    public class CreateSuppliersHandler : IRequestHandler<CreateSuppliersCommand, ResponseModel<SuppliersVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CreateSuppliersHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserProvider _userProvider;

        public CreateSuppliersHandler(IUnitofWork unitofWork, ILogger<CreateSuppliersHandler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor, IUserProvider userProvider)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _userProvider = userProvider;
        }

        public async Task<ResponseModel<SuppliersVM>> Handle(CreateSuppliersCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Action :{@Action} , Request :{@Request}", nameof(Handle), request);
            var response = new ResponseModel<SuppliersVM>();
            var dept = await _unitofWork.Suppliers.GetEntityAsync(u => u.CompanyName == request.CompanyName);
            if (dept == null)
            {
                var model = request.Adapt<Suppliers>();
                model.CreatedOn = DateTime.Now;
                var result = await _unitofWork.Suppliers.Add(model);
                if (result > 0)
                {
                    var res = await _unitofWork.Suppliers.GetById(result);
                    response.Data = res.Adapt<SuppliersVM>();
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
                response.Message = "Suppliers Already Exists.";
                return response;
            }
            return response;
        }
    }
}

