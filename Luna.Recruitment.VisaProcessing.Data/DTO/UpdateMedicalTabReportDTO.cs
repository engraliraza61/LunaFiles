using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
    public class UpdateMedicalTabReportDTO
    {
        public long Id { get; set; }
        public DateTime? MedicalOnlineReceivingDate { get; set; }
        public DateTime? MedicalOnlineSendingDate { get; set; }
        public string medicalRemarks { get; set; }
    }
}
