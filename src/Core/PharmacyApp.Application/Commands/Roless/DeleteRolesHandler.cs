using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.Roless
{
    public class DeleteRolesHandler : IRequestHandler<DeleteRolesCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DeleteRolesHandler> _logger;

        public DeleteRolesHandler(IUnitofWork unitofWork, ILogger<DeleteRolesHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel> Handle(DeleteRolesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var dept = await _unitofWork.Roles.GetById(request.RoleId);
            if (dept != null && dept.RoleId > 0)
            {
                var res = await _unitofWork.Roles.Delete(dept);
                return new ResponseModel { Message = "Delete Successfully", Succeeded = true };
            }
            return new ResponseModel { Message = "Roles Not Found", Succeeded = false };
        }
    }
}

