using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareDBBackend.Models.Entities
{
    public class Prescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key

        [Required]
        public int UserId { get; set; } // Foreign Key

        [ForeignKey("UserId")]
        public UserInfo? User { get; set; } 

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public string Dosage { get; set; }

        [Required]
        public string Frequency { get; set; }

        [Required]
        public int Milligrams { get; set; }

        [Required]
        public int Refills { get; set; }

        [Required]
        [MaxLength(255)]
        public string Doctor { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(255)]
        public string Reason { get; set; } = "No reason provided";
    }
}
