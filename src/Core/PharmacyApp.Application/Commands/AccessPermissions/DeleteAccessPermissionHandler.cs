using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.AccessPermissions
{
    public class DeleteAccessPermissionHandler : IRequestHandler<DeleteAccessPermissionCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DeleteAccessPermissionHandler> _logger;

        public DeleteAccessPermissionHandler(IUnitofWork unitofWork, ILogger<DeleteAccessPermissionHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel> Handle(DeleteAccessPermissionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var dept = await _unitofWork.AccessPermission.GetById(request.PermissionId);
            if (dept != null && dept.PermissionId > 0)
            {
                var res = await _unitofWork.AccessPermission.Delete(dept);
                return new ResponseModel { Message = "Delete Successfully", Succeeded = true };
            }
            return new ResponseModel { Message = "AccessPermission Not Found", Succeeded = false };
        }
    }
}

