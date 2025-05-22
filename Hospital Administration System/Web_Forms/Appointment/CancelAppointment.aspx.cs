using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace Hospital_Administration_System.Web_Forms.Appointment
{
    public partial class CancelAppointment : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // You can load any appointment details here if needed
                // For example:
                // string appointmentId = Request.QueryString["id"];
                // LoadAppointmentDetails(appointmentId);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            // Redirect back to the previous page or appointments list
            Response.Redirect("/Web_Forms/DirectAppointmentBookings.aspx");
        }

        protected void btnCancelAppointment_Click(object sender, EventArgs e)
        {
           
                string id = Request.QueryString["id"];
                try
                {
                    String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                    //SqlConnection cnn;
                    //SqlDataAdapter adapter;
                    //SqlCommand cmd;
                    SqlConnection cnn = new SqlConnection(connectionstring);
                    cnn.Open();


                    SqlCommand cmd = new SqlCommand("UPDATE Appointments SET Status = 'Cancelled' WHERE  AppID = @appid", cnn);
                    cmd.Parameters.AddWithValue("@appid", id);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = cmd;
                    adapter.UpdateCommand.ExecuteNonQuery();
                    cnn.Close();
                    //addDriverAcceptedOrder(numOrder);




                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess",
                    "alert('Appointment cancelled successfully!'); window.location.href = '/Web_Forms/DirectAppointmentBookings.aspx';", true);

                }
                catch (SqlException error)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
                }


                
            
        }
    }
}