using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
    public class JVRestClass
    {
        public int ID { get; set; }
        public string EntryType { get; set; }
        public string Series { get; set; }
        public string company { get; set; }
        public string posting_date { get; set; }
        public long total_debit { get; set; }
        public long total_credit { get; set; }
        public string exchange_rate { get; set; }
        public string user_remark { get; set; }
        public virtual ICollection<accounts> accounts { get; set; }
    }
    public class accounts
    {
        public string account { get; set; }
        public long debit { get; set; }
        public long credit { get; set; }
        public string debit_in_account_currency { get; set; }
        public string credit_in_account_currency { get; set; }
    }
}
