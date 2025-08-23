using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;

namespace PharmacyApp.Application.Commands.Userss
{
    public class UpdateUsersHandler : IRequestHandler<UpdateUsersCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UpdateUsersHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateUsersHandler(IUnitofWork unitofWork, ILogger<UpdateUsersHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel> Handle(UpdateUsersCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var deptDuplicate = await _unitofWork.Users.GetEntityAsync(u=>u.UserName==request.UserName && u.UserId!=request.UserId);
            if (deptDuplicate != null)
            {
                return new ResponseModel { Message = "Users Already Exists", Succeeded = false };
            }
            var dept = await _unitofWork.Users.GetById(request.UserId);
            if (dept != null && dept.UserId > 0)
            {
                dept.UserName = request.UserName;
                dept.MobileNo = request.MobileNo;
                dept.Email = request.Email;
                dept.RoleId = request.RoleId;
                dept.FullName = request.FullName;
                dept.IsActive = request.IsActive;
              
                var result = await _unitofWork.Users.Update(dept);
                if (result)
                {
                    return new ResponseModel { Message = "Updated Successfully", Succeeded = true };
                }
            }
            return new ResponseModel {Message = "Failed to update",Succeeded=false};
        }
    }
}

