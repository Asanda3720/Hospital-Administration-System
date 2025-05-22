using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.AdmitPatient
{
    public partial class AdmitPatient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdmit_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (id != null)
            {
                admit(id);

            }
        }
        public void admit(string id)
        {
            if ( drdRooms.SelectedIndex == 0 )
            {
                lblErrorRoom.Visible = true;
                return;
            }
            lblErrorRoom.Visible = false;

            int.TryParse(TextBox1.Text, out int num);



            //string id = Request.QueryString["appid"];
            //if (id != null)
            //{
            try
            {
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(connectionstring))
                {
                    cnn.Open();
                    SqlCommand comm = new SqlCommand("INSERT INTO Admitted_Patients(appID, number_of_days, discharged, Room) VALUES (@appid, @number_of_days, @discharged, @room)", cnn);
                    comm.Parameters.AddWithValue("@appid", id);
                    comm.Parameters.AddWithValue("@number_of_days", num);
                    comm.Parameters.AddWithValue("@discharged", "No"); // Fixed parameter name
                    comm.Parameters.AddWithValue("@room", drdRooms.SelectedValue.ToString()); // Fixed parameter name
                    comm.ExecuteNonQuery();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess",
                            $"alert('Patient Successfully Admitted!'); window.location.href = '/Web_Forms/AppointmentDetails/ConsultPatient.aspx?appid={id}';", true);
            }
            catch (Exception ex)
            {
                // Log the error (consider using a logging framework)
                System.Diagnostics.Trace.TraceError($"Error adding Lab Results: {ex.Message}");

                // Show error message to user
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Error adding Lab Results: {ex.Message}');", true);
            }
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert",
            //        "alert('Invalid Url. Rescan QR CODE!'); window.location='/Web_Forms/Login.aspx';", true);
            //}
        }
    }
}