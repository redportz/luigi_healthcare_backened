using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareDBBackend.Models.Entities;
using System.Text.Json.Serialization;



namespace HealthcareDBBackend.Models.Entities

{
    [Table("Doctors")]
    public class DoctorDetails
    {
        [Key]
        public int DoctorId { get; set; }  // Same as UserId (acts as foreign key)


        [Required]
        public int UserId { get; set; } // Optional if DoctorId already links to User

        public string? Specialty { get; set; }

        public string? LicenseNumber { get; set; }

        public int? YearsOfExperience { get; set; }

        public string? Certifications { get; set; }

        [ForeignKey("UserId")]
        public required UserInfo UserInfo { get; set; }
        
        [JsonIgnore]
        public ICollection<Appointment> Appointments { get; set; }
    }
}
