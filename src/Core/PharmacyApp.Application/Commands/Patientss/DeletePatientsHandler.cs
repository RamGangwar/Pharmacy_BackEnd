using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.Patientss
{
    public class DeletePatientsHandler : IRequestHandler<DeletePatientsCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DeletePatientsHandler> _logger;

        public DeletePatientsHandler(IUnitofWork unitofWork, ILogger<DeletePatientsHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel> Handle(DeletePatientsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var dept = await _unitofWork.Patients.GetById(request.PatientId);
            if (dept != null && dept.PatientId > 0)
            {
                var res = await _unitofWork.Patients.Delete(dept);
                return new ResponseModel { Message = "Delete Successfully", Succeeded = true };
            }
            return new ResponseModel { Message = "Patients Not Found", Succeeded=false };
        }
    }
}

