using HealthcareDBBackend.Data;
using HealthcareDBBackend.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCareDBBackend.Controllers
{
    [ApiController]
    [EnableCors]
    [Route("/[controller]")]
    public class UserInfoController : Controller
    {
        private readonly UserInfoDBContext userInfoDBContext;

        public UserInfoController(UserInfoDBContext userInfoDBContext)
        {
            this.userInfoDBContext = userInfoDBContext;
        }

        // Fetch messages for a specific user
        [HttpGet]
        [Route("GetMessages/{userId:int}")]
        public async Task<IActionResult> GetMessagesByUserId([FromRoute] int userId)
        {
            var messages = await userInfoDBContext.Messages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .ToListAsync();

            return Ok(messages);
        }

        // Send a new message
        [HttpPost]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] Message message)
        {
            message.SentAt = DateTime.UtcNow;
            await userInfoDBContext.Messages.AddAsync(message);
            await userInfoDBContext.SaveChangesAsync();

            return Ok("Message sent successfully.");
        }

        // Mark a message as read
        [HttpPost]
        [Route("MarkMessageAsRead/{messageId:int}")]
        public async Task<IActionResult> MarkMessageAsRead([FromRoute] int messageId)
        {
            var message = await userInfoDBContext.Messages.FindAsync(messageId);
            if (message == null)
            {
                return NotFound("Message not found.");
            }

            message.ReadAt = DateTime.UtcNow;
            await userInfoDBContext.SaveChangesAsync();

            return Ok("Message marked as read.");
        }

        // Handles CORS preflight requests
        [HttpOptions]
        [Route("AddUserInfo")]
        public IActionResult PreflightRoute()
        {
            return NoContent();
        }






        // Get all users
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllInfos([FromQuery] int currentUserId)
        {
            var currentUser = await userInfoDBContext.UserInfo.FindAsync(currentUserId);
            if (currentUser == null)
                return NotFound("Current user not found.");

            var usersQuery = userInfoDBContext.UserInfo.AsQueryable();

            // Only allow patients to see non-patients
            if (currentUser.Role == "Patient")
            {
                usersQuery = usersQuery.Where(u => u.Role != "Patient");
            }

            var users = await usersQuery
                .Select(u => new {
                    u.UserId,
                    u.FirstName,
                    u.LastName,
                    u.DateOfBirth,
                    u.Email,
                    u.Role,
                    Specialty = u.Role == "Doctor"
                        ? userInfoDBContext.Doctors
                            .Where(d => d.DoctorId == u.UserId)
                            .Select(d => d.Specialty)
                            .FirstOrDefault()
                        : null
                })
                .ToListAsync();

            return Ok(users);
        }

        // Get a user by ID
        [HttpGet]
        [Route("GetById/{id:int}")]
        [ActionName("GetUserByID")]
        public async Task<IActionResult> GetUserByID([FromRoute] int id)
        {
            var user = await userInfoDBContext.UserInfo
                .Where(u => u.UserId == id)
                .Select(u => new
                {
                    u.UserId,
                    u.FirstName,
                    u.LastName,
                    u.Email,
                    u.DateOfBirth,
                    u.PhoneNumber,
                    u.Address,
                    u.Role,
                    DoctorDetails = u.Role == "Doctor"
                        ? userInfoDBContext.Doctors
                            .Where(d => d.UserId == u.UserId)
                            .Select(d => new
                            {
                                d.Specialty,
                                d.LicenseNumber,
                                d.Certifications,
                                d.YearsOfExperience
                            })
                            .FirstOrDefault()
                        : null

                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        // Add a new user
        [HttpPost]
        [Route("AddUserInfo")]
        public async Task<IActionResult> AddUserInfo(UserInfo userInfo)
        {

            userInfo.UserId = 0;
            await userInfoDBContext.UserInfo.AddAsync(userInfo);
            await userInfoDBContext.SaveChangesAsync();

            Response.Headers.Append("Access-Control-Allow-Origin", "*");

            return Ok("Created new user");
        }

        // Update user info
        [HttpPut]
        [Route("UpdateUserInfo/{id:int}")]
        public async Task<IActionResult> UpdateUserInfo([FromRoute] int id, [FromBody] UserInfo updateUserInfo)
        {
            var existingNote = await userInfoDBContext.UserInfo.FindAsync(id);
            if (existingNote == null)
            {
                return NotFound();
            }

            existingNote.FirstName = updateUserInfo.FirstName;
            existingNote.LastName = updateUserInfo.LastName;
            //Set other rows here
            existingNote.Address = updateUserInfo.Address;
            existingNote.PhoneNumber = updateUserInfo.PhoneNumber;

            await userInfoDBContext.SaveChangesAsync();

            return Ok(existingNote);
        }
    }
}
