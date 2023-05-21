using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
    public class MedicalCenterDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
        public string GHCCode { get; set; }

    }
}
