using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.OtherRepository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Roless
{
    public class CreateRolesHandler : IRequestHandler<CreateRolesCommand, ResponseModel<RolesVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CreateRolesHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IUserProvider _userprovider;

        public CreateRolesHandler(IUnitofWork unitofWork, ILogger<CreateRolesHandler> logger, IMediator mediator, IUserProvider userprovider)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
            _userprovider = userprovider;
        }

        public async Task<ResponseModel<RolesVM>> Handle(CreateRolesCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation(nameof(Handle), request);
            var response = new ResponseModel<RolesVM>();
            var dept = await _unitofWork.Roles.GetEntityAsync(a=>a.RoleName==request.RoleName);
            if (dept == null)
            {
                var model = request.Adapt<Roles>();
                model.CreatedBy = _userprovider.UserId;
                model.CreatedOn = DateTime.Now;
                model.IsActive = true;
                var result = await _unitofWork.Roles.Add(model);
                if (result > 0)
                {
                    var res = await _unitofWork.Roles.GetById(result);
                    response.Data = res.Adapt<RolesVM>();
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
                response.Message = "Role Already Exists.";
                return response;
            }
        }
    }
}

