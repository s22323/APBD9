using APBD9.DTOs;
using APBD9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace APBD9.Services
{
    public class ClinicDbService : IClinicDbService
    {
        private readonly Context context;

        public ClinicDbService(Context context)
        {
            this.context = context;
        }

        public async Task AddDoctorAsync(DoctorDTO doctorDTO)
        {
            await context.AddAsync(new Doctor
            {
                FirstName = doctorDTO.FirstName,
                LastName = doctorDTO.LastName,
                Email = doctorDTO.Email
            });
            await context.SaveChangesAsync();
        }

        public async Task ChangeDoctorAsync(int id, DoctorDTO doctorDTO)
        {
            Doctor doctor = await context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                throw new Exception("Doctor doesn't exist");
            }
            doctor.FirstName = doctorDTO.FirstName;
            doctor.LastName = doctorDTO.LastName;
            doctor.Email = doctorDTO.Email;
            await context.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(int id)
        {
            Doctor doctor = await context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                throw new Exception("Doctor doesn't exist");
            }

            bool hasPrescriptions = await context.Prescriptions.AnyAsync(e => e.IdDoctor == id);
            if (hasPrescriptions)
            {
                throw new Exception("You can't delete this doctor because he has prescriptions");
            }

            context.Remove(doctor);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DoctorDTO>> GetDoctorsAsync()
        {
            var doctors = await context.Doctors.Select(e => new DoctorDTO
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email
            }).ToListAsync();
            return doctors;
        }

        public async Task<PrescriptionDTO> GetPrescription(int id)
        {
            Prescription prescription = await context.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                throw new Exception("There is no prescription with such id");
            }

            PrescriptionDTO prescriptionDTO = await context.Prescriptions.Where(e => e.PrescriptionId == id)
                .Select(e => new PrescriptionDTO
                {
                    Medicaments = e.PrescriptionMedicaments.Select(e => new MedicamentDTO
                    {
                        Name = e.IMedicamentNavigation.Name,
                        Dose = e.Dose
                    }),
                    Date = e.Date,
                    DueDate = e.DueDate,
                    PatientFirstName = e.IPatientNavigation.FirstName,
                    PatientLastName = e.IPatientNavigation.LastName,
                    PatientDateOfBirth = e.IPatientNavigation.BirthDate,
                    DoctorFirstName = e.IDoctorNavigation.FirstName,
                    DoctorLastName = e.IDoctorNavigation.LastName
                }).FirstAsync();

            return prescriptionDTO;
        }
    }
}
