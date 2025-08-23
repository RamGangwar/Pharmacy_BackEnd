using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.Roless
{
    public class GetRolesByIdHandler : IRequestHandler<GetRolesByIdQuery, ResponseModel<RolesVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetRolesByIdHandler> _logger;
        private readonly IMediator _mediator;

        public GetRolesByIdHandler(IUnitofWork unitofWork, ILogger<GetRolesByIdHandler> logger, IMediator mediator)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ResponseModel<RolesVM>> Handle(GetRolesByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var depts = (await _unitofWork.Roles.GetById(request.RoleId)).Adapt<RolesVM>();
            return new ResponseModel<RolesVM>
            {
                Data = depts,
                Succeeded = depts != null ? true : false,
                Message = depts != null ? "Record Found" : "No Record Found"
            };
        }
    }
}

