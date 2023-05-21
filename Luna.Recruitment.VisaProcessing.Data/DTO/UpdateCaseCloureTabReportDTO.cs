using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
    public class UpdateCaseCloureTabReportDTO
    {
        public long? Id { get; set; }
        public DateTime? DocumentHandoverDate { get; set; }
        public DateTime? DateOfCloure { get; set; }
    }
}
