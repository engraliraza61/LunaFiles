﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public partial class OepvisaDemand
    {
        public OepvisaDemand()
        {
            OepvisaDemandDetail = new HashSet<OepvisaDemandDetail>();
            PermissionRequest = new HashSet<PermissionRequest>();
            VisaProcess = new HashSet<VisaProcess>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string BatchNumber { get; set; }
        public string VisaNumberDate { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? Quantity { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public long? CounslateId { get; set; }
        public long? OepvisaDemandStatusEntitySetupId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public long? Oepid { get; set; }
        public long? SponserId { get; set; }
        public long? CountryId { get; set; }
        public long? ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string AbroadClientName { get; set; }
        public string AbroadClientPhone { get; set; }
        public DateTime? VisaNumberExpiryDate { get; set; }
        public string SponserGlcode { get; set; }
        public string PurchaseGlcode { get; set; }
        public int? ExpiryDays { get; set; }
        public string FileType { get; set; }
        public string VisaType { get; set; }
        public string SponserGroup { get; set; }
        public string VisaExpire { get; set; }

        public virtual Client Client { get; set; }
        public virtual Counslate Counslate { get; set; }
        public virtual Country Country { get; set; }
        public virtual Oep Oep { get; set; }
        public virtual EntitySetup OepvisaDemandStatusEntitySetup { get; set; }
        public virtual Sponser Sponser { get; set; }
        public virtual ICollection<OepvisaDemandDetail> OepvisaDemandDetail { get; set; }
        public virtual ICollection<PermissionRequest> PermissionRequest { get; set; }
        public virtual ICollection<VisaProcess> VisaProcess { get; set; }
    }
}
