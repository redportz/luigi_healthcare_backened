using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareDBBackend.Models.Entities;
using System.Text.Json.Serialization;



namespace HealthcareDBBackend.Models.Entities
{
    [Table("PatientDetails")]
    public class PatientDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientId { get; set; }

        [Required]
        [ForeignKey("UserInfo")]
        public int UserId { get; set; }

        public string? InsuranceName { get; set; }

        public string? MemberID { get; set; }

        [Required]
        public required UserInfo UserInfo { get; set; }

        [JsonIgnore]
        public ICollection<Appointment> Appointments { get; set; }
    }
}
