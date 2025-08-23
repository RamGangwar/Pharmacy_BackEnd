using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.UserRefreshTokens
{
    public class GetUserRefreshTokenByIdHandler : IRequestHandler<GetUserRefreshTokenByIdQuery, ResponseModel<UserRefreshTokenVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetUserRefreshTokenByIdHandler> _logger;
        private readonly IMediator _mediator;

        public GetUserRefreshTokenByIdHandler(IUnitofWork unitofWork, ILogger<GetUserRefreshTokenByIdHandler> logger, IMediator mediator)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ResponseModel<UserRefreshTokenVM>> Handle(GetUserRefreshTokenByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
//            var depts = (await _unitofWork.UserRefreshTokens.GetById(request.UserRefreshTokenId)).Adapt<UserRefreshTokenVM>();
//            return new ResponseModel<UserRefreshTokenVM> 
//            { 
//              Data = depts, Succeeded = depts != null ? true : false, Message = depts != null ? "Record Found":"No Record Found" 
//           };
            throw new NotImplementedException();
        }
    }
}

