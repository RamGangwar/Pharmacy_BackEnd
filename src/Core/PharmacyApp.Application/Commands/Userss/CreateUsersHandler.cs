using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.OtherRepository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Userss
{
    public class CreateUsersHandler : IRequestHandler<CreateUsersCommand, ResponseModel<UsersVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CreateUsersHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserProvider _userProvider;
        private readonly IEncryptRepository _encryptRepository;

        public CreateUsersHandler(IUnitofWork unitofWork, ILogger<CreateUsersHandler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor, IUserProvider userProvider, IEncryptRepository encryptRepository)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _userProvider = userProvider;
            _encryptRepository = encryptRepository;
        }

        public async Task<ResponseModel<UsersVM>> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation(nameof(Handle), request);
            var response = new ResponseModel<UsersVM>();
            var dept = await _unitofWork.Users.GetEntityAsync(u => u.UserName == request.UserName);
            if (dept == null)
            {
                var model = request.Adapt<Users>();
                model.CreatedBy = _userProvider.UserId;
                model.CreatedOn = DateTime.Now;
                model.Password = _encryptRepository.Encrypt(model.Password);
                var result = await _unitofWork.Users.Add(model);
                if (result > 0)
                {
                    var res = await _unitofWork.Users.GetById(result);
                    response.Data = res.Adapt<UsersVM>();
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
                response.Message = "User Already Exists.";
                return response;
            }
        }
    }
}

