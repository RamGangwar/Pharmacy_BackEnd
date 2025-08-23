using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Domain.Model.AuthenticationModel
{
    public class AuthenticationResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public UserBaseModel User { get; set; }
        public string Message { get; set; }
        public bool Succeeded { get; set; }
    }
}
