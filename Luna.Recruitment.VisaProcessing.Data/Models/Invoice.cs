using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public class Invoice
    {
        public long Id { get; set; }
        public long? CandidateProfileId { get; set; }
        public long? VisaProcessId { get; set; }
        public long? Amount { get; set; }
        public long? InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public long? Balance { get; set; }
        public short? Status { get; set; }
        public virtual CandidateProfile CandidateProfile { get; set; }
        public virtual VisaProcess VisaProcess { get; set; }
    }
}
