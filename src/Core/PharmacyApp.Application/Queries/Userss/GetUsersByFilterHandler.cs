using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Userss
{
    public class GetUsersByFilterHandler : IRequestHandler<GetUsersByFilterQuery, PagingModel<UsersVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetUsersByFilterHandler> _logger;

        public GetUsersByFilterHandler(IUnitofWork unitofWork, ILogger<GetUsersByFilterHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<PagingModel<UsersVM>> Handle(GetUsersByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var result = await _unitofWork.Users.GetByPaging(request);
            return new PagingModel<UsersVM>(result, request.PageIndex, request.PageSize, result.Count() > 0 ? result.FirstOrDefault().TotalRecord : 0);
        }
    }
}

