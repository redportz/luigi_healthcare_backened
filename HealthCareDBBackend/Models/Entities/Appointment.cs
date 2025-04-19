using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareDBBackend.Models.Entities
{
    [Table("Appointments")]

        public class Appointment
        {
            [Key]
            public int AppointmentId { get; set; }

            [Required]
            public DateTime AppointmentDate { get; set; }

            public string? Reason { get; set; }

            [Required]
            public int UserId { get; set; }  // <- was PatientId

            [Required]
            public int DoctorId { get; set; }

            [ForeignKey("DoctorId")]
            public DoctorDetails Doctor { get; set; }

            [ForeignKey("UserId")]
            public PatientDetails Patient { get; set; }
        }
}
