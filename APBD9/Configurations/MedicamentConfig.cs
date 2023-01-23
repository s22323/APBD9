using APBD9.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD9.Configurations
{
    public class MedicamentConfig : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Medicament> builder)
        {
            builder.ToTable("Medicament");
            builder.HasKey(e => e.IdMedicament).HasName("IdMedicament_PK");
            builder.Property(e => e.IdMedicament).UseIdentityColumn();
            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Description).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Type).HasMaxLength(100).IsRequired();

            var medicaments = new List<Medicament>();

            medicaments.Add(new Medicament
            {
                IdMedicament = 1,
                Name = "Glucophage",
                Description = "Lek na cukrzycę",
                Type = "Tabletka"
            });

            medicaments.Add(new Medicament
            {
                IdMedicament = 2,
                Name = "Mucosolvan",
                Description = "Lek na kaszel",
                Type = "Syrop"
            });

        }
    }
}
