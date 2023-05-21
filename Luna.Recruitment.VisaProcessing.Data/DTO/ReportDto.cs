using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
    public class ReportDto
    {
        public string Id { get; set; }
        public string visaProcessId { get; set; }
        public string OEPCode { get; set; }
        public string FileNumber { get; set; }
        public string Name { get; set; }
        public string Passport { get; set; }
        public string Trade { get; set; }
        public string VisaNumber { get; set; }
        public string VisaNumberDate { get; set; }
        public string ENumber { get; set; }
        public string ENumberDate { get; set; }
        public string StatusRemarks { get; set; }
        public string BiometricSendingDate { get; set; }
        public string BiometricReceivingDate { get; set; }
        public string MedicalOnlineSendingDate { get; set; }
        public string MedicalOnlineReceivingDate { get; set; }
        public string PassportSubmissionDate { get; set; }
        public string PassportReceivingDate { get; set; }
        public string Company { get; set; }
        public string Status { get; set; }
        public string Reference { get; set; }
        public string RefContact { get; set; }
        public string RegistrationNo { get; set; }
        public string RegistrationDate { get; set; }
        public string FlightNumber { get; set; }
        public string TicketNumber { get; set; }
        public string ArivalDate { get; set; }
        public string DepartureDate { get; set; }
        public string SectorFrom { get; set; }
        public string SectorTO { get; set; }
        public string DocumentHandOverDate { get; set; }
        public string DateOfCloure { get; set; }
        public string CNIC { get; set; }
        public string Amount { get; set; }
        public long TotalReceived { get; set; }
        public long TotalReceivable { get; set; }
        public long Balance { get; set; }
        public string Price { get; set; }
        public long TotalExpenses { get; set; }
        public string FatherName { get; set; }
        public string DeploymentDate { get; set; }
        public int TradeId { get; set; }
        public String ProcessingStartDate { get; set; }
        public string ProtectorDate { get; set; }
    }
    public class TabReport
    {
        public List<ReportDto> Start { get; set; }
        public List<ReportDto> ENumber { get; set; }
        public List<ReportDto> Biometric { get; set; }
        public List<ReportDto> MedicalOnline { get; set; }
        public List<ReportDto> Submission { get; set; }
        public List<ReportDto> Endorsement { get; set; }
        public List<ReportDto> Protector { get; set; }
        public List<ReportDto> Deployment { get; set; }
        public List<ReportDto> TravelBooking { get; set; }
        public List<ReportDto> CaseCloure { get; set; }
    }
}