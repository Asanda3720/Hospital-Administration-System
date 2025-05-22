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
    public partial class EditLabResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["appid"];
                if (id != null)
                {
                    loadAppDetails(id);
                }
            }
        }
        public void loadAppDetails(string id)
        {
            try
            {
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                //SqlConnection cnn;
                //SqlDataAdapter adapter;
                //SqlCommand cmd;
                SqlConnection cnn = new SqlConnection(connectionstring);
                cnn.Open();


                SqlCommand comm = new SqlCommand("SELECT Overview, Results  FROM LabResults WHERE AppID = @appID", cnn);
                comm.Parameters.AddWithValue("@appID", id);

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    TextBox9.Text = reader.GetValue(0).ToString();
                    TextBox10.Text = reader.GetValue(1).ToString();

                }
                cnn.Close();

            }
            catch (SqlException error)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
            }
        }

        protected void btnUpdateReport_Click(object sender, EventArgs e)
        {

            string id = Request.QueryString["appid"];
            try
            {
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                //SqlConnection cnn;
                //SqlDataAdapter adapter;
                //SqlCommand cmd;
                SqlConnection cnn = new SqlConnection(connectionstring);
                cnn.Open();


                SqlCommand cmd = new SqlCommand("UPDATE LabResults SET Overview = @overview, Results = @results WHERE  AppID = @appid", cnn);
                cmd.Parameters.AddWithValue("@overview", TextBox9.Text);
                cmd.Parameters.AddWithValue("@results", TextBox10.Text);
                cmd.Parameters.AddWithValue("@appid", id);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.UpdateCommand = cmd;
                adapter.UpdateCommand.ExecuteNonQuery();
                cnn.Close();




                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Report successfully updated!');", true);
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
        $"alert('Report successfully updated!'); window.location='/Web_Forms/Lab/LabTech.aspx';", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess",
                //    "alert('Report successfully updated!');';", true);

            }
            catch (SqlException error)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (id != null)
            {

                Response.Redirect($"/Web_Forms/Lab/LabTech.aspx");
            }
        }
    }
}