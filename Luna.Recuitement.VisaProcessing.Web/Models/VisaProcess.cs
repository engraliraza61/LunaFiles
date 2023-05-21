using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Web.Models
{
    public partial class VisaProcess
    {
        public long Id { get; set; }
        public long? OepvisaDemandId { get; set; }
        public long? VisaTradeEntitySetupId { get; set; }
        public long? CandidateSelectionDetailId { get; set; }
        public DateTime? ProcessingStartDate { get; set; }
        public DateTime? DocumentReceivingDate { get; set; }
        public DateTime? MedicalOnlineSendingDate { get; set; }
        public DateTime? MedicalOnlineReceivingDate { get; set; }
        public DateTime? BiometricSendingDate { get; set; }
        public DateTime? BiometricReceivingDate { get; set; }
        public string Enumber { get; set; }
        public DateTime? EnumberDate { get; set; }
        public DateTime? PassportSubmissionDate { get; set; }
        public DateTime? PassportReceivingDate { get; set; }
        public long? StatusEntitySetupId { get; set; }
        public string StatusRemarks { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? DepartureDate1 { get; set; }
        public DateTime? ArrivalDate1 { get; set; }
        public string FlightNumber1 { get; set; }
        public string TicketNumber1 { get; set; }
        public long? Sector1From { get; set; }
        public long? Sector1To { get; set; }
        public DateTime? DepartureDate2 { get; set; }
        public DateTime? ArrivalDate2 { get; set; }
        public string FlightNumber2 { get; set; }
        public string TicketNumber2 { get; set; }
        public long? Sector2From { get; set; }
        public long? Sector2To { get; set; }
        public DateTime? DepartureDate3 { get; set; }
        public DateTime? ArrivalDate3 { get; set; }
        public string FlightNumber3 { get; set; }
        public string TicketNumber3 { get; set; }
        public long? Sector3From { get; set; }
        public long? Sector3To { get; set; }
        public string VisisSerialNumber { get; set; }

        public virtual CandidateSelectionDetail CandidateSelectionDetail { get; set; }
        public virtual OepvisaDemand OepvisaDemand { get; set; }
        public virtual EntitySetup Sector1FromNavigation { get; set; }
        public virtual EntitySetup Sector1ToNavigation { get; set; }
        public virtual EntitySetup Sector2FromNavigation { get; set; }
        public virtual EntitySetup Sector2ToNavigation { get; set; }
        public virtual EntitySetup Sector3FromNavigation { get; set; }
        public virtual EntitySetup Sector3ToNavigation { get; set; }
        public virtual EntitySetup StatusEntitySetup { get; set; }
        public virtual EntitySetup VisaTradeEntitySetup { get; set; }
    }
}
