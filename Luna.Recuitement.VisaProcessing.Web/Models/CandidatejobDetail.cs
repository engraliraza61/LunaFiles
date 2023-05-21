using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Web.Models
{
    public partial class CandidatejobDetail
    {
        public long Id { get; set; }
        public long CandidateSelectionDetailId { get; set; }
        public int? DurationYears { get; set; }
        public int? DurationMonths { get; set; }
        public string Currency { get; set; }
        public double? Salary { get; set; }
        public int? DutyHours { get; set; }
        public long? SelectionTradeEntitySetupId { get; set; }
        public string Reference { get; set; }
        public string ReferenceName { get; set; }
        public string ReferencePhone { get; set; }
        public bool IsAccommodation { get; set; }
        public string AccommodationDetail { get; set; }
        public bool IsMedical { get; set; }
        public string MedicalDetail { get; set; }
        public bool IsTransport { get; set; }
        public string TransportDetail { get; set; }
        public bool IsAirTicket { get; set; }
        public string AirTicketDetail { get; set; }
        public bool IsOvertime { get; set; }
        public string OvertimeDetail { get; set; }
        public bool IsFood { get; set; }
        public string FoodDetail { get; set; }
        public bool IsOthers { get; set; }
        public string OthersDetail { get; set; }

        public virtual CandidateSelectionDetail CandidateSelectionDetail { get; set; }
        public virtual EntitySetup SelectionTradeEntitySetup { get; set; }
    }
}
