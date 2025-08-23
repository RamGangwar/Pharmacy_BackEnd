using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.Roless
{
    public class GetRolesByFilterHandler : IRequestHandler<GetRolesByFilterQuery, PagingModel<RolesVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetRolesByFilterHandler> _logger;

        public GetRolesByFilterHandler(IUnitofWork unitofWork, ILogger<GetRolesByFilterHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<PagingModel<RolesVM>> Handle(GetRolesByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var result = await _unitofWork.Roles.GetByPaging(request);
            return new PagingModel<RolesVM>(result, request.PageIndex, request.PageSize, result.Count() > 0 ? result.FirstOrDefault().TotalRecord : 0);
        }
    }
}

