using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luna.Recruitment.VisaProcessing.Web.Models
{
    public class ListClass
    {
        public dynamic Data { get; set; }
        public int TotalPages { get { return ((TotalRecords + PageSize - 1)) / PageSize; } }
        public int TotalRecords { get; set; }
        public int PageSize { get { return 10; } }
        public int CurrentPage { get; set; }
    }
}
