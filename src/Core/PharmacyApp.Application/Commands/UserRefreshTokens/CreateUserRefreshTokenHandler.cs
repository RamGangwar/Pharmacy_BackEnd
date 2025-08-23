using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.UserRefreshTokens
{
    public class CreateUserRefreshTokenHandler : IRequestHandler<CreateUserRefreshTokenCommand, ResponseModel<UserRefreshTokenVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CreateUserRefreshTokenHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateUserRefreshTokenHandler(IUnitofWork unitofWork, ILogger<CreateUserRefreshTokenHandler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<UserRefreshTokenVM>> Handle(CreateUserRefreshTokenCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation(nameof(Handle), request);
            var response = new ResponseModel<UserRefreshTokenVM>();
//            var query = new {UserRefreshTokenName = request.UserRefreshTokenName };
//            var dept = await _unitofWork.UserRefreshTokens.GetByClause(query);
//            if (dept == null)
//            {
//                var empId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
//                var model = request.Adapt<UserRefreshToken>();
//                model.CreatedBy = Convert.ToInt32(empId);
//                model.CreatedOn = DateTime.Now;
//                var result = await _unitofWork.UserRefreshTokens.Add(model);
//                if (result > 0)
//                {
//                    var res = await _unitofWork.UserRefreshTokens.GetById(result);
//                    response.Data = res.Adapt<UserRefreshTokenVM>();
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
//                response.Message = UserRefreshToken Already Exists.";
//                return response;
//            }
             return response;
        }
    }
}

