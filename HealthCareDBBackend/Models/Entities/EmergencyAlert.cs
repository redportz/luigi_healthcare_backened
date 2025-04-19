using System;
using System.ComponentModel.DataAnnotations;

namespace EmergencyAlertAPI.Models
{
    public class EmergencyAlert
    {
        public int Id { get; set; }

        [Required]
        public string PatientName { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        public string EmergencyType { get; set; } = "General";
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending";
    }
}
