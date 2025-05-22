using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.XRAY
{
    public partial class AllXRAYS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadAppointments();
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Get the AppID from the row being edited
            GridViewRow row = GridView1.Rows[e.NewEditIndex];
            string id = GridView1.DataKeys[row.RowIndex].Value.ToString();

            // Redirect to the edit page
            Response.Redirect($"/Web_Forms/XRAY/EditXRayReport.aspx?appid={id}");
        }
        private void LoadAppointments()
        {
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("SELECT xrayID, appID, Overview, Report, Body_Part FROM XRAY", conn);
                    conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind the DataTable to the GridView
                    GridView1.DataKeyNames = new string[] { "AppID" }; // Set the primary key field
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    conn.Close();
                }
            }
        }
    }
}