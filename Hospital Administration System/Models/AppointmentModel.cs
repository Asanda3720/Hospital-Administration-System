using System.Data;  // Add this for DataTable

namespace Hospital_Administration_System.Models
{
    public class AppointmentModel
    {
        public int AppID { get; set; }
        public string Category { get; set; }
        public string Speciality { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
        public string Progress { get; set; }
        public bool Rescheduled { get; set; }
        public bool HasOrderedFood { get; set; }
        public bool HasPrescribedMed { get; set; }
        public bool IsAdmitted { get; set; }
    }

    public class AppointmentPdfModel
    {
        public DataTable AppointmentData { get; set; }
        public DataTable LabResults { get; set; }
        public DataTable Prescriptions { get; set; }
        public DataTable XrayResults { get; set; }
        public DataTable PatientInfo { get; set; }
    }

    public class UserCookieModel
    {
        public string Email { get; set; }
        public string UserType { get; set; }
        public int UserId { get; set; }
    }
}