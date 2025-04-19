using System.ComponentModel.DataAnnotations;

namespace HealthcareDBBackend.Models.Requests
{
    public class LoginRequest
    {
        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
