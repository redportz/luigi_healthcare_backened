using HealthcareDBBackend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareDBBackend.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly UserInfoDBContext _context;

        public PatientsController(UserInfoDBContext context)
        {
            _context = context;
        }

        [HttpGet("byUserId/{userId}")]
        public async Task<IActionResult> GetPatientByUserId(int userId)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);

            if (patient == null)
                return NotFound("Patient not found for given UserId");

            return Ok(new { patientId = patient.PatientId });
        }
        
    }
}
