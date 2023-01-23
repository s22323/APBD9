using APBD9.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBD9.Configurations
{
    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patient");
            builder.HasKey(e => e.IdPatient).HasName("Patient_PK");
            builder.Property(e => e.IdPatient).UseIdentityColumn();
            builder.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            builder.Property(e => e.BirthDate).IsRequired();

            var patients = new List<Patient>();

            patients.Add(new Patient
            {
                IdPatient = 1,
                FirstName = "Bella",
                LastName = "Ćwir",
                BirthDate = DateTime.Now.AddYears(-30)
            });

            patients.Add(new Patient
            {
                IdPatient = 2,
                FirstName = "Jan",
                LastName = "Nowak",
                BirthDate = DateTime.Now.AddYears(-58)
            });

            builder.HasData(patients);
        }
    }
}
