using APBD9.DTOs;
using APBD9.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD9.Controllers
{
    [Route("api/clinic")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicDbService clinicDbService;

        public ClinicController(IClinicDbService clinicDbService)
        {
            this.clinicDbService = clinicDbService;
        }

        [HttpGet("doctors")]
        public async Task<IActionResult> GetDoctors()
        {
            var result = await clinicDbService.GetDoctorsAsync();

            return Ok(result);
        }

        [HttpPost("doctors")]
        public async Task<IActionResult> AddDoctor([FromBody] DoctorDTO doctor)
        {
            return Ok(clinicDbService.AddDoctorAsync(doctor));
        }

        [HttpPut("doctors/{id}")]
        public async Task<IActionResult> ChangeDoctor([FromRoute] int id, [FromBody] DoctorDTO doctor)
        {
            return Ok(clinicDbService.ChangeDoctorAsync(id, doctor));
        }

        [HttpDelete("doctors/{id}")]
        public async Task<IActionResult> DeleteDoctor([FromRoute] int id)
        {
            return Ok(clinicDbService.DeleteDoctorAsync(id));
        }

        [HttpGet("prescriptions/{id}")]
        public async Task<IActionResult> GetPrescription([FromRoute] int id)
        {
            return Ok(clinicDbService.GetPrescription(id));
        }

    }
}
