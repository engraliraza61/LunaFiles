﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public partial class Oep
    {
        public Oep()
        {
            Oeplicense = new HashSet<Oeplicense>();
            OepvisaDemand = new HashSet<OepvisaDemand>();
            OepvisaDemandPo = new HashSet<OepvisaDemandPo>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public long? CountryId { get; set; }
        public long? StateId { get; set; }
        public long? CityId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<Oeplicense> Oeplicense { get; set; }
        public virtual ICollection<OepvisaDemand> OepvisaDemand { get; set; }
        public virtual ICollection<OepvisaDemandPo> OepvisaDemandPo { get; set; }
    }
}
