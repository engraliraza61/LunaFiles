using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
    public class PaymentRestClass
    {
        public long reference_no;
        public DateTime reference_date;

        public string doctype { get; set; }
        public string series { get; set; }
        public string payment_type { get; set; }
        public string posting_date { get; set; }
        public string Company { get; set; }
        public string paid_from { get; set; }
        public string Account_Currency { get; set; }
        public string paid_to { get; set; }
        public string paid_to_account_currency { get; set; }
        public float Exchange_Rate { get; set; }
        public float paid_amount { get; set; }
        public float received_amount { get; set; }
        public float target_exchange_rate { get; set; }
        public string party_type { get; set; }
        public string party { get; set; }
        public string mode_of_payment { get; set; }
    }
}
