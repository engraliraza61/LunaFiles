using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
    public class UpdateProcessingTabReportDTO
    {
        public long Id { get; set; }
        public long CandidateId { get; set; }
        public long TradeId { get; set; }
        public string ProcessingStartDate { get; set; }
    }
}
