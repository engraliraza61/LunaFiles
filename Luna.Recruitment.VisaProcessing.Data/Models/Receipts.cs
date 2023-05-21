using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public class Receipts
    {
        public long Id { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime? Date { get; set; }
        public string Account { get; set; }
        public string ReceiptType { get; set; }
    }
}
