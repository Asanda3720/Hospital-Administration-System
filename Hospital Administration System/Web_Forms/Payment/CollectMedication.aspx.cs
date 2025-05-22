using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Payment
{
    public partial class CollectMedication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (id != null)
            {
                LoadAppointments(id);
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (id != null)
            {
                collectMed(id);
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess",
                            "alert('Medication Successfully Collected!'); window.location.href = '/Web_Forms/DirectAppointmentBookings.aspx';", true);
            }
        }
        private void LoadAppointments(string id)
        {
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    conn.Open();
                    // Include preID in your SELECT query
                    SqlCommand cmd = new SqlCommand("SELECT preID, MedicationName, Intake, Times, Description FROM Prescription WHERE AppID = @appID", conn);
                    cmd.Parameters.AddWithValue("@appID", id);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // Rows exist - hide the "not exist" label
                      //  lblNotExist.Visible = false;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                    else
                    {
                        // No rows exist - show the label
                       // lblNotExist.Visible = true;
                        GridView1.DataSource = null; // Clear any previous data
                        GridView1.DataBind();
                    }
                    conn.Close();
                }
            }
            else
            {
                // Cookie doesn't exist - handle accordingly
               // lblNotExist.Visible = true;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        private void collectMed(string id)
        {
            
            try
            {
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                //SqlConnection cnn;
                //SqlDataAdapter adapter;
                //SqlCommand cmd;
                SqlConnection cnn = new SqlConnection(connectionstring);
                cnn.Open();


                SqlCommand cmd = new SqlCommand("UPDATE Prescription SET Collected = 'Yes' WHERE  AppID = @appid", cnn);
                cmd.Parameters.AddWithValue("@appid", id);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.UpdateCommand = cmd;
                adapter.UpdateCommand.ExecuteNonQuery();
                cnn.Close();




                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Report successfully updated!');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess",
                //    "alert('Report successfully updated!');';", true);

            }
            catch (SqlException error)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
            }
        }
    }
}