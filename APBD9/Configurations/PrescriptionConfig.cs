using APBD9.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBD9.Configurations
{
    public class PrescriptionConfig : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("Prescription");
            builder.HasKey(e => e.PrescriptionId).HasName("PrescriptionId_PK");
            builder.Property(e => e.PrescriptionId).UseIdentityColumn();
            builder.Property(e => e.Date).IsRequired();
            builder.Property(e => e.DueDate).IsRequired();

            builder.HasOne(e => e.IDoctorNavigation)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.IdDoctor)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Doctor_Prescription_FK");

            builder.HasOne(e => e.IPatientNavigation)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.IdPatient)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Patient_Prescription_FK");

            var prescriptions = new List<Prescription>();

            prescriptions.Add(new Prescription
            {
                PrescriptionId = 1,
                Date = DateTime.Now,
                DueDate = DateTime.Now.AddDays(90),
                IdPatient = 1,
                IdDoctor = 2
            });

            prescriptions.Add(new Prescription
            {
                PrescriptionId = 2,
                Date = DateTime.Now.AddDays(-5),
                DueDate = DateTime.Now.AddDays(60),
                IdPatient = 2,
                IdDoctor = 1
            });

            builder.HasData(prescriptions);
        }
    }
}
