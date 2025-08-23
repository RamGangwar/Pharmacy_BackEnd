using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.UserRefreshTokens
{
    public class DeleteUserRefreshTokenHandler : IRequestHandler<DeleteUserRefreshTokenCommand, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DeleteUserRefreshTokenHandler> _logger;

        public DeleteUserRefreshTokenHandler(IUnitofWork unitofWork, ILogger<DeleteUserRefreshTokenHandler> logger)
        {
            _unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<ResponseModel> Handle(DeleteUserRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(Handle), request);
//            var dept = await _unitofWork.UserRefreshTokens.GetById(request.UserRefreshTokenId);
//            if (dept != null && dept.UserRefreshTokenId > 0)
//            {
//                var res = await _unitofWork.UserRefreshTokens.Delete(dept);
//                return new ResponseModel { Message = "Delete Successfully", Succeeded=true };
//            }
            return new ResponseModel { Message = "UserRefreshToken Not Found", Succeeded=false };
        }
    }
}

