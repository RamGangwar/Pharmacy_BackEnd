using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.Moduless
{
    public class DeleteModulesHandler : IRequestHandler<DeleteModulesCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DeleteModulesHandler> _logger;

        public DeleteModulesHandler(IUnitofWork unitofWork, ILogger<DeleteModulesHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel> Handle(DeleteModulesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
//            var dept = await _unitofWork.Moduless.GetById(request.ModulesId);
//            if (dept != null && dept.ModulesId > 0)
//            {
//                var res = await _unitofWork.Moduless.Delete(dept);
//                return new ResponseModel { Message = "Delete Successfully", Succeeded=true };
//            }
            return new ResponseModel { Message = "Modules Not Found", Succeeded=false };
        }
    }
}

