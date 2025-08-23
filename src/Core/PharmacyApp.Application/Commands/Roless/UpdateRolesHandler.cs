using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;

namespace PharmacyApp.Application.Commands.Roless
{
    public class UpdateRolesHandler : IRequestHandler<UpdateRolesCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UpdateRolesHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateRolesHandler(IUnitofWork unitofWork, ILogger<UpdateRolesHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel> Handle(UpdateRolesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var query = new { RolesName = request.RoleName, RolesId_neq = request.RoleId };
            var deptDuplicate = await _unitofWork.Roles.GetEntityAsync(a => a.RoleName == request.RoleName && a.RoleId != request.RoleId);
            if (deptDuplicate != null)
            {
                return new ResponseModel { Message = "Role Already Exists", Succeeded = false };
            }
            var dept = await _unitofWork.Roles.GetById(request.RoleId);
            if (dept != null && dept.RoleId > 0)
            {
                dept.RoleName = request.RoleName;
                dept.IsActive = request.IsActive;

                var result = await _unitofWork.Roles.Update(dept);
                if (result)
                {
                    return new ResponseModel { Message = "Updated Successfully", Succeeded = true };
                }
            }
            return new ResponseModel { Message = "Failed to update", Succeeded = false };
        }
    }
}

