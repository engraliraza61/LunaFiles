using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.DTO
{
    public class VisaQuantityValidationDto
    {
        public int? expiryDays;
        public int? expiryDay;

        public int Balance { get; set; }
        public long OEPVisaDemandId { get; set; }
        public long OEPVisaDemandDetailId { get; set; }
        public int? ExpiryDays { get; set; }
    }
}
