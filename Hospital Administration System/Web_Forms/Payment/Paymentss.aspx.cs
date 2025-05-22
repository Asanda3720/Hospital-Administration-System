using Hospital_Administration_System.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace Hospital_Administration_System.Web_Forms.Payment
{
    public partial class Paymentss : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load any initial data
            }
        }

        [WebMethod]
        public static async Task<PaymentResult> ProcessPayment(string paymentMethodId, decimal amount)
        {
            try
            {
                var service = new PaymentService();
                return await service.ProcessPaymentAsync(paymentMethodId, amount, "Medical services payment");
            }
            catch (Exception ex)
            {
                return new PaymentResult
                {
                    Success = false,
                    Message = "An unexpected error occurred"
                };
            }
        }
        [WebMethod]
        public static AppointmentResult CreateAppointment(string userEmail)
        {
            try
            {
                var httpContext = HttpContext.Current;

                if (string.IsNullOrEmpty(userEmail))
                {
                    return new AppointmentResult
                    {
                        Success = false,
                        Message = "User email not provided."
                    };
                }

                // Get appointment info from cookies
                var appCookie = httpContext.Request.Cookies["appInfo"];
                if (appCookie == null)
                {
                    return new AppointmentResult
                    {
                        Success = false,
                        Message = "Appointment information missing."
                    };
                }

                // Look up user ID from database based on email
                string connectionString = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                string userId;

                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    cnn.Open();

                    // Get user ID
                    SqlCommand userCmd = new SqlCommand(
                        "SELECT User_ID FROM Users WHERE Email = @email", cnn);
                    userCmd.Parameters.AddWithValue("@email", userEmail);

                    var result = userCmd.ExecuteScalar();
                    if (result == null)
                    {
                        return new AppointmentResult
                        {
                            Success = false,
                            Message = "User not found in database."
                        };
                    }

                    userId = result.ToString();

                    // Create appointment
                    DateTime.TryParse(appCookie["date"], out DateTime appDate);

                    SqlCommand comm = new SqlCommand(
                        @"INSERT INTO Appointments(Category, Speciality, Doctor, Date, Time, User_ID, Status, Rescheduled) 
                  VALUES (@category, @speciality, @doctor, @date, @time, @userid, @status, @rescheduled);
                  SELECT SCOPE_IDENTITY();", cnn);

                    comm.Parameters.AddWithValue("@category", appCookie["category"]);
                    comm.Parameters.AddWithValue("@speciality", appCookie["speciality"]);
                    comm.Parameters.AddWithValue("@doctor", "None");
                    comm.Parameters.AddWithValue("@date", appDate);
                    comm.Parameters.AddWithValue("@time", appCookie["time"]);
                    comm.Parameters.AddWithValue("@userid", userId);
                    comm.Parameters.AddWithValue("@status", "Active");
                    comm.Parameters.AddWithValue("@rescheduled", 0);

                    var appointmentId = comm.ExecuteScalar();

                    return new AppointmentResult
                    {
                        Success = true,
                        UserId = userId,
                        AppointmentId = appointmentId.ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the error
                return new AppointmentResult
                {
                    Success = false,
                    Message = "An unexpected error occurred: " + ex.Message
                };
            }
        }

        public class AppointmentResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string UserId { get; set; }
            public string AppointmentId { get; set; }
        }

        // Remove the old btnSubmit_Click handler since we're using AJAX now

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //addAppointment();
            //string id = "6";
            //if (id != null)
            //{
            //    //ClientScript.RegisterStartupScript(this.GetType(), "alert",
            //    //    $"alert('Appointment Successfully Booked!'); window.location='/Web_Forms/Payment/Receipts.aspx?id={id}';", true);
            //}
        }
        public void addAppointment()
        {


            String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
            SqlConnection cnn;
            HttpCookie appCookie = Request.Cookies["appInfo"];
            if (appCookie != null)
            {
                DateTime.TryParse(appCookie["date"], out DateTime appDate);
                string id = Request.QueryString["id"];
                if (id != null)
                {
                    cnn = new SqlConnection(connectionstring);
                    cnn.Open(); //open connection

                    SqlCommand comm = new SqlCommand($"INSERT INTO Appointments(Category, Speciality, Doctor, Date, Time, User_ID, Status, Rescheduled) VALUES (@category, @speciality, @doctor, @date, @time, @userid, @status, @rescheduled)", cnn);
                    comm.Parameters.AddWithValue("@category", appCookie["category"]);
                    comm.Parameters.AddWithValue("@speciality", appCookie["speciality"]);
                    comm.Parameters.AddWithValue("@doctor", "None");


                    comm.Parameters.AddWithValue("@date", appDate);
                    comm.Parameters.AddWithValue("@time", appCookie["time"]);
                    comm.Parameters.AddWithValue("@userid", id);
                    comm.Parameters.AddWithValue("@status", "Active");
                    comm.Parameters.AddWithValue("@rescheduled", 0);
                    comm.ExecuteNonQuery();
                    cnn.Close(); //close connection

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            $"alert('Appointment Successfully Booked!'); window.location='/Web_Forms/Payment/Receipts.aspx?id={id}';", true);
                }
                else
                {
                    Response.Redirect("~/Web_Forms/Login.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Web_Forms/Login.aspx");
            }
        }
    }
}