using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.Discountss
{
    public class DeleteDiscountsHandler : IRequestHandler<DeleteDiscountsCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DeleteDiscountsHandler> _logger;

        public DeleteDiscountsHandler(IUnitofWork unitofWork, ILogger<DeleteDiscountsHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel> Handle(DeleteDiscountsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var dept = await _unitofWork.Discounts.GetById(request.DiscountId);
            if (dept != null && dept.DiscountId > 0)
            {
                var res = await _unitofWork.Discounts.Delete(dept);
                return new ResponseModel { Message = "Delete Successfully", Succeeded = true };
            }
            return new ResponseModel { Message = "Discounts Not Found", Succeeded = false };
        }
    }
}

