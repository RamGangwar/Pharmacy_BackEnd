using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.Model.Reponse;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Queries.AccessPermissions
{
    public class GetAccessPermissionByFilterHandler : IRequestHandler<GetAccessPermissionByFilterQuery, ResponseModel<IEnumerable<AccessPermissionVM>>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetAccessPermissionByFilterHandler> _logger;

        public GetAccessPermissionByFilterHandler(IUnitofWork unitofWork, ILogger<GetAccessPermissionByFilterHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel<IEnumerable<AccessPermissionVM>>> Handle(GetAccessPermissionByFilterQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var result = await _unitofWork.AccessPermission.GetByPaging(request);
            return new ResponseModel<IEnumerable<AccessPermissionVM>>
            {
                Data = result,
                Succeeded = result.Count()>0 ? true : false,
                Message = result.Count() > 0 ? "Record Found" : "No Record Found"
            };
        }
    }
}

