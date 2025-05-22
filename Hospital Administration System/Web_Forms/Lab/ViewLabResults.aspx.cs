using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Lab
{
    public partial class ViewLabResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (id != null)
            {

                LoadAppointments(id);
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

                    SqlCommand cmd = new SqlCommand("SELECT LabID AS \"LAB ID\", Overview, Results  FROM LabResults WHERE AppID = @appId", conn);
                    cmd.Parameters.AddWithValue("@appId", id);
                    conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind the DataTable to the GridView
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    conn.Close();

                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (id != null)
            {

                Response.Redirect($"/Web_Forms/Lab/EditLabResults.aspx?appid={id}");
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