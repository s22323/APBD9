namespace APBD9.DTOs
{
    public class PrescriptionDTO
    {
        public IEnumerable<MedicamentDTO> Medicaments { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public DateTime PatientDateOfBirth { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
    }
}
