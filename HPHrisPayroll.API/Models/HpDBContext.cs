using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HPHrisPayroll.API.Models
{
    public partial class HpDBContext : DbContext
    {
        public HpDBContext()
        {
        }

        public HpDBContext(DbContextOptions<HpDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditLogs> AuditLogs { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<EmailAddresses> EmailAddresses { get; set; }
        public virtual DbSet<EmailTypes> EmailTypes { get; set; }
        public virtual DbSet<EmergencyContacts> EmergencyContacts { get; set; }
        public virtual DbSet<EmployeeAddresses> EmployeeAddresses { get; set; }
        public virtual DbSet<EmployeeEducation> EmployeeEducation { get; set; }
        public virtual DbSet<EmployeeNoConfig> EmployeeNoConfig { get; set; }
        public virtual DbSet<EmployeeStatus> EmployeeStatus { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<EmploymentHistory> EmploymentHistory { get; set; }
        public virtual DbSet<JobLevels> JobLevels { get; set; }
        public virtual DbSet<PayrollModes> PayrollModes { get; set; }
        public virtual DbSet<PhoneNumbers> PhoneNumbers { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<TaxStatus> TaxStatus { get; set; }
        public virtual DbSet<UserGroupAccess> UserGroupAccess { get; set; }
        public virtual DbSet<UserGroups> UserGroups { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<AuditLogs>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.Activty)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ControllerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateAccessed).HasColumnType("datetime");

                entity.Property(e => e.Ipaddress)
                    .IsRequired()
                    .HasColumnName("IPAddress")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Method)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.HasKey(e => e.CompanyCode);

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyUid)
                    .HasColumnName("CompanyUID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Departments>(entity =>
            {
                entity.HasKey(e => e.DeptCode);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CompanyCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CompanyCodeNavigation)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Departments_Companies");
            });

            modelBuilder.Entity<EmailAddresses>(entity =>
            {
                entity.HasKey(e => e.EmailAddId);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailAddress).HasMaxLength(10);

                entity.Property(e => e.EmployeeNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TypeCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmployeeNoNavigation)
                    .WithMany(p => p.EmailAddresses)
                    .HasForeignKey(d => d.EmployeeNo)
                    .HasConstraintName("FK_EmailAddresses_Employees");

                entity.HasOne(d => d.TypeCodeNavigation)
                    .WithMany(p => p.EmailAddresses)
                    .HasForeignKey(d => d.TypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmailAddresses_EmailTypes");
            });

            modelBuilder.Entity<EmailTypes>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmergencyContacts>(entity =>
            {
                entity.HasKey(e => e.EmergencyContactId);

                entity.Property(e => e.EmergencyContactId).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmployeeNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RelationshipToEmployee)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmployeeNoNavigation)
                    .WithMany(p => p.EmergencyContacts)
                    .HasForeignKey(d => d.EmployeeNo)
                    .HasConstraintName("FK_EmergencyContacts_Employees");
            });

            modelBuilder.Entity<EmployeeAddresses>(entity =>
            {
                entity.HasKey(e => e.EmployeeAddressId);

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmployeeNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Postal)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmployeeNoNavigation)
                    .WithMany(p => p.EmployeeAddresses)
                    .HasForeignKey(d => d.EmployeeNo)
                    .HasConstraintName("FK_EmployeeAddresses_Employees");
            });

            modelBuilder.Entity<EmployeeEducation>(entity =>
            {
                entity.HasKey(e => e.EducationRefId);

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Course)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateGraduated).HasColumnType("datetime");

                entity.Property(e => e.EmployeeNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Qualification)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.School)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmployeeNoNavigation)
                    .WithMany(p => p.EmployeeEducation)
                    .HasForeignKey(d => d.EmployeeNo)
                    .HasConstraintName("FK_EmployeeEducation_Employees");
            });

            modelBuilder.Entity<EmployeeNoConfig>(entity =>
            {
                entity.Property(e => e.CompanyCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateLastUpdated).HasColumnType("datetime");

                entity.Property(e => e.Prefix)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CompanyCodeNavigation)
                    .WithMany(p => p.EmployeeNoConfig)
                    .HasForeignKey(d => d.CompanyCode)
                    .HasConstraintName("FK_EmployeeNoConfig_Companies");
            });

            modelBuilder.Entity<EmployeeStatus>(entity =>
            {
                entity.HasKey(e => e.EmployeeStatusCode);

                entity.Property(e => e.EmployeeStatusCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CompanyCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeNo);

                entity.Property(e => e.EmployeeNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.BirthCountry)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.CompanyCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentAddress)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeptCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpUid)
                    .HasColumnName("EmpUID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.EmployeeStatusCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Hdmfno)
                    .HasColumnName("HDMFNo")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PayrollMode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PhilHealthNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Sssno)
                    .HasColumnName("SSSNo")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TaxStatusCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tinno)
                    .HasColumnName("TINNo")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.CompanyCodeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CompanyCode)
                    .HasConstraintName("FK_Employees_Companies");

                entity.HasOne(d => d.DeptCodeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DeptCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Departments");

                entity.HasOne(d => d.EmployeeNoNavigation)
                    .WithOne(p => p.Employees)
                    .HasForeignKey<Employees>(d => d.EmployeeNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_PayrollModes");

                entity.HasOne(d => d.EmployeeStatusCodeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_EmployeeStatus");

                entity.HasOne(d => d.PositionNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Position)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Positions");

                entity.HasOne(d => d.TaxStatusCodeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.TaxStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_TaxStatus");
            });

            modelBuilder.Entity<EmploymentHistory>(entity =>
            {
                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateStarted).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.EmployeeNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmployeeNoNavigation)
                    .WithMany(p => p.EmploymentHistory)
                    .HasForeignKey(d => d.EmployeeNo)
                    .HasConstraintName("FK_EmploymentHistory_Employees");
            });

            modelBuilder.Entity<JobLevels>(entity =>
            {
                entity.HasKey(e => e.JobLevelCode);

                entity.Property(e => e.JobLevelCode)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CompanyCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateTimeUpdated).HasColumnType("datetime");

                entity.Property(e => e.JobLevelDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CompanyCodeNavigation)
                    .WithMany(p => p.JobLevels)
                    .HasForeignKey(d => d.CompanyCode)
                    .HasConstraintName("FK_JobLevels_Companies");
            });

            modelBuilder.Entity<PayrollModes>(entity =>
            {
                entity.HasKey(e => e.PayrollMode);

                entity.Property(e => e.PayrollMode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CompanyCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CompanyCodeNavigation)
                    .WithMany(p => p.PayrollModes)
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PayrollModes_Companies");
            });

            modelBuilder.Entity<PhoneNumbers>(entity =>
            {
                entity.HasKey(e => e.PhoneId);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmployeeNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmployeeNoNavigation)
                    .WithMany(p => p.PhoneNumbers)
                    .HasForeignKey(d => d.EmployeeNo)
                    .HasConstraintName("FK_PhoneNumbers_Employees");
            });

            modelBuilder.Entity<Positions>(entity =>
            {
                entity.HasKey(e => e.Position);

                entity.Property(e => e.Position)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CompanyCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateTimeUpdated).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.JobLevelCode)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.JobLevelCodeNavigation)
                    .WithMany(p => p.Positions)
                    .HasForeignKey(d => d.JobLevelCode)
                    .HasConstraintName("FK_Positions_JobLevels");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TaxStatus>(entity =>
            {
                entity.HasKey(e => e.TaxStatusCode);

                entity.Property(e => e.TaxStatusCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TaxStatusDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserGroupAccess>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserGroupAccess)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserGroupAccess_Roles");

                entity.HasOne(d => d.UserGroup)
                    .WithMany(p => p.UserGroupAccess)
                    .HasForeignKey(d => d.UserGroupId)
                    .HasConstraintName("FK_UserGroupAccess_UserGroups");
            });

            modelBuilder.Entity<UserGroups>(entity =>
            {
                entity.HasKey(e => e.UserGroupId);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserGroupName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmployeeNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastActive).HasColumnType("datetime");

                entity.Property(e => e.PasswordExpiration).HasColumnType("datetime");

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.PasswordSalt).IsRequired();

                entity.Property(e => e.Syek)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserUid)
                    .HasColumnName("UserUID")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.EmployeeNoNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.EmployeeNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Users");

                entity.HasOne(d => d.UserGroup)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserGroupId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Users_UserGroups");
            });
        }
    }
}
