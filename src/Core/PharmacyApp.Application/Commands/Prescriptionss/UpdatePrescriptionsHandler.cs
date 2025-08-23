using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;

namespace PharmacyApp.Application.Commands.Prescriptionss
{
    public class UpdatePrescriptionsHandler : IRequestHandler<UpdatePrescriptionsCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UpdatePrescriptionsHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdatePrescriptionsHandler(IUnitofWork unitofWork, ILogger<UpdatePrescriptionsHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseModel> Handle(UpdatePrescriptionsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            
            var dept = await _unitofWork.Prescriptions.GetById(request.prescription_id);
            if (dept != null && dept.prescription_id > 0)
            {
                dept.DoctorName = request.DoctorName;
                dept.DoctorContact = request.DoctorContact;
                dept.Notes = request.Notes;
                var result = await _unitofWork.Prescriptions.Update(dept);
                if (result)
                {
                    return new ResponseModel { Message = "Updated Successfully", Succeeded = true };
                }
            }
            return new ResponseModel {Message = "Failed to update",Succeeded=false};
        }
    }
}

