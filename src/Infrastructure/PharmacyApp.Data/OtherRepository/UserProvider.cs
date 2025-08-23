using Microsoft.AspNetCore.Http;
using PharmacyApp.Application.OtherRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Data.OtherRepository
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public int UserId
        {
            get
            {
               string userid =this.httpContextAccessor.HttpContext.User.Claims.First(i => i.Type == "UserId").Value;
                return Convert.ToInt32(userid);
            }          
        }
    }
}
