using MediatR;
using PharmacyApp.Application.OtherRepository;
using PharmacyApp.Application.Repository;
using PharmacyApp.Application.UnitOfWork;
using PharmacyApp.Domain.Entity;
using PharmacyApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Commands.Authentication
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, ResponseModel>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IEncryptRepository _encryptRepository;
        public ChangePasswordHandler(IUnitofWork unitofWork, IEncryptRepository encryptRepository)
        {
            _unitofWork = unitofWork;
            _encryptRepository = encryptRepository;
        }
        public async Task<ResponseModel> Handle(ChangePasswordRequest req, CancellationToken cancellationToken)
        {
            bool res = false;
            var user = new Users();
            if (req.UserId > 0)
            {
                user = await _unitofWork.Users.GetById(req.UserId);
            }
            else if (!string.IsNullOrEmpty(req.UserName))
            {
                user = await _unitofWork.Users.GetEntityAsync(u => u.UserName == req.UserName);
            }
            else
            {
                return new ResponseModel { Message = "Invalid UserId or Email", };
            }
            if (user != null && user.UserId > 0)
            {
                if (req.IsForgotPassword)
                {
                    string pss = _encryptRepository.Encrypt(req.NewPassword);
                    user.Password = pss;
                    await _unitofWork.Users.Update(user);
                    res = true;
                }
                else
                {
                    string currpwd = _encryptRepository.Decrypt(user.Password);
                    if (currpwd != req.CurrentPassword)
                    {
                        return new ResponseModel { StatusCode = 0, Succeeded = false, Message = "Incorrect current password" };
                    }
                    else if (req.CurrentPassword == req.NewPassword)
                    {
                        return new ResponseModel { StatusCode = 0, Succeeded=false, Message = " Current password and New Password can not be same" };
                    }
                    else
                    {
                        string newpss = _encryptRepository.Encrypt(req.NewPassword);
                        user.Password = newpss;
                        await _unitofWork.Users.Update(user);
                        res = true;
                    }

                }
            }
            return new ResponseModel { StatusCode = 1,Succeeded=true, Message = "Password Changed" };

        }
    }
}
