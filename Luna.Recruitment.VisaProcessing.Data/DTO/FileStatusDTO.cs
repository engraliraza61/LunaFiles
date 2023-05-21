using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
    public class FileStatusDTO
    {
        public string FileID { get; set; }
        public string Total { get; set; }
        public string Trade { get; set; }
        public string MedicallyUnfit { get; set; }
        public string MedicalUnderProcess { get; set; }
        public string Biometric_M_R_onlineProcess { get; set; }
        public string PendingDocuments { get; set; }
        public string ReadyforSubmission { get; set; }
        public string SubmittedforVisaStamping { get; set; }
        public string VisaStamped { get; set; }
        public string Declined { get; set; }
        public string VisaExpired { get; set; }
        public string Deployed { get; set; }
        public string DegreeAttestationUnderProcess { get; set; }
        public string PassportApplied { get; set; }
        public string Objection { get; set; }
        public string StoppedbyManagement { get; set; }
        public string StoppedbyCompany { get; set; }
        public string UnderSelection { get; set; }
        public string DemandRequired { get; set; }
        public string WakalaAwaited { get; set; }
        public string VisaCancelled { get; set; }
        public string Protector { get; set; }
        public string NoShow { get; set; }
        public string FlightCancelled { get; set; }
        public string ReadyToPrint { get; set; }
        public string MedicallyFIT { get; set; }
        public string PassportReturned { get; set; }
        public object Available { get; set; }
        public string ArabicName { get; set; }
    }
}
