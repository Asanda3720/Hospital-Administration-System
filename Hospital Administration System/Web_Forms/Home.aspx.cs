using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Hospital_Administration_System.Web_Forms
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {

                string email = userCookieRetriever["email"];
                btnRegister.Visible = false;
                btnBookAppointment.Visible = false;

                if (userCookieRetriever["userType"] == "5")
                {
                    if (HasPendingEmergency())
                    {
                    }
                    else
                    {
                    }
                    
                }
               
                //
                if (userCookieRetriever["userType"] == "1")
                {
                    //btnBookAppointment.Visible = true;
                    btnRegister.Visible = false;
                    btnBookAppointment.Visible = true;
                }
                else
                {
                    btnRegister.Visible = false;
                    btnBookAppointment.Visible = false;
                }
                Session.Remove("VerificationCode");
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {


            Response.Redirect("~/Web_Forms/Register.aspx");
        }

        protected void btnBookAppointment_Click(object sender, EventArgs e)
        {
            //if (IsEmergencyActive())
            //{
            //    // Stop the emergency sound before redirecting
            //    ClientScript.RegisterStartupScript(this.GetType(), "StopSound",
            //        "stopEmergencySound();", true);
            //}

            Response.Redirect("/Web_Forms/DirectAppointmentBookings.aspx");
        }
        //protected bool IsEmergencyActive()
        //{
        //    // Implement your actual emergency check logic here
        //    // This could check a database, session variable, etc.
        //    // For now, we'll just check if userType is 5 (emergency personnel)
        //    HttpCookie userCookie = Request.Cookies["userInfo"];
        //    return (userCookie != null && userCookie["userType"] == "4");
        //}
        bool HasPendingEmergency()
        {
            String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
            SqlConnection cnn;
            string query = "SELECT emergencyID FROM EMERGENCY WHERE Status = 'Pending'";

            cnn = new SqlConnection(connectionstring);
            cnn.Open();
            using (SqlCommand cmd = new SqlCommand(query, cnn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows; // Returns true if at least one record exists
                }
            }
        }

    }

    
}