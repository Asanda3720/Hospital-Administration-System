using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Emergency
{
    public partial class EditEmergency : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["appid"];
                if (id != null)
                {
                    loadEmergencyDetails(id);
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if ( drdStatus.SelectedIndex == 0 )
            {
                lblErrorStatus.Visible = true;
                return;
            }
            lblErrorStatus.Visible = false;

            string id = Request.QueryString["appid"];
            try
            {
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                //SqlConnection cnn;
                //SqlDataAdapter adapter;
                //SqlCommand cmd;
                SqlConnection cnn = new SqlConnection(connectionstring);
                cnn.Open();


                SqlCommand cmd = new SqlCommand("UPDATE EMERGENCY SET Status = @status WHERE emergencyID = @id", cnn);
                cmd.Parameters.AddWithValue("@status", "Resolved");
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.UpdateCommand = cmd;
                adapter.UpdateCommand.ExecuteNonQuery();
                cnn.Close();

                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess",
                           "alert('Emergency Successfully Updated!'); window.location.href = '/Web_Forms/Emergency/AllEmergencies.aspx';", true);
                //ClientScript.RegisterStartupScript(this.GetType(), "alert",
                //        $"alert(Emergency Successfully Updated'); window.location='Web_Forms/Emergency/AllEmergencies.aspx';", true);
               
                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Report successfully updated!');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess",
                //    "alert('Report successfully updated!');';", true);

            }
            catch (SqlException error)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
            }
        }
        public void loadEmergencyDetails(string id)
        {
            try
            {
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                //SqlConnection cnn;
                //SqlDataAdapter adapter;
                //SqlCommand cmd;
                SqlConnection cnn = new SqlConnection(connectionstring);
                cnn.Open();


                SqlCommand comm = new SqlCommand("SELECT Name, Surname, Email, Reason, Address FROM EMERGENCY WHERE emergencyID = @id", cnn);
                comm.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    TextBox1.Text = reader.GetValue(0).ToString();
                    TextBox2.Text = reader.GetValue(1).ToString();
                    TextBox3.Text = reader.GetValue(2).ToString();
                    TextBox4.Text = reader.GetValue(3).ToString();
                    TextBox5.Text = reader.GetValue(4).ToString();
                    
                }
                cnn.Close();

            }
            catch (SqlException error)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
            }
        }
    }
}