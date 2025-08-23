using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;

namespace PharmacyApp.Application.Queries.Supplierss
{
    public class GetSuppliersByIdHandler : IRequestHandler<GetSuppliersByIdQuery, ResponseModel<SuppliersVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<GetSuppliersByIdHandler> _logger;
        private readonly IMediator _mediator;

        public GetSuppliersByIdHandler(IUnitofWork unitofWork, ILogger<GetSuppliersByIdHandler> logger, IMediator mediator)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ResponseModel<SuppliersVM>> Handle(GetSuppliersByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var depts = (await _unitofWork.Suppliers.GetById(request.SupplierId)).Adapt<SuppliersVM>();
            return new ResponseModel<SuppliersVM>
            {
                Data = depts,
                Succeeded = depts != null ? true : false,
                Message = depts != null ? "Record Found" : "No Record Found"
            };
        }
    }
}

