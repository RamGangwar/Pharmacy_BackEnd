using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.Prescriptionss
{
    public class DeletePrescriptionsHandler : IRequestHandler<DeletePrescriptionsCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DeletePrescriptionsHandler> _logger;

        public DeletePrescriptionsHandler(IUnitofWork unitofWork, ILogger<DeletePrescriptionsHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel> Handle(DeletePrescriptionsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var dept = await _unitofWork.Prescriptions.GetById(request.prescription_id);
            if (dept != null && dept.prescription_id > 0)
            {
                var res = await _unitofWork.Prescriptions.Delete(dept);
                return new ResponseModel { Message = "Delete Successfully", Succeeded = true };
            }
            return new ResponseModel { Message = "Prescriptions Not Found", Succeeded=false };
        }
    }
}

