using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Hospital_Administration_System.Web_Forms.Meal
{
    public partial class YourMeals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["appID"];
            if (id != null)
            {
                LoadAppointmentMeals(id);
            }
        }
        private void LoadAppointmentMeals(string id)
        {
            
            
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {

                    SqlCommand cmd = new SqlCommand("SELECT Breakfast, Lunch, Dinner FROM AppointmentMeals WHERE AppID = @appId", conn);
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
            string id = Request.QueryString["appID"];
            if (id != null)
            {
                
                Response.Redirect($"/Web_Forms/Meal/EditMeals.aspx?appID={id}");
            }
            
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/Web_Forms/DirectAppointmentBookings.aspx");
        }
    }
    
}