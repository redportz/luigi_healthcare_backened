using System;

namespace HealthcareDBBackend.Models.Requests
{
    public class RegistrationRequest
    {
        // UserInfo fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SSN { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        // For patients, Role will be "Patient". You could also allow other roles if needed.
        public string Role { get; set; } = "Patient";

        // Insurance fields (for PatientDetails)
        public string InsuranceName { get; set; }
        public string MemberID { get; set; }
    }
}
