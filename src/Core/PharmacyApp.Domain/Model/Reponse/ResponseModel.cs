using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Domain.Model
{
    public class ResponseModel
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
    public class ResponseModel<entity> where entity : class
    {
        public entity Data { get; set; }
        public string Message { get; set; }
        public int StatusCode => Succeeded ? 1 : 0;
        public bool Succeeded { get; set; }

    }

}
