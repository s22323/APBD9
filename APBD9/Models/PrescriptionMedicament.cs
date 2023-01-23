namespace APBD9.Models
{
    public class PrescriptionMedicament
    {
        public int IdMedicament { get; set; }
        public int IdPrescription { get; set; }
        public int Dose { get; set; }
        public string Details { get; set; }

        public virtual Medicament IMedicamentNavigation { get; set; }
        public virtual Prescription IPrescriptionNavigation { get; set; }

    }
}
