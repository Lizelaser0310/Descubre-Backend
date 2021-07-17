using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain.Models
{
    public partial class DescubreContext : DbContext
    {
        public DescubreContext()
        {
        }

        public DescubreContext(DbContextOptions<DescubreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alternative> Alternative { get; set; }
        public virtual DbSet<Career> Career { get; set; }
        public virtual DbSet<CareerModality> CareerModality { get; set; }
        public virtual DbSet<Institution> Institution { get; set; }
        public virtual DbSet<InstitutionCareer> InstitutionCareer { get; set; }
        public virtual DbSet<Modality> Modality { get; set; }
        public virtual DbSet<Recomendation> Recomendation { get; set; }
        public virtual DbSet<Response> Response { get; set; }
        public virtual DbSet<Result> Result { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<TestResult> TestResult { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=Descubre;Username=postgres;Password='_fIq}Q#1;'");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum(null, "QuestionType", new[] { "Select", "RadioButton" })
                .HasPostgresEnum(null, "ResultState", new[] { "Finished", "OnProgress", "Cancelled" })
                .HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<Alternative>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_at");

                entity.Property(e => e.Denomination)
                    .IsRequired()
                    .HasMaxLength(1024);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_at");

                entity.HasOne(d => d.Modality)
                    .WithMany(p => p.Alternative)
                    .HasForeignKey(d => d.ModalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Modality_Question");
            });

            modelBuilder.Entity<Career>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_at");

                entity.Property(e => e.Denomination)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.Information).IsRequired();

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_at");
            });

            modelBuilder.Entity<CareerModality>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.HasOne(d => d.Career)
                    .WithMany(p => p.CareerModality)
                    .HasForeignKey(d => d.CareerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Career_CareerModality_FK");

                entity.HasOne(d => d.Modality)
                    .WithMany(p => p.CareerModality)
                    .HasForeignKey(d => d.ModalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Modality_CareerModality_FK");
            });

            modelBuilder.Entity<Institution>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_at");

                entity.Property(e => e.Denomination)
                    .IsRequired()
                    .HasMaxLength(1024);

                entity.Property(e => e.Information).IsRequired();

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_at");
            });

            modelBuilder.Entity<InstitutionCareer>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.HasOne(d => d.Career)
                    .WithMany(p => p.InstitutionCareer)
                    .HasForeignKey(d => d.CareerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Career_InstitutionCareer_FK");

                entity.HasOne(d => d.Institution)
                    .WithMany(p => p.InstitutionCareer)
                    .HasForeignKey(d => d.InstitutionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Institution_InstitutionCareer_FK");
            });

            modelBuilder.Entity<Modality>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_at");

                entity.Property(e => e.Denomination)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_at");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Modality)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Test_Modality");
            });

            modelBuilder.Entity<Recomendation>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.HasOne(d => d.Career)
                    .WithMany(p => p.Recomendation)
                    .HasForeignKey(d => d.CareerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Career_recomendation_Fk");

                entity.HasOne(d => d.Result)
                    .WithMany(p => p.Recomendation)
                    .HasForeignKey(d => d.ResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Result_Recomendation_Fk");
            });

            modelBuilder.Entity<Response>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.HasOne(d => d.Alternative)
                    .WithMany(p => p.Response)
                    .HasForeignKey(d => d.AlternativeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Alternative_Response_FK");

                entity.HasOne(d => d.TestResult)
                    .WithMany(p => p.Response)
                    .HasForeignKey(d => d.TestResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TestResult_Response_FK");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.EndDate).HasColumnType("timestamp with time zone");

                entity.Property(e => e.StartDate).HasColumnType("timestamp with time zone");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Result)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_Result_fk");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();
                
                entity.Property(e => e.Denomination)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_at");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_at");

                entity.Property(e => e.Denomination)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_at");
            });

            modelBuilder.Entity<TestResult>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.HasOne(d => d.Modality)
                    .WithMany(p => p.TestResult)
                    .HasForeignKey(d => d.ModalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Modality_TestResult_FK");

                entity.HasOne(d => d.Result)
                    .WithMany(p => p.TestResult)
                    .HasForeignKey(d => d.ResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Result_TestResult_FK");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TestResult)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Test_TestResult_FK");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Birthday).HasColumnType("timestamp with time zone");

                entity.Property(e => e.Dni)
                    .HasMaxLength(8)
                    .HasColumnName("DNI");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.Phone).HasMaxLength(32);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(512);
                
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Created_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("Updated_at");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Role_User_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
