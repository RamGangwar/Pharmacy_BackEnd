using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using PharmacyApp.Domain.ViewModels;
namespace PharmacyApp.Application.Commands.Mediciness
{
    public class CreateMedicinesHandler : IRequestHandler<CreateMedicinesCommand, ResponseModel<MedicinesVM>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<CreateMedicinesHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateMedicinesHandler(IUnitofWork unitofWork, ILogger<CreateMedicinesHandler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<MedicinesVM>> Handle(CreateMedicinesCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation(nameof(Handle), request);
            var response = new ResponseModel<MedicinesVM>();
            var dept = await _unitofWork.Medicines.GetEntityAsync(u=>u.MedicineName==request.MedicineName);
            if (dept == null)
            {
                var model = request.Adapt<Medicines>();
                model.CreatedOn = DateTime.Now;
                var result = await _unitofWork.Medicines.Add(model);
                if (result > 0)
                {
                    var res = await _unitofWork.Medicines.GetById(result);
                    response.Data = res.Adapt<MedicinesVM>();
                    response.Succeeded = true;
                    response.Message = "Saved Successfully.";
                    return response;
                }
                else
                {
                    response.Succeeded = false;
                    response.Message = "Failed to save.";
                    return response;
                }
            }
            else
            {
                response.Succeeded = false;
                response.Message = "Medicine Already Exists.";
                return response;
            }
            return response;
        }
    }
}

