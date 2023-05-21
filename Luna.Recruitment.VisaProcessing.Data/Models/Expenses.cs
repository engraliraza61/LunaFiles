using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public partial class Expenses
    {
        public long Id { get; set; }
        public long? CandidateProfileId { get; set; }
        public long? VisaProcessId { get; set; }
        public long? EntitySetupId { get; set; }
        public long? Amount { get; set; }
        public long? Balance { get; set; }
        public short? Status { get; set; }
        public virtual CandidateProfile CandidateProfile { get; set; }
        public virtual VisaProcess VisaProcess { get; set; }
        public virtual EntitySetup EntitySetup { get; set; }
        public DateTime? Date { get; set; }
        public string ReceiptType { get; set; }
        public string BankAccoutNo { get; set; }
        public string CheqNo { get; set; }
        public string VoucherNo { get; set; }
        public string GlStatus { get; set; }
    }
}
