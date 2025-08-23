using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;

namespace PharmacyApp.Application.Commands.AccessPermissions
{
    public class UpdateAccessPermissionHandler : IRequestHandler<UpdateAccessPermissionCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UpdateAccessPermissionHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateAccessPermissionHandler(IUnitofWork unitofWork, ILogger<UpdateAccessPermissionHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel> Handle(UpdateAccessPermissionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var deptDuplicate = await _unitofWork.AccessPermission.GetEntityAsync(u=>u.RoleId==request.RoleId && u.ModuleId==request.ModuleId && u.PermissionId!=request.PermissionId);
            if (deptDuplicate != null)
            {
                return new ResponseModel { Message = "AccessPermission Already Exists", Succeeded = false };
            }
            var dept = await _unitofWork.AccessPermission.GetById(request.PermissionId);
            if (dept != null && dept.PermissionId > 0)
            {
                dept.CanAdd = request.CanAdd;
                dept.CanEdit = request.CanEdit;
                dept.CanDelete = request.CanDelete;
                dept.CanView = request.CanView;
                
                var result = await _unitofWork.AccessPermission.Update(dept);
                if (result)
                {
                    return new ResponseModel { Message = "Updated Successfully", Succeeded = true };
                }
            }
            return new ResponseModel {Message = "Failed to update",Succeeded=false};
        }
    }
}

