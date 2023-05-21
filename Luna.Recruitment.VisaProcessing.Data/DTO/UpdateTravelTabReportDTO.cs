using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
    public class UpdateTravelTabReportDTO
    {
        public long Id { get; set; }
        public DateTime? DepartureDate1 { get; set; }
        public DateTime? ArrivalDate1 { get; set; }
        public string FlightNumber1 { get; set; }
        public string TicketNumber1 { get; set; }
        public long? Sector1From { get; set; }
        public long? Sector1To { get; set; }

    }
}
