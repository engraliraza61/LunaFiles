using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
    public class UpdateSubmissionTabReportDTO
    {
        public long Id { get; set; }
        public DateTime? PassportSubmissionDate { get; set; }
        public string submiisonRemarks { get; set; }
    }
}
