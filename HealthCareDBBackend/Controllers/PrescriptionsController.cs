using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthcareDBBackend.Data;
using HealthcareDBBackend.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareDBBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly UserInfoDBContext _context;

        public PrescriptionsController(UserInfoDBContext context)
        {
            _context = context;
        }

        // ✅ Get prescriptions for a specific user
        [HttpGet]
        public async Task<IActionResult> GetPrescriptions([FromQuery] int userId)
        {
            var user = await _context.UserInfo
                .Where(u => u.UserId == userId)
                .Select(u => new { u.FirstName, u.LastName })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound("User not found.");

            var prescriptions = await _context.Prescriptions
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return Ok(new 
            { 
                user = new { user.FirstName, user.LastName },
                prescriptions = prescriptions 
            });
        }


        // ✅ Get a single prescription by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Prescription>> GetPrescriptionById(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);

            if (prescription == null)
                return NotFound("Prescription not found.");

            return Ok(prescription);
        }

        // ✅ Add a prescription (Only doctors can do this)
        [HttpPost]
        public async Task<ActionResult<Prescription>> AddPrescription(Prescription prescription, [FromQuery] int userId)
        {
            var user = await _context.UserInfo.FindAsync(userId);
            if (user == null || user.Role != "Doctor")
            {
                return Unauthorized("Only doctors can add prescriptions.");
            }

            var patientExists = await _context.UserInfo.AnyAsync(u => u.UserId == prescription.UserId);
            if (!patientExists)
            {
                return BadRequest("Patient does not exist. Please create a user first.");
            }

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPrescriptionById), new { id = prescription.Id }, prescription);
        }

        // ✅ Update a prescription (Only doctors can do this)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrescription(int id, Prescription updatedPrescription, [FromQuery] int userId)
        {
            var user = await _context.UserInfo.FindAsync(userId);
            if (user == null || user.Role != "Doctor")
            {
                return Unauthorized("Only doctors can update prescriptions.");
            }

            var existingPrescription = await _context.Prescriptions.FindAsync(id);
            if (existingPrescription == null)
            {
                return NotFound("Prescription not found.");
            }

            // Update fields
            existingPrescription.Name = updatedPrescription.Name;
            existingPrescription.Dosage = updatedPrescription.Dosage;
            existingPrescription.Frequency = updatedPrescription.Frequency;
            existingPrescription.Milligrams = updatedPrescription.Milligrams;
            existingPrescription.Refills = updatedPrescription.Refills;
            existingPrescription.Doctor = updatedPrescription.Doctor;
            existingPrescription.Phone = updatedPrescription.Phone;
            existingPrescription.Reason = updatedPrescription.Reason;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //  Delete a prescription (Only doctors can do this)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id, [FromQuery] int userId)
        {
            var user = await _context.UserInfo.FindAsync(userId);
            if (user == null || user.Role != "Doctor")
            {
                return Unauthorized("Only doctors can delete prescriptions.");
            }

            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound("Prescription not found.");
            }

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }  

}  
