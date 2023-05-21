using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Web.Models
{
    public partial class EntitySetup
    {
        public EntitySetup()
        {
            CandidateDocument = new HashSet<CandidateDocument>();
            CandidatejobDetail = new HashSet<CandidatejobDetail>();
            Oeplicense = new HashSet<Oeplicense>();
            OepvisaDemandDetail = new HashSet<OepvisaDemandDetail>();
            OepvisaDemandPo = new HashSet<OepvisaDemandPo>();
            OepvisaDemandPodetail = new HashSet<OepvisaDemandPodetail>();
            PermissionDocumentMapDocumentTypeEntitySetup = new HashSet<PermissionDocumentMap>();
            PermissionDocumentMapPermissionTypeEntitySetup = new HashSet<PermissionDocumentMap>();
            VaccineVaccineTypeEntitySetup = new HashSet<Vaccine>();
            VaccineVaccineVariantEntitySetup = new HashSet<Vaccine>();
            VisaProcessSector1FromNavigation = new HashSet<VisaProcess>();
            VisaProcessSector1ToNavigation = new HashSet<VisaProcess>();
            VisaProcessSector2FromNavigation = new HashSet<VisaProcess>();
            VisaProcessSector2ToNavigation = new HashSet<VisaProcess>();
            VisaProcessSector3FromNavigation = new HashSet<VisaProcess>();
            VisaProcessSector3ToNavigation = new HashSet<VisaProcess>();
            VisaProcessStatusEntitySetup = new HashSet<VisaProcess>();
            VisaProcessVisaTradeEntitySetup = new HashSet<VisaProcess>();
        }

        public long Id { get; set; }
        public long EntityTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual EntityType EntityType { get; set; }
        public virtual ICollection<CandidateDocument> CandidateDocument { get; set; }
        public virtual ICollection<CandidatejobDetail> CandidatejobDetail { get; set; }
        public virtual ICollection<Oeplicense> Oeplicense { get; set; }
        public virtual ICollection<OepvisaDemandDetail> OepvisaDemandDetail { get; set; }
        public virtual ICollection<OepvisaDemandPo> OepvisaDemandPo { get; set; }
        public virtual ICollection<OepvisaDemandPodetail> OepvisaDemandPodetail { get; set; }
        public virtual ICollection<PermissionDocumentMap> PermissionDocumentMapDocumentTypeEntitySetup { get; set; }
        public virtual ICollection<PermissionDocumentMap> PermissionDocumentMapPermissionTypeEntitySetup { get; set; }
        public virtual ICollection<Vaccine> VaccineVaccineTypeEntitySetup { get; set; }
        public virtual ICollection<Vaccine> VaccineVaccineVariantEntitySetup { get; set; }
        public virtual ICollection<VisaProcess> VisaProcessSector1FromNavigation { get; set; }
        public virtual ICollection<VisaProcess> VisaProcessSector1ToNavigation { get; set; }
        public virtual ICollection<VisaProcess> VisaProcessSector2FromNavigation { get; set; }
        public virtual ICollection<VisaProcess> VisaProcessSector2ToNavigation { get; set; }
        public virtual ICollection<VisaProcess> VisaProcessSector3FromNavigation { get; set; }
        public virtual ICollection<VisaProcess> VisaProcessSector3ToNavigation { get; set; }
        public virtual ICollection<VisaProcess> VisaProcessStatusEntitySetup { get; set; }
        public virtual ICollection<VisaProcess> VisaProcessVisaTradeEntitySetup { get; set; }
    }
}
