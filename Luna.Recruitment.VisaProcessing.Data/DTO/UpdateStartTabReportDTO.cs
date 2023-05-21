using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
    public class UpdateStartTabReportDTO
    {
        public long Id { get; set; }
        public long CandidateId { get; set; }
        public long TradeId { get; set; }
        public string ENumber { get; set; }
        public DateTime? ENumberDate { get; set; }
        public string eNumberRemarks { get; set; }
    }
}
