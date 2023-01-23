using APBD9.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBD9.Configurations
{
    public class PrescriptionMedicamentConfig : IEntityTypeConfiguration<PrescriptionMedicament>
    {
        public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
        {
            builder.ToTable("PrescriptionMedicament");
            builder.HasKey(e => new { e.IdPrescription, e.IdMedicament }).HasName("IdPrescriptionIdMedicament_PK");
            builder.Property(e => e.Dose);
            builder.Property(e => e.Details).HasMaxLength(100).IsRequired();

            builder.HasOne(e => e.IMedicamentNavigation)
                .WithMany(e => e.PrescriptionMedicaments)
                .HasForeignKey(e => e.IdMedicament)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Medicament_Prescription_FK");

            builder.HasOne(e => e.IPrescriptionNavigation)
                .WithMany(e => e.PrescriptionMedicaments)
                .HasForeignKey(e => e.IdPrescription)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Prescription_Medicament>FK");

            var prescriptionMedicaments = new List<PrescriptionMedicament>();

            prescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdMedicament = 1,
                IdPrescription = 2,
                Dose = 100,
                Details = "2 tabletki dziennie"
            });

            prescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdMedicament = 2,
                IdPrescription = 1,
                Dose = 200,
                Details = "3 łyżeczki syropu dziennie"
            });

            builder.HasData(prescriptionMedicaments);

        }
    }
}
