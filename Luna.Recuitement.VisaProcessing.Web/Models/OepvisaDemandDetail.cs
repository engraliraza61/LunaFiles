using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Web.Models
{
    public partial class OepvisaDemandDetail
    {
        public long Id { get; set; }
        public long? OepvisaDemandId { get; set; }
        public long? JobTypeEntitySetupId { get; set; }
        public int? Quantity { get; set; }
        public long? CountryId { get; set; }
        public long? StateId { get; set; }
        public long? CityId { get; set; }
        public double? MinSalary { get; set; }
        public double? MaxSalary { get; set; }
        public int? DurationYears { get; set; }
        public int? DurationMonths { get; set; }
        public bool IsAccommodation { get; set; }
        public string AccommodationDetail { get; set; }
        public bool IsMedical { get; set; }
        public string MedicalDetail { get; set; }
        public bool IsTransport { get; set; }
        public string TransportDetail { get; set; }
        public bool IsAirTicket { get; set; }
        public string AirTicketDetail { get; set; }
        public bool IsOverTime { get; set; }
        public string OverTimeDetail { get; set; }
        public bool IsOthers { get; set; }
        public string OthersDetail { get; set; }
        public bool IsFood { get; set; }
        public string FoodDetail { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual EntitySetup JobTypeEntitySetup { get; set; }
        public virtual OepvisaDemand OepvisaDemand { get; set; }
        public virtual State State { get; set; }
    }
}
