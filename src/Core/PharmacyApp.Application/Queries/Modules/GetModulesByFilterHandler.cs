using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.Modules
{
    public class GetModulesByFilterHandler : IRequestHandler<GetModulesByFilterQuery, List<ModulesVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetModulesByFilterHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetModulesByFilterHandler(IUnitofWork unitofWork, ILogger<GetModulesByFilterHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ModulesVM>> Handle(GetModulesByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var result = new List<ModulesVM>();
            if (request.IsPermission)
            {
                result = await _unitofWork.Modules.GetListForAll();
            }
            else
            {
                if (request.EmployeeId == 0 && request.RoleId == 0)
                {
                    //var roleid = _httpContextAccessor.HttpContext.Session.GetString("RoleId");
                    //request.RoleId = !string.IsNullOrEmpty(roleid) ? Convert.ToInt32(roleid) : 0;
                    result = await _unitofWork.Modules.GetByPaging(request);
                }
                else
                {
                    result = await _unitofWork.Modules.GetByPaging(request);
                }
            }
            return new List<ModulesVM>(result);
        }


        //public async Task<List<ModulesVM>> Handle(GetModulesByFilterQuery request, CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation(nameof(Handle), request);
        //    // 1 for View model and 0 for entity
        //    var res = await _unitofWork.Modules.SaveEntityOrModel(1);

        //    var result = new List<ModulesVM>();
        //    return new List<ModulesVM>(result);
        //}
    }
}
