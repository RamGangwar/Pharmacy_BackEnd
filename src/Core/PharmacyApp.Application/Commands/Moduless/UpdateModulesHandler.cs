using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;

namespace PharmacyApp.Application.Commands.Moduless
{
    public class UpdateModulesHandler : IRequestHandler<UpdateModulesCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UpdateModulesHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateModulesHandler(IUnitofWork unitofWork, ILogger<UpdateModulesHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel> Handle(UpdateModulesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
//            var query = new {ModulesName = request.ModulesName, ModulesId_neq = request.ModulesId };
//            var deptDuplicate = await _unitofWork.Moduless.GetByClause(query);
//            if (deptDuplicate != null)
//            {
//                return new ResponseModel {Message = "Modules Already Exists",Succeeded=false};
//            }
//            var dept = await _unitofWork.Moduless.GetById(request.ModulesId);
//            if (dept != null && dept.ModulesId > 0)
//            {
//                var empId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
//                dept.ModulesName = request.ModulesName;
//                dept.ModifyBy = Convert.ToInt32(empId);
//                dept.ModifyOn = DateTime.Now;
//                var result = await _unitofWork.Moduless.Update(dept);
//                if (result)
//                {
//                    return new ResponseModel {Message = "Updated Successfully",Succeeded=true};
//                }
//            }
            return new ResponseModel {Message = "Failed to update",Succeeded=false};
        }
    }
}

