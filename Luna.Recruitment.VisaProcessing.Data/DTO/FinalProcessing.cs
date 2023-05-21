using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
    public class FinalProcessing: ENumberReport
    {
        public string selectiongroup;

        public int Sr { get; set; }
        public string FileID { get; set; }
        public string NameOfCandidate { get; set; }
        public string FatherName { get; set; }
        public string Passport { get; set; }
        public string Profession { get; set; }
        public string Company { get; set; }
        public string Reference { get; set; }
        public DateTime StartingDate { get; set; }
        public string Contact { get; set; }
        public string SelectionTrade { get; set; }
        public string SelectionGroup { get; set; }
        public string Salary { get; set; }
        public string VaccineDose { get; set; }
        public string VaccineDate { get; set; }
        public string VaccineName { get; set; }
        public DateTime VisaStampingDate { get; set; }
        public DateTime VisaExpiryDate { get; set; }
        public string FoodDetail { get; set; }
        public string TotalReceivable { get; set; }
        public string CandidateId { get; set; }
        public string VisaProcessId { get; set; }
        public string Total { get; set; }
        public string UnderProcessing { get; set; }
        public string ArabicName { get; set; }
        public string TradeName { get; set; }
        public string Conslate { get; set; }
        public string Used { get; set; }
        public object Available { get; set; }
        public DateTime DeploymentDate { get; set; }
    }
    public class ENumberReport
    {
        public string GivenName { get; set; }
        public string SurName { get; set; }
        public string CnicNo { get; set; }
        public string Sector { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfIsse { get; set; }
        public string DateOfExpiry { get; set; }
        public string ENumber { get; set; }
        public string OEP { get; set; }
        public string Consulate { get; set; }
        public string Registration { get; set; }
        public string VisaProcessingStatus { get; set; }
        public string StatusRemarks { get; set; }
        public DateTime EDate { get; set; }
        public DateTime SubmittionDate { get; set; }
        public DateTime ReceivingDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Registrationdates { get; set; }

        public int PermissionNumebr { get; set; }
        public string PermissionDate { get; set; }
        public string NameOfPerson { get; set; }
        public string PassportNo { get; set; }
        public string RegistrationNumber { get; set; }
        public string Selector { get; set; }
        public DateTime DepartureDate { get; set; }
        public string FlightNumber { get; set; }
        public string TicketNumber { get; set; }
        public string FileNo { get; set; }
        public string VisaQuantity { get; set; }
        public string Trade { get; set; }

        

    }

}
