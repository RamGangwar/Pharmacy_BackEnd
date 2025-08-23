using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.Userss
{
    public class DeleteUsersHandler : IRequestHandler<DeleteUsersCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DeleteUsersHandler> _logger;

        public DeleteUsersHandler(IUnitofWork unitofWork, ILogger<DeleteUsersHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel> Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
            var dept = await _unitofWork.Users.GetById(request.UserId);
            if (dept != null && dept.UserId > 0)
            {
                var res = await _unitofWork.Users.Delete(dept);
                return new ResponseModel { Message = "Delete Successfully", Succeeded = true };
            }
            return new ResponseModel { Message = "Users Not Found", Succeeded = false };
        }
    }
}

