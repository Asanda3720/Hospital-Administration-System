using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.AdmitPatient
{
    public partial class AllAdmittedPatients : System.Web.UI.Page
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
                    u.Name,
                    u.Surname,
                    u.Gender,
                    ap.appID,
                    ap.number_of_days,
                    ap.discharged,
                    ap.Room
                FROM 
                    Admitted_Patients ap
                JOIN 
                    Appointments a ON ap.appID = a.AppID
                JOIN 
                    Users u ON a.User_ID = u.User_ID
                ORDER BY 
                    ap.admitID DESC",
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