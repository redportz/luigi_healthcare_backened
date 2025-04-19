namespace HealthcareDBBackend.Models.Dto
{
    public class AppointmentDto
    {
        public DateTime AppointmentDate { get; set; }
        public string? Reason { get; set; }
        public int UserId { get; set; }
        public int DoctorId { get; set; }
    }
}
