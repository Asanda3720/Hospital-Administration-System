using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.AppointmentDetails
{
    public partial class PrescribeMedication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (id != null)
            {
                LoadAppointments(id);
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            string appID = Request.QueryString["appid"];
            Response.Redirect($"/Web_Forms/AppointmentDetails/ConsultPatient.aspx?appid={appID}");
        }

        protected void btnAddMed_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtIntake.Text, out int intake) || intake < 1 || intake > 10)
            {
                lblErrorPills.Visible = true;
                return;
            }
            //int.TryParse(txtIntake.Text, out int intake);
            //if ( intake < 0 || intake > 10)
            //{
            //    lblErrorPills.Visible = true;
            //    return;
            //}
            lblErrorPills.Visible = false;

            string id = Request.QueryString["appid"];
            if (id != null)
            {
                try
                {
                    String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                    SqlConnection cnn = new SqlConnection(connectionstring);
                    cnn.Open(); //open connection

                    SqlCommand comm = new SqlCommand($"INSERT INTO Prescription(appID, MedicationName, Intake, Times, Description, Collected) VALUES (@appID, @med, @intake, @times, @desc, @collected)", cnn);
                    comm.Parameters.AddWithValue("@appID", id);
                    comm.Parameters.AddWithValue("@med", drdMed.SelectedValue.ToString());
                    comm.Parameters.AddWithValue("@intake", intake);
                    comm.Parameters.AddWithValue("@times", drdTimes.SelectedValue.ToString());
                    comm.Parameters.AddWithValue("@desc", txtDesc.Text);
                    comm.Parameters.AddWithValue("@collected", "No");
                    comm.ExecuteNonQuery();
                    cnn.Close(); //close connection


                    LoadAppointments(id);
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            $"alert('Prescription Successfully Added!');", true);
                }
                catch (SqlException error)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
                }
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
                    SqlCommand cmd = new SqlCommand("SELECT preID, MedicationName, Intake, Times, Description, Collected FROM Prescription WHERE AppID = @appID", conn);
                    cmd.Parameters.AddWithValue("@appID", id);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // Rows exist - hide the "not exist" label
                        lblNotExist.Visible = false;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                    else
                    {
                        // No rows exist - show the label
                        lblNotExist.Visible = true;
                        GridView1.DataSource = null; // Clear any previous data
                        GridView1.DataBind();
                    }
                    conn.Close();
                }
            }
            else
            {
                // Cookie doesn't exist - handle accordingly
                lblNotExist.Visible = true;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                // Get the preID of the record to delete
                int preID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Prescription WHERE preID = @preID", conn);
                    cmd.Parameters.AddWithValue("@preID", preID);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                // Refresh the GridView after deletion
                string appID = Request.QueryString["AppID"]; // Or however you get the AppID
                LoadAppointments(appID);
            }
            catch (Exception ex)
            {
                // Handle error (you might want to show a message to the user)
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error deleting prescription: {ex.Message}');", true);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = e.Row.FindControl("LinkButton1") as LinkButton; // Default ID for delete button
                if (lb != null && lb.Text == "Delete")
                {
                    lb.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this prescription?');");
                }
            }
        }
    }
}