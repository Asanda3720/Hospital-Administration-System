using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Pharmacist
{
    public partial class PrescribedMed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadAppointments();
        }
        private void LoadAppointments()
        {
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand(
                        @"SELECT
    appID ,
    MedicationName,
    Intake,
    Times,
    Description,
    Collected
    
FROM Prescription
                ORDER BY 
                    preID DESC",
                        conn);

                    conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind the DataTable to the GridView
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }
    }
}