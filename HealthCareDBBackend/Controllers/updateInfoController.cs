using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HealthcareDBBackend.Data;
using HealthcareDBBackend.Models.Entities;

namespace HealthcareDBBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserContactController : ControllerBase
    {
        private readonly UserInfoDBContext _context;

        public UserContactController(UserInfoDBContext context)
        {
            _context = context;
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateContact(int userId, [FromBody] UserContactInfo data)
        {
            var user = await _context.UserInfo.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return NotFound(new { message = "User not found" });

            user.PhoneNumber = data.PhoneNumber;
            user.Address = data.Address;

            await _context.SaveChangesAsync();

            return Ok(new { message = "User contact info updated" });
        }
    }
}