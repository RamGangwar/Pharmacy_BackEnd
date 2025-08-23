using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Moduless
{
    public class CreateModulesHandler : IRequestHandler<CreateModulesCommand, ResponseModel<ModulesVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CreateModulesHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateModulesHandler(IUnitofWork unitofWork, ILogger<CreateModulesHandler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<ModulesVM>> Handle(CreateModulesCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation(nameof(Handle), request);
            var response = new ResponseModel<ModulesVM>();
//            var query = new {ModulesName = request.ModulesName };
//            var dept = await _unitofWork.Moduless.GetByClause(query);
//            if (dept == null)
//            {
//                var empId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
//                var model = request.Adapt<Modules>();
//                model.CreatedBy = Convert.ToInt32(empId);
//                model.CreatedOn = DateTime.Now;
//                var result = await _unitofWork.Moduless.Add(model);
//                if (result > 0)
//                {
//                    var res = await _unitofWork.Moduless.GetById(result);
//                    response.Data = res.Adapt<ModulesVM>();
//                    response.Succeeded=true;
//                    response.Message = "Saved Successfully.";
//                    return response;
//                }
//                else
//                {
//                    response.Succeeded=false;
//                    response.Message = "Failed to save.";
//                    return response;
//                }
//            }
//            else
//            {
//                response.Succeeded=false;
//                response.Message = Modules Already Exists.";
//                return response;
//            }
             return response;
        }
    }
}

