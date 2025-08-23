using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.AccessPermissions
{
    public class GetAccessPermissionByIdHandler : IRequestHandler<GetAccessPermissionByIdQuery, ResponseModel<AccessPermissionVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetAccessPermissionByIdHandler> _logger;
        private readonly IMediator _mediator;

        public GetAccessPermissionByIdHandler(IUnitofWork unitofWork, ILogger<GetAccessPermissionByIdHandler> logger, IMediator mediator)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ResponseModel<AccessPermissionVM>> Handle(GetAccessPermissionByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var depts = (await _unitofWork.AccessPermission.GetById(request.PermissionId)).Adapt<AccessPermissionVM>();
            return new ResponseModel<AccessPermissionVM>
            {
                Data = depts,
                Succeeded = depts != null ? true : false,
                Message = depts != null ? "Record Found" : "No Record Found"
            };
        }
    }
}

