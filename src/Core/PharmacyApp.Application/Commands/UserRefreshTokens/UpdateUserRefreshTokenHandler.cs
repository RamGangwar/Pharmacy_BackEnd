using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;

namespace PharmacyApp.Application.Commands.UserRefreshTokens
{
    public class UpdateUserRefreshTokenHandler : IRequestHandler<UpdateUserRefreshTokenCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UpdateUserRefreshTokenHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateUserRefreshTokenHandler(IUnitofWork unitofWork, ILogger<UpdateUserRefreshTokenHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel> Handle(UpdateUserRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
//            var query = new {UserRefreshTokenName = request.UserRefreshTokenName, UserRefreshTokenId_neq = request.UserRefreshTokenId };
//            var deptDuplicate = await _unitofWork.UserRefreshTokens.GetByClause(query);
//            if (deptDuplicate != null)
//            {
//                return new ResponseModel {Message = "UserRefreshToken Already Exists",Succeeded=false};
//            }
//            var dept = await _unitofWork.UserRefreshTokens.GetById(request.UserRefreshTokenId);
//            if (dept != null && dept.UserRefreshTokenId > 0)
//            {
//                var empId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
//                dept.UserRefreshTokenName = request.UserRefreshTokenName;
//                dept.ModifyBy = Convert.ToInt32(empId);
//                dept.ModifyOn = DateTime.Now;
//                var result = await _unitofWork.UserRefreshTokens.Update(dept);
//                if (result)
//                {
//                    return new ResponseModel {Message = "Updated Successfully",Succeeded=true};
//                }
//            }
            return new ResponseModel {Message = "Failed to update",Succeeded=false};
        }
    }
}

