using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Lab
{
    public partial class AddLabResults : System.Web.UI.Page
    {
        protected void btnUpdateReport_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (id != null)
            {
                try
                {
                    String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                    using (SqlConnection cnn = new SqlConnection(connectionstring))
                    {
                        cnn.Open();
                        SqlCommand comm = new SqlCommand("INSERT INTO LabResults(appID, Overview, Results) VALUES (@appid, @overview, @report)", cnn);
                        comm.Parameters.AddWithValue("@appid", id);
                        comm.Parameters.AddWithValue("@overview", TextBox9.Text);
                        comm.Parameters.AddWithValue("@report", TextBox10.Text); // Fixed parameter name
                        comm.ExecuteNonQuery();
                    }

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        $"alert('Lab Results Successfully Added! You will be redirected.'); window.location='/Web_Forms/Lab/ViewLabResults.aspx?appid={id}';", true);
                }
                catch (Exception ex)
                {
                    // Log the error (consider using a logging framework)
                    System.Diagnostics.Trace.TraceError($"Error adding Lab Results: {ex.Message}");

                    // Show error message to user
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        $"alert('Error adding Lab Results: {ex.Message}');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Invalid Url. Rescan QR CODE!'); window.location='/Web_Forms/Login.aspx';", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (id != null)
            {
                Response.Redirect($"/Web_Forms/AppointmentDetails/ConsultPatient.aspx?appid={id}");
            }
        }
    }
}