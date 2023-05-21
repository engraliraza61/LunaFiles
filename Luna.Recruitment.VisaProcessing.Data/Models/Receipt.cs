using Luna.Recruitment.VisaProcessing.Data.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public partial class Receipt
    {
        public long Id { get; set; }
        public long? CandidateProfileId { get; set; }
        public long? VisaProcessId { get; set; }
        public DateTime? Date { get; set; }
        public string ReceiptType { get; set; }
        public string BankAccoutNo { get; set; }
        public string CheqNo { get; set; }
        public double? Amount { get; set; }
        public string ReceiptNo { get; set; }
        public virtual CandidateProfile CandidateProfile { get; set; }
        public virtual VisaProcess VisaProcess { get; set; }
        public string VoucherNo { get; set; }
        public string GlStatus { get; set; }
    }
}
