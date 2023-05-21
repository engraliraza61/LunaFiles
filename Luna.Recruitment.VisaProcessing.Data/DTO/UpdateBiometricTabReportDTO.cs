using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
    public class UpdateBiometricTabReportDTO
    {
        public long Id { get; set; }
        public DateTime? BiometricReceivingDate { get; set; }
        public DateTime? BiometricSendingDate { get; set; }
        public string biometricRemarks { get; set; }
    }
}
