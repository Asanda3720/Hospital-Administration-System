using Hospital_Administration_System.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Hospital_Administration_System.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;

        // Add these missing methods
        private DataTable GetAppointmentData(int appId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT Category, Speciality, Doctor, Date, Time, Status, 
                     Rescheduled, Progress, Notes 
              FROM Appointments 
              WHERE AppID = @AppID", conn);

                cmd.Parameters.AddWithValue("@AppID", appId);
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }

        private DataTable GetLabResults(int appId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT Overview, Results 
              FROM LabResults 
              WHERE AppID = @AppID", conn);

                cmd.Parameters.AddWithValue("@AppID", appId);
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }

        
    }
}