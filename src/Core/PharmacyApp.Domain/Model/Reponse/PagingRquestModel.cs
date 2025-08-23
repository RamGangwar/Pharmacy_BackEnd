using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Domain.Model.Reponse
{
    public class PagingRquestModel
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int SkipRow { get; set; } = 0;
        public string SortBy { get; set; } = "CreatedOn";
        public string SortOrder { get; set; } = "Desc";
    }
}
