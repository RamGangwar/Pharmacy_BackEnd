using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.OtherRepository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.AccessPermissions
{
    public class CreateAccessPermissionHandler : IRequestHandler<CreateAccessPermissionCommand, ResponseModel<AccessPermissionVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CreateAccessPermissionHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserProvider _userProvider;

        public CreateAccessPermissionHandler(IUnitofWork unitofWork, ILogger<CreateAccessPermissionHandler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor, IUserProvider userProvider)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _userProvider = userProvider;
        }

        public async Task<ResponseModel<AccessPermissionVM>> Handle(CreateAccessPermissionCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation(nameof(Handle), request);
            var response = new ResponseModel<AccessPermissionVM>();
            var dept = await _unitofWork.AccessPermission.GetEntityAsync(u=>u.RoleId==request.RoleId && u.ModuleId==request.ModuleId);
            if (dept == null)
            {
                var model = request.Adapt<AccessPermission>();
                model.CreatedBy = _userProvider.UserId;
                model.CreatedOn = DateTime.Now;
                var result = await _unitofWork.AccessPermission.Add(model);
                if (result > 0)
                {
                    var res = await _unitofWork.AccessPermission.GetById(result);
                    response.Data = res.Adapt<AccessPermissionVM>();
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
                response.Message = "Access Permission Already Exists.";
                return response;
            }
        }
    }
}

