using HealthcareDBBackend.Data;
using HealthcareDBBackend.Models.Entities;
using HealthcareDBBackend.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HealthcareDBBackend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserInfoDBContext _context;

        public AuthController(UserInfoDBContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            if (request == null)
                return BadRequest("Request is null");

            var existingUser = await _context.UserInfo.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
                return BadRequest("Email already registered");

            var user = new UserInfo
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                SSN = request.SSN,
                Email = request.Email,
                Password = request.Password,
                Role = request.Role
            };

            await _context.UserInfo.AddAsync(user);
            await _context.SaveChangesAsync();

            if (user.Role.Equals("Patient", StringComparison.OrdinalIgnoreCase))
            {
                var patientDetails = new PatientDetails
                {
                    UserId = user.UserId,
                    InsuranceName = request.InsuranceName,
                    MemberID = request.MemberID,
                    UserInfo = user
                };

                await _context.Patients.AddAsync(patientDetails);
                await _context.SaveChangesAsync();
            }

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.UserInfo.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || user.Password != request.Password)
                return Unauthorized("Invalid email or password");

            return Ok(new { message = "Login successful", userId = user.UserId, role = user.Role });
        }
    }
}
