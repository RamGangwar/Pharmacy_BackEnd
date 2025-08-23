using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.Userss
{
    public class GetUsersByIdHandler : IRequestHandler<GetUsersByIdQuery, ResponseModel<UsersVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetUsersByIdHandler> _logger;
        private readonly IMediator _mediator;

        public GetUsersByIdHandler(IUnitofWork unitofWork, ILogger<GetUsersByIdHandler> logger, IMediator mediator)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ResponseModel<UsersVM>> Handle(GetUsersByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var depts = (await _unitofWork.Users.GetById(request.UserId)).Adapt<UsersVM>();
            return new ResponseModel<UsersVM>
            {
                Data = depts,
                Succeeded = depts != null ? true : false,
                Message = depts != null ? "Record Found" : "No Record Found"
            };
        }
    }
}

