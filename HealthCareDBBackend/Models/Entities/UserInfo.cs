using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareDBBackend.Models.Entities
{
    public class UserInfo
    {
        [Key]
        public int UserId { get; set; }  // Primary Key

        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }

        [Required]
        public required DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(11)]
        public required string SSN { get; set; } 

        [Required]
        [MaxLength(255)]
        public required string Email { get; set; }  
        
        public string? Address { get; set; } 
        public string? PhoneNumber { get; set; } 


        [Required]
        public required string Password { get; set; } 

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = "Patient";  

        public ICollection<Prescription>? Prescriptions { get; set; }
    }
}
