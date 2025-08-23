using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.UserRefreshTokens
{
    public class GetUserRefreshTokenByFilterHandler : IRequestHandler<GetUserRefreshTokenByFilterQuery, PagingModel<UserRefreshTokenVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetUserRefreshTokenByFilterHandler> _logger;

        public GetUserRefreshTokenByFilterHandler(IUnitofWork unitofWork, ILogger<GetUserRefreshTokenByFilterHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<PagingModel<UserRefreshTokenVM>> Handle(GetUserRefreshTokenByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
//            var result = await _unitofWork.UserRefreshTokens.GetByPaging(request);//           return new PagingModel<UserRefreshTokenVM>(result, request.PageIndex, request.PageSize, result.Count() > 0 ? result.FirstOrDefault().TotalRecord : 0);
            throw new NotImplementedException();
        }
    }
}

