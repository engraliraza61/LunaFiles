using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public class ReceiptsDetail
    {
        [Key]
        public long Id { get; set; }
        public long ReceiptsId { get; set; }
        public long AgentId { get; set; }
        public long OepVisaDemandId { get; set; }
        public long CandidateProfileId { get; set; }
        public string CheckNo { get; set; }
        public string Amount { get; set; }
        public string VoucherNo { get; set; }
        public string GlStatus { get; set; }
        public string Description { get; set; }
    }
}
