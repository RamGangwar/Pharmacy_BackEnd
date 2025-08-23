using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PharmacyApp.Domain.Model.Reponse
{
    public class PagingModel<entity> where entity : class
    {


        public PagingModel(IEnumerable<entity> Data, int PageIndex, int PageSize, int TotalRecord)
        {
            this.Data = Data;
            this.PageIndex = PageIndex;            
            this.PageSize = PageSize;
            this.TotalRecord = TotalRecord;

            this.TotalPage =(int)Math.Ceiling((decimal)this.TotalRecord / this.PageSize);
        }
        public IEnumerable<entity> Data { get; set; }
        public int PageIndex { get; set; }
        public int TotalPage { get; private set; }
        public int PageSize { get; set; }
        //[JsonIgnore]
        public int TotalRecord { get; set; }
    }
}
