using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.DTO
{
    public class ReceiptAgentJV
    {
        public int FileId { get; set; }
        public string Code { get; set; }
        public long PassportId { get; set; }
        public string PassportNo { get; set; }
        public string CandidateName { get; set; }
        public double? TotalReceivable { get; set; }
    }
}
