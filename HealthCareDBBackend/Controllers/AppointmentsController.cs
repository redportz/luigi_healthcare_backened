using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthcareDBBackend.Models.Entities;
using HealthcareDBBackend.Data;
using HealthcareDBBackend.Models.Dto;

namespace HealthcareDBBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly UserInfoDBContext _context;

        public AppointmentsController(UserInfoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByUser(int userId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.UserId == userId)
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();

            return Ok(appointments);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByDoctor(int doctorId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();

            return Ok(appointments);
        }

[HttpGet("All")]
public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsAll()
{
    var appointments = await _context.Appointments
        .Include(a => a.Doctor)
            .ThenInclude(d => d.UserInfo)
        .Include(a => a.Patient)
            .ThenInclude(p => p.UserInfo)
        .ToListAsync();

    return Ok(appointments);
}


        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment([FromBody] AppointmentDto dto)
        {
            var appointment = new Appointment
            {
                AppointmentDate = dto.AppointmentDate,
                Reason = dto.Reason,
                UserId = dto.UserId,
                DoctorId = dto.DoctorId
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppointmentsByUser), new { userId = appointment.UserId }, appointment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentDto dto)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            appointment.AppointmentDate = dto.AppointmentDate;
            appointment.Reason = dto.Reason;
            appointment.UserId = dto.UserId;
            appointment.DoctorId = dto.DoctorId;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
