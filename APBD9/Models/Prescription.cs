namespace APBD9.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }

        public virtual Patient IPatientNavigation { get; set; }
        public virtual Doctor IDoctorNavigation { get; set; }

        public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    }
}