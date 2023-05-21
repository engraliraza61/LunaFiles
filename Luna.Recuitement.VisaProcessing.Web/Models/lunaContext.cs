using System;
using Luna.Recruitment.VisaProcessing.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Web.Models
{
    public partial class lunaContext : DbContext
    {
        public lunaContext()
        {
        }

        public lunaContext(DbContextOptions<lunaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<BulkInsertion> BulkInsertion { get; set; }
        public virtual DbSet<BulkSelectionMaster> BulkSelectionMaster { get; set; }
        public virtual DbSet<CandidateDocument> CandidateDocument { get; set; }
        public virtual DbSet<CandidateProfile> CandidateProfile { get; set; }
        public virtual DbSet<CandidateSelection> CandidateSelection { get; set; }
        public virtual DbSet<CandidateSelectionDetail> CandidateSelectionDetail { get; set; }
        public virtual DbSet<CandidatejobDetail> CandidatejobDetail { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Counslate> Counslate { get; set; }
        public virtual DbSet<CounslateVisaFormTemplate> CounslateVisaFormTemplate { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Dependent> Dependent { get; set; }
        public virtual DbSet<EntitySetup> EntitySetup { get; set; }
        public virtual DbSet<EntityType> EntityType { get; set; }
        public virtual DbSet<FollowUp> FollowUp { get; set; }
        public virtual DbSet<Medical> Medical { get; set; }
        public virtual DbSet<MedicalCenter> MedicalCenter { get; set; }
        public virtual DbSet<Mehrum> Mehrum { get; set; }
        public virtual DbSet<Nominee> Nominee { get; set; }
        public virtual DbSet<Oep> Oep { get; set; }
        public virtual DbSet<Oeplicense> Oeplicense { get; set; }
        public virtual DbSet<OepvisaDemand> OepvisaDemand { get; set; }
        public virtual DbSet<OepvisaDemandDetail> OepvisaDemandDetail { get; set; }
        public virtual DbSet<OepvisaDemandPo> OepvisaDemandPo { get; set; }
        public virtual DbSet<OepvisaDemandPodetail> OepvisaDemandPodetail { get; set; }
        public virtual DbSet<Passport> Passport { get; set; }
        public virtual DbSet<PermissionDocumentMap> PermissionDocumentMap { get; set; }
        public virtual DbSet<PermissionRequest> PermissionRequest { get; set; }
        public virtual DbSet<Sponser> Sponser { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<TestCenter> TestCenter { get; set; }
        public virtual DbSet<Vaccine> Vaccine { get; set; }
        public virtual DbSet<VisaProcess> VisaProcess { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Luna;User Id=sa;Password=Abcd@1234;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(100);

                entity.Property(e => e.Phone2).HasMaxLength(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Agent)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Agent_City");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Agent)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Agent_Country");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Agent)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Agent_State");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<BulkInsertion>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BirthCountry).HasMaxLength(50);

                entity.Property(e => e.Cnic)
                    .HasColumnName("CNIC")
                    .HasMaxLength(50);

                entity.Property(e => e.CnicexpiryDate)
                    .HasColumnName("CNICExpiryDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.CnicissueDate)
                    .HasColumnName("CNICIssueDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.MaritalStatus).HasMaxLength(50);
            });

            modelBuilder.Entity<BulkSelectionMaster>(entity =>
            {
                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.UploadDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CandidateDocument>(entity =>
            {
                entity.Property(e => e.FilePath).HasMaxLength(50);

                entity.Property(e => e.FileTypeEntitySetupId).HasColumnName("FileType_EntitySetupId");

                entity.Property(e => e.UploadDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CandidateProfile)
                    .WithMany(p => p.CandidateDocument)
                    .HasForeignKey(d => d.CandidateProfileId)
                    .HasConstraintName("FK_CandidateDocument_CandidateProfile");

                entity.HasOne(d => d.FileTypeEntitySetup)
                    .WithMany(p => p.CandidateDocument)
                    .HasForeignKey(d => d.FileTypeEntitySetupId)
                    .HasConstraintName("FK_CandidateDocument_EntitySetup");
            });

            modelBuilder.Entity<CandidateProfile>(entity =>
            {
                entity.Property(e => e.BusinessAddress).HasMaxLength(50);

                entity.Property(e => e.BusinessPhone).HasMaxLength(50);

                entity.Property(e => e.CellNumber).HasMaxLength(50);

                entity.Property(e => e.Cnic)
                    .HasColumnName("CNIC")
                    .HasMaxLength(50);

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Domicile).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.FatherArabicName).HasColumnName("Father_ArabicName");

                entity.Property(e => e.FatherName).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.HusbandArabicName)
                    .HasColumnName("Husband_ArabicName")
                    .HasMaxLength(50);

                entity.Property(e => e.HusbandName).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MaritalStatus).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.PlaceOfBirthCityId).HasColumnName("PlaceOfBirth_CityId");

                entity.Property(e => e.PlaceOfBirthCountryId).HasColumnName("PlaceOfBirth_CountryId");

                entity.Property(e => e.PreviousNationalityCountryId).HasColumnName("PreviousNationality_CountryId");

                entity.Property(e => e.Qualification).HasMaxLength(50);

                entity.Property(e => e.Reference).HasMaxLength(50);

                entity.Property(e => e.ReferenceName).HasMaxLength(50);

                entity.Property(e => e.ReferencePhone).HasMaxLength(50);

                entity.Property(e => e.Religion).HasMaxLength(50);

                entity.Property(e => e.Sect).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.CandidateProfile)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_CandidateProfile_Agent");

                entity.HasOne(d => d.PlaceOfBirthCity)
                    .WithMany(p => p.CandidateProfile)
                    .HasForeignKey(d => d.PlaceOfBirthCityId)
                    .HasConstraintName("FK_CandidateProfile_City");

                entity.HasOne(d => d.PlaceOfBirthCountry)
                    .WithMany(p => p.CandidateProfilePlaceOfBirthCountry)
                    .HasForeignKey(d => d.PlaceOfBirthCountryId)
                    .HasConstraintName("FK_CandidateProfile_Country");

                entity.HasOne(d => d.PreviousNationalityCountry)
                    .WithMany(p => p.CandidateProfilePreviousNationalityCountry)
                    .HasForeignKey(d => d.PreviousNationalityCountryId)
                    .HasConstraintName("FK_CandidateProfile_Country1");
            });

            modelBuilder.Entity<CandidateSelection>(entity =>
            {
                entity.Property(e => e.SelectionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SponserId).HasComment("SponserId for which the selection is done.");

                entity.HasOne(d => d.Sponser)
                    .WithMany(p => p.CandidateSelection)
                    .HasForeignKey(d => d.SponserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateSelection_Sponser");
            });

            modelBuilder.Entity<CandidateSelectionDetail>(entity =>
            {
                entity.HasOne(d => d.CandidateProfile)
                    .WithMany(p => p.CandidateSelectionDetail)
                    .HasForeignKey(d => d.CandidateProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateSelectionDetail_CandidateProfile");

                entity.HasOne(d => d.CandidateSelection)
                    .WithMany(p => p.CandidateSelectionDetail)
                    .HasForeignKey(d => d.CandidateSelectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateSelectionDetail_CandidateSelection");
            });

            modelBuilder.Entity<CandidatejobDetail>(entity =>
            {
                entity.Property(e => e.Currency).HasMaxLength(50);

                entity.Property(e => e.Reference).HasMaxLength(50);

                entity.Property(e => e.ReferenceName).HasMaxLength(50);

                entity.Property(e => e.ReferencePhone).HasMaxLength(50);

                entity.Property(e => e.SelectionTradeEntitySetupId).HasColumnName("SelectionTrade_EntitySetupId");

                entity.HasOne(d => d.CandidateSelectionDetail)
                    .WithMany(p => p.CandidatejobDetail)
                    .HasForeignKey(d => d.CandidateSelectionDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidatejobDetail_CandidateSelectionDetail");

                entity.HasOne(d => d.SelectionTradeEntitySetup)
                    .WithMany(p => p.CandidatejobDetail)
                    .HasForeignKey(d => d.SelectionTradeEntitySetupId)
                    .HasConstraintName("FK_CandidatejobDetail_EntitySetup");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_Region");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Client_City");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Client_Country");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Client_State");
            });

            modelBuilder.Entity<Counslate>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Counslate)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Counslate_City");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Counslate)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Counslate_Country");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Counslate)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Counslate_State");
            });

            modelBuilder.Entity<CounslateVisaFormTemplate>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Counslate)
                    .WithMany(p => p.CounslateVisaFormTemplate)
                    .HasForeignKey(d => d.CounslateId)
                    .HasConstraintName("FK_CounslateVisaFormTemplate_Counslate");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Dependent>(entity =>
            {
                entity.Property(e => e.ArabicName).HasMaxLength(50);

                entity.Property(e => e.Cnic)
                    .HasColumnName("CNIC")
                    .HasMaxLength(50);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Relationship).HasMaxLength(50);

                entity.HasOne(d => d.CandidateProfile)
                    .WithMany(p => p.Dependent)
                    .HasForeignKey(d => d.CandidateProfileId)
                    .HasConstraintName("FK_Dependent_CandidateProfile");
            });

            modelBuilder.Entity<EntitySetup>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.EntityType)
                    .WithMany(p => p.EntitySetup)
                    .HasForeignKey(d => d.EntityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EntitySetup_EntityType");
            });

            modelBuilder.Entity<EntityType>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<FollowUp>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FollowUpDate).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.CandidateSelectionDetail)
                    .WithMany(p => p.FollowUp)
                    .HasForeignKey(d => d.CandidateSelectionDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FollowUp_CandidateSelectionDetail");
            });

            modelBuilder.Entity<Medical>(entity =>
            {
                entity.Property(e => e.DateExamined).HasColumnType("datetime");

                entity.Property(e => e.GccslipNo)
                    .HasColumnName("GCCSlipNo")
                    .HasMaxLength(50);

                entity.Property(e => e.GhccodeNo)
                    .HasColumnName("GHCCodeNo")
                    .HasMaxLength(50);

                entity.Property(e => e.MedicalCenterName).HasMaxLength(50);

                entity.Property(e => e.ReportExpiryDate).HasColumnType("datetime");

                entity.HasOne(d => d.CandidateProfile)
                    .WithMany(p => p.Medical)
                    .HasForeignKey(d => d.CandidateProfileId)
                    .HasConstraintName("FK_Medical_CandidateProfile1");

                entity.HasOne(d => d.Sponser)
                    .WithMany(p => p.Medical)
                    .HasForeignKey(d => d.SponserId)
                    .HasConstraintName("FK_Medical_Sponser");
            });

            modelBuilder.Entity<MedicalCenter>(entity =>
            {
                entity.Property(e => e.Gccslip)
                    .HasColumnName("GCCSlip")
                    .HasMaxLength(50);

                entity.Property(e => e.Ghccode)
                    .HasColumnName("GHCCode")
                    .HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.MedicalCenter)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_MedicalCenter_City");
            });

            modelBuilder.Entity<Mehrum>(entity =>
            {
                entity.Property(e => e.ArabicName).HasMaxLength(50);

                entity.Property(e => e.Cnic)
                    .HasColumnName("CNIC")
                    .HasMaxLength(50);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Relationship).HasMaxLength(50);

                entity.HasOne(d => d.CandidateProfile)
                    .WithMany(p => p.Mehrum)
                    .HasForeignKey(d => d.CandidateProfileId)
                    .HasConstraintName("FK_Mehrum_CandidateProfile");
            });

            modelBuilder.Entity<Nominee>(entity =>
            {
                entity.Property(e => e.ArabicName).HasMaxLength(50);

                entity.Property(e => e.Cnic)
                    .HasColumnName("CNIC")
                    .HasMaxLength(50);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Relationship).HasMaxLength(50);

                entity.HasOne(d => d.CandidateProfile)
                    .WithMany(p => p.Nominee)
                    .HasForeignKey(d => d.CandidateProfileId)
                    .HasConstraintName("FK_Nominee_CandidateProfile");
            });

            modelBuilder.Entity<Oep>(entity =>
            {
                entity.ToTable("OEP");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Oep)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_OEP_City");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Oep)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_OEP_Country");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Oep)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_OEP_State");
            });

            modelBuilder.Entity<Oeplicense>(entity =>
            {
                entity.ToTable("OEPLicense");

                entity.Property(e => e.Boaddress).HasColumnName("BOAddress");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.Hoaddress).HasColumnName("HOAddress");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LicenseNumber).HasMaxLength(50);

                entity.Property(e => e.Oepid).HasColumnName("OEPId");

                entity.Property(e => e.OepstatusEntitySetupId).HasColumnName("OEPStatus_EntitySetupId");

                entity.Property(e => e.ProprieterName).HasMaxLength(250);

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.Oep)
                    .WithMany(p => p.Oeplicense)
                    .HasForeignKey(d => d.Oepid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OEPLicense_OEP");

                entity.HasOne(d => d.OepstatusEntitySetup)
                    .WithMany(p => p.Oeplicense)
                    .HasForeignKey(d => d.OepstatusEntitySetupId)
                    .HasConstraintName("FK_OEPLicense_EntitySetup");
            });

            modelBuilder.Entity<OepvisaDemand>(entity =>
            {
                entity.ToTable("OEPVisaDemand");

                entity.Property(e => e.AbroadClientName).HasMaxLength(100);

                entity.Property(e => e.AbroadClientPhone).HasMaxLength(100);

                entity.Property(e => e.BatchNumber)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ClientName).HasMaxLength(100);

                entity.Property(e => e.ClientPhone).HasMaxLength(100);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.FileType).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.Oepid).HasColumnName("OEPId");

                entity.Property(e => e.OepvisaDemandStatusEntitySetupId).HasColumnName("OEPVisaDemandStatus_EntitySetupId");

                entity.Property(e => e.PurchaseGlcode)
                    .HasColumnName("PurchaseGLCode")
                    .HasMaxLength(50);

                entity.Property(e => e.ReceivingDate).HasColumnType("datetime");

                entity.Property(e => e.SponserGlcode)
                    .HasColumnName("SponserGLCode")
                    .HasMaxLength(50);

                entity.Property(e => e.SponserGroup).HasMaxLength(50);

                entity.Property(e => e.VisaNumberDate).HasMaxLength(50);

                entity.Property(e => e.VisaNumberExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.VisaType).HasMaxLength(50);
            });

            modelBuilder.Entity<OepvisaDemandDetail>(entity =>
            {
                entity.ToTable("OEPVisaDemandDetail");

                entity.Property(e => e.AirTicketDetail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FoodDetail).HasMaxLength(255);

                entity.Property(e => e.JobTypeEntitySetupId).HasColumnName("JobType_EntitySetupId");

                entity.Property(e => e.MedicalDetail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OepvisaDemandId).HasColumnName("OEPVisaDemandId");

                entity.Property(e => e.OthersDetail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OverTimeDetail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TransportDetail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.OepvisaDemandDetail)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_OEPVisaDemandDetail_City");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.OepvisaDemandDetail)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_OEPVisaDemandDetail_Country");

                entity.HasOne(d => d.JobTypeEntitySetup)
                    .WithMany(p => p.OepvisaDemandDetail)
                    .HasForeignKey(d => d.JobTypeEntitySetupId)
                    .HasConstraintName("FK_OEPVisaDemandDetail_EntitySetup");

                entity.HasOne(d => d.OepvisaDemand)
                    .WithMany(p => p.OepvisaDemandDetail)
                    .HasForeignKey(d => d.OepvisaDemandId)
                    .HasConstraintName("FK_OEPVisaDemandDetail_OEPVisaDemand");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.OepvisaDemandDetail)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_OEPVisaDemandDetail_State");
            });

            modelBuilder.Entity<OepvisaDemandPo>(entity =>
            {
                entity.ToTable("OEPVisaDemandPO");

                entity.Property(e => e.AbroadClientName).HasMaxLength(100);

                entity.Property(e => e.AbroadClientPhone).HasMaxLength(100);

                entity.Property(e => e.BatchNumber).HasMaxLength(250);

                entity.Property(e => e.ClientName).HasMaxLength(100);

                entity.Property(e => e.ClientPhone).HasMaxLength(100);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.FileType).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.Mode)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Oepid).HasColumnName("OEPId");

                entity.Property(e => e.OepvisaDemandPostatusEntitySetupId).HasColumnName("OEPVisaDemandPOStatus_EntitySetupId");

                entity.Property(e => e.Podate)
                    .HasColumnName("PODate")
                    .HasColumnType("datetime");

                entity.Property(e => e.PurchaseGlcode)
                    .HasColumnName("PurchaseGLCode")
                    .HasMaxLength(50);

                entity.Property(e => e.ReceivingDate).HasColumnType("datetime");

                entity.Property(e => e.SponserGlcode)
                    .HasColumnName("SponserGLCode")
                    .HasMaxLength(50);

                entity.Property(e => e.SponserGroup).HasMaxLength(50);

                entity.Property(e => e.VisaNumberDate).HasColumnType("datetime");

                entity.Property(e => e.VisaNumberExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.VisaType).HasMaxLength(50);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.OepvisaDemandPo)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_OEPVisaDemandPO_Client");

                entity.HasOne(d => d.Counslate)
                    .WithMany(p => p.OepvisaDemandPo)
                    .HasForeignKey(d => d.CounslateId)
                    .HasConstraintName("FK_OEPVisaDemandPO_Counslate");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.OepvisaDemandPo)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_OEPVisaDemandPO_Country");

                entity.HasOne(d => d.Oep)
                    .WithMany(p => p.OepvisaDemandPo)
                    .HasForeignKey(d => d.Oepid)
                    .HasConstraintName("FK_OEPVisaDemandPO_OEP");

                entity.HasOne(d => d.OepvisaDemandPostatusEntitySetup)
                    .WithMany(p => p.OepvisaDemandPo)
                    .HasForeignKey(d => d.OepvisaDemandPostatusEntitySetupId)
                    .HasConstraintName("FK_OEPVisaDemandPO_EntitySetup");

                entity.HasOne(d => d.Sponser)
                    .WithMany(p => p.OepvisaDemandPo)
                    .HasForeignKey(d => d.SponserId)
                    .HasConstraintName("FK_OEPVisaDemandPO_Sponser");
            });

            modelBuilder.Entity<OepvisaDemandPodetail>(entity =>
            {
                entity.ToTable("OEPVisaDemandPODetail");

                entity.Property(e => e.AirTicketDetail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FoodDetail).HasMaxLength(255);

                entity.Property(e => e.JobTypeEntitySetupId).HasColumnName("JobType_EntitySetupId");

                entity.Property(e => e.MedicalDetail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OepvisaDemandPoid).HasColumnName("OEPVisaDemandPOId");

                entity.Property(e => e.OthersDetail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OverTimeDetail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TransportDetail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.OepvisaDemandPodetail)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_OEPVisaDemandPODetail_City");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.OepvisaDemandPodetail)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_OEPVisaDemandPODetail_Country");

                entity.HasOne(d => d.JobTypeEntitySetup)
                    .WithMany(p => p.OepvisaDemandPodetail)
                    .HasForeignKey(d => d.JobTypeEntitySetupId)
                    .HasConstraintName("FK_OEPVisaDemandPODetail_EntitySetup");

                entity.HasOne(d => d.OepvisaDemandPo)
                    .WithMany(p => p.OepvisaDemandPodetail)
                    .HasForeignKey(d => d.OepvisaDemandPoid)
                    .HasConstraintName("FK_OEPVisaDemandPODetail_OEPVisaDemandPO");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.OepvisaDemandPodetail)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_OEPVisaDemandPODetail_State");
            });

            modelBuilder.Entity<Passport>(entity =>
            {
                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PassportNo).HasMaxLength(50);

                entity.Property(e => e.PlaceOfBirthCityId).HasColumnName("PlaceOfBirth_CityId");

                entity.Property(e => e.PlaceOfBirthCountryId).HasColumnName("PlaceOfBirth_CountryId");

                entity.Property(e => e.PlaceOfBirthStateId).HasColumnName("PlaceOfBirth_StateId");

                entity.Property(e => e.PlaceOfIssueCountryId).HasColumnName("PlaceOfIssue_CountryId");

                entity.Property(e => e.Religion).HasMaxLength(50);

                entity.HasOne(d => d.CandidateProfile)
                    .WithMany(p => p.Passport)
                    .HasForeignKey(d => d.CandidateProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Passport_CandidateProfile");

                entity.HasOne(d => d.PlaceOfBirthCity)
                    .WithMany(p => p.Passport)
                    .HasForeignKey(d => d.PlaceOfBirthCityId)
                    .HasConstraintName("FK_Passport_City");

                entity.HasOne(d => d.PlaceOfBirthCountry)
                    .WithMany(p => p.PassportPlaceOfBirthCountry)
                    .HasForeignKey(d => d.PlaceOfBirthCountryId)
                    .HasConstraintName("FK_Passport_Country1");

                entity.HasOne(d => d.PlaceOfBirthState)
                    .WithMany(p => p.Passport)
                    .HasForeignKey(d => d.PlaceOfBirthStateId)
                    .HasConstraintName("FK_Passport_State");

                entity.HasOne(d => d.PlaceOfIssueCountry)
                    .WithMany(p => p.PassportPlaceOfIssueCountry)
                    .HasForeignKey(d => d.PlaceOfIssueCountryId)
                    .HasConstraintName("FK_Passport_Country");
            });

            modelBuilder.Entity<PermissionDocumentMap>(entity =>
            {
                entity.Property(e => e.DocumentTypeEntitySetupId).HasColumnName("DocumentType_EntitySetupId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PermissionTypeEntitySetupId).HasColumnName("PermissionType_EntitySetupId");

                entity.HasOne(d => d.DocumentTypeEntitySetup)
                    .WithMany(p => p.PermissionDocumentMapDocumentTypeEntitySetup)
                    .HasForeignKey(d => d.DocumentTypeEntitySetupId)
                    .HasConstraintName("FK_PermissionDocumentMap_EntitySetup1");

                entity.HasOne(d => d.PermissionTypeEntitySetup)
                    .WithMany(p => p.PermissionDocumentMapPermissionTypeEntitySetup)
                    .HasForeignKey(d => d.PermissionTypeEntitySetupId)
                    .HasConstraintName("FK_PermissionDocumentMap_EntitySetup");
            });

            modelBuilder.Entity<PermissionRequest>(entity =>
            {
                entity.Property(e => e.ApplyDate).HasColumnType("datetime");

                entity.Property(e => e.OepvisaDemandId).HasColumnName("OEPVisaDemandId");

                entity.Property(e => e.PermissionNumber).HasMaxLength(50);

                entity.Property(e => e.PermissionTypeEntitySetupId).HasColumnName("PermissionType_EntitySetupId");

                entity.Property(e => e.ReceivingDate).HasColumnType("datetime");

                entity.Property(e => e.ValidityDate)
                    .HasColumnType("datetime")
                    .HasComment("ReceivingDate + 120");

                entity.HasOne(d => d.OepvisaDemand)
                    .WithMany(p => p.PermissionRequest)
                    .HasForeignKey(d => d.OepvisaDemandId)
                    .HasConstraintName("FK_PermissionRequest_OEPVisaDemand");
            });

            modelBuilder.Entity<Sponser>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CompanyShortName).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.ProjectName).HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Sponser)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Sponser_City");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Sponser)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Sponser_Country");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Sponser)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Sponser_State");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ArabicName).HasMaxLength(50);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.State)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Region_Country");
            });

            modelBuilder.Entity<TestCenter>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.TestCenter)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_TestCenter_City");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TestCenter)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_TestCenter_Country");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.TestCenter)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_TestCenter_State");
            });

            modelBuilder.Entity<Vaccine>(entity =>
            {
                entity.Property(e => e.VaccineDate).HasColumnType("datetime");

                entity.Property(e => e.VaccineDose).HasMaxLength(100);

                entity.Property(e => e.VaccineTypeEntitySetupId).HasColumnName("VaccineType_EntitySetupId");

                entity.Property(e => e.VaccineValidity).HasColumnType("datetime");

                entity.Property(e => e.VaccineVariantEntitySetupId).HasColumnName("VaccineVariant_EntitySetupId");

                entity.HasOne(d => d.CandidateProfile)
                    .WithMany(p => p.Vaccine)
                    .HasForeignKey(d => d.CandidateProfileId)
                    .HasConstraintName("FK_Vaccine_CandidateProfile1");

                entity.HasOne(d => d.VaccineTypeEntitySetup)
                    .WithMany(p => p.VaccineVaccineTypeEntitySetup)
                    .HasForeignKey(d => d.VaccineTypeEntitySetupId)
                    .HasConstraintName("FK_Vaccine_EntitySetup");

                entity.HasOne(d => d.VaccineVariantEntitySetup)
                    .WithMany(p => p.VaccineVaccineVariantEntitySetup)
                    .HasForeignKey(d => d.VaccineVariantEntitySetupId)
                    .HasConstraintName("FK_Vaccine_EntitySetup1");
            });

            modelBuilder.Entity<VisaProcess>(entity =>
            {
                entity.Property(e => e.ArrivalDate1).HasColumnType("datetime");

                entity.Property(e => e.ArrivalDate2).HasColumnType("datetime");

                entity.Property(e => e.ArrivalDate3).HasColumnType("datetime");

                entity.Property(e => e.BiometricReceivingDate).HasColumnType("datetime");

                entity.Property(e => e.BiometricSendingDate).HasColumnType("datetime");

                entity.Property(e => e.DepartureDate1).HasColumnType("datetime");

                entity.Property(e => e.DepartureDate2).HasColumnType("datetime");

                entity.Property(e => e.DepartureDate3).HasColumnType("datetime");

                entity.Property(e => e.DocumentReceivingDate).HasColumnType("datetime");

                entity.Property(e => e.Enumber)
                    .HasColumnName("ENumber")
                    .HasMaxLength(50);

                entity.Property(e => e.EnumberDate)
                    .HasColumnName("ENumberDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FlightNumber1).HasMaxLength(50);

                entity.Property(e => e.FlightNumber2).HasMaxLength(50);

                entity.Property(e => e.FlightNumber3).HasMaxLength(50);

                entity.Property(e => e.MedicalOnlineReceivingDate).HasColumnType("datetime");

                entity.Property(e => e.MedicalOnlineSendingDate).HasColumnType("datetime");

                entity.Property(e => e.OepvisaDemandId).HasColumnName("OEPVisaDemandId");

                entity.Property(e => e.PassportReceivingDate).HasColumnType("datetime");

                entity.Property(e => e.PassportSubmissionDate).HasColumnType("datetime");

                entity.Property(e => e.ProcessingStartDate).HasColumnType("datetime");

                entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

                entity.Property(e => e.RegistrationNumber).HasMaxLength(50);

                entity.Property(e => e.StatusEntitySetupId).HasColumnName("Status_EntitySetupId");

                entity.Property(e => e.TicketNumber1).HasMaxLength(50);

                entity.Property(e => e.TicketNumber2).HasMaxLength(50);

                entity.Property(e => e.TicketNumber3).HasMaxLength(50);

                entity.Property(e => e.VisaTradeEntitySetupId).HasColumnName("VisaTrade_EntitySetupId");

                entity.Property(e => e.VisisSerialNumber).HasMaxLength(50);

                entity.HasOne(d => d.CandidateSelectionDetail)
                    .WithMany(p => p.VisaProcess)
                    .HasForeignKey(d => d.CandidateSelectionDetailId)
                    .HasConstraintName("FK_VisaProcessing_CandidateSelectionDetail");

                entity.HasOne(d => d.OepvisaDemand)
                    .WithMany(p => p.VisaProcess)
                    .HasForeignKey(d => d.OepvisaDemandId)
                    .HasConstraintName("FK_VisaProcessing_OEPVisaDemand");

                entity.HasOne(d => d.Sector1FromNavigation)
                    .WithMany(p => p.VisaProcessSector1FromNavigation)
                    .HasForeignKey(d => d.Sector1From)
                    .HasConstraintName("FK_VisaProcess_EntitySetup");

                entity.HasOne(d => d.Sector1ToNavigation)
                    .WithMany(p => p.VisaProcessSector1ToNavigation)
                    .HasForeignKey(d => d.Sector1To)
                    .HasConstraintName("FK_VisaProcess_EntitySetup1");

                entity.HasOne(d => d.Sector2FromNavigation)
                    .WithMany(p => p.VisaProcessSector2FromNavigation)
                    .HasForeignKey(d => d.Sector2From)
                    .HasConstraintName("FK_VisaProcess_EntitySetup2");

                entity.HasOne(d => d.Sector2ToNavigation)
                    .WithMany(p => p.VisaProcessSector2ToNavigation)
                    .HasForeignKey(d => d.Sector2To)
                    .HasConstraintName("FK_VisaProcess_EntitySetup3");

                entity.HasOne(d => d.Sector3FromNavigation)
                    .WithMany(p => p.VisaProcessSector3FromNavigation)
                    .HasForeignKey(d => d.Sector3From)
                    .HasConstraintName("FK_VisaProcess_EntitySetup4");

                entity.HasOne(d => d.Sector3ToNavigation)
                    .WithMany(p => p.VisaProcessSector3ToNavigation)
                    .HasForeignKey(d => d.Sector3To)
                    .HasConstraintName("FK_VisaProcess_EntitySetup5");

                entity.HasOne(d => d.StatusEntitySetup)
                    .WithMany(p => p.VisaProcessStatusEntitySetup)
                    .HasForeignKey(d => d.StatusEntitySetupId)
                    .HasConstraintName("FK_VisaProcessing_EntitySetup1");

                entity.HasOne(d => d.VisaTradeEntitySetup)
                    .WithMany(p => p.VisaProcessVisaTradeEntitySetup)
                    .HasForeignKey(d => d.VisaTradeEntitySetupId)
                    .HasConstraintName("FK_VisaProcessing_EntitySetup");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
