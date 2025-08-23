using MediatR;
using PharmacyApp.Application.Repository;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.Model.AuthenticationModel;
using PharmacyApp.Application.UnitOfWork;

namespace PharmacyApp.Application.Commands.Authentication
{
    public class AuthenticationHandler : IRequestHandler<AuthenticationRequest, ResponseModel<AuthenticationResponse>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IJWTService _jWTService;
        public AuthenticationHandler(IUnitofWork unitofWork, IJWTService jWTService)
        {
            _unitofWork = unitofWork;
            _jWTService = jWTService;
        }
        public async Task<ResponseModel<AuthenticationResponse>> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var authResponse = await _jWTService.GetTokenAsync(request, request.IpAddress);
            if (authResponse == null)
                return new ResponseModel<AuthenticationResponse> { Data = null,  Message = authResponse.Message,Succeeded=false };
            return new ResponseModel<AuthenticationResponse> { Data = authResponse, Succeeded=authResponse.Succeeded, Message = authResponse.Message };
        }
    }
}
