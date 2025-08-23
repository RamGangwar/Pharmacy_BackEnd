using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;

namespace PharmacyApp.Application.Commands.Patientss
{
    public class UpdatePatientsHandler : IRequestHandler<UpdatePatientsCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UpdatePatientsHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdatePatientsHandler(IUnitofWork unitofWork, ILogger<UpdatePatientsHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel> Handle(UpdatePatientsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var deptDuplicate = await _unitofWork.Patients.GetEntityAsync(u => u.MobileNo == request.MobileNo && u.PatientId != request.PatientId);
            if (deptDuplicate != null)
            {
                return new ResponseModel { Message = "Patients Already Exists", Succeeded = false };
            }
            var dept = await _unitofWork.Patients.GetById(request.PatientId);
            if (dept != null && dept.PatientId > 0)
            {
                dept.FullName = request.FullName;
                dept.Email = request.Email;
                dept.MobileNo = request.MobileNo;
                dept.Address = request.Address;

                var result = await _unitofWork.Patients.Update(dept);
                if (result)
                {
                    return new ResponseModel { Message = "Updated Successfully", Succeeded = true };
                }
            }
            return new ResponseModel { Message = "Failed to update", Succeeded = false };
        }
    }
}

