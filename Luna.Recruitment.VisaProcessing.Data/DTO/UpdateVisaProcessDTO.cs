using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
    public class UpdateVisaProcessDTO
    {
        public long Id { get; set; }
        public DateTime? MedicalOnlineSendingDate { get; set; }
        public DateTime? MedicalOnlineReceivingDate { get; set; }
        public DateTime? BiometricSendingDate { get; set; }
        public DateTime? BiometricReceivingDate { get; set; }
        public DateTime? PassportSubmissionDate { get; set; }
        public DateTime? PassportReceivingDate { get; set; }
        public string ENumber { get; set; }
        public DateTime? ENumberDate { get; set; }
    }
}
