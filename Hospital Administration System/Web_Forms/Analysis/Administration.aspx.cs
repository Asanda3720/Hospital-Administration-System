using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Hospital_Administration_System.Web_Forms.Analysis
{
    public partial class Administration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEmergencies();
                LoadAppointments("Active");
                LoadAppointmentMeals();
                LoadXRAY();
                LoadPrescriptions();
            }
        }
        protected void drdAppStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drdAppStatus.SelectedIndex == 0)
                LoadAppointments("Active");
            else if (drdAppStatus.SelectedIndex == 1)
                LoadRescheduledAppointments();
            else if (drdAppStatus.SelectedIndex == 2)
                LoadAppointments("Cancelled");
            else if (drdAppStatus.SelectedIndex == 3)
                LoadAppointments("Completed");
            else if (drdAppStatus.SelectedIndex == 4)
                LoadAllAppointments();

        }
        private void LoadEmergencies()
        {
            
                //connections
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                SqlConnection cnn;
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {

                    SqlCommand cmd = new SqlCommand("SELECT * FROM EMERGENCY ORDER BY emergencyID Desc", conn);
                    //cmd.Parameters.AddWithValue("@appId", id);
                    conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind the DataTable to the GridView
                    GridView3.DataSource = dt;
                    GridView3.DataBind();
                    conn.Close();

                }
            
        }
        private void LoadAppointments(string status)
        {
            
                //connections
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                SqlConnection cnn;
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {

                    SqlCommand cmd = new SqlCommand("SELECT u.Name, u.Surname,  u.Email, a.Category, a.Speciality,  FORMAT(a.Date, 'dd-MMMM-yyyy') AS 'Date', a.Time, a.Status, a.Progress, a.Notes FROM Appointments a LEFT JOIN Users u ON u.User_ID = a.User_ID WHERE a.Status = @status", conn);
                    cmd.Parameters.AddWithValue("@status", status);
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
        private void LoadRescheduledAppointments()
        {

            //connections
            String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
            SqlConnection cnn;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {

                SqlCommand cmd = new SqlCommand("SELECT u.Name, u.Surname,  u.Email, a.Category, a.Speciality, FORMAT(a.Date, 'dd-MMMM-yyyy') AS 'Date', a.Time, a.Status, a.Progress, a.Notes FROM Appointments a LEFT JOIN Users u ON u.User_ID = a.User_ID WHERE a.Rescheduled = '1'", conn);
               // cmd.Parameters.AddWithValue("@status", '1');
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
        private void LoadAllAppointments()
        {

            //connections
            String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
            SqlConnection cnn;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {

                SqlCommand cmd = new SqlCommand("SELECT u.Name, u.Surname,  u.Email, a.Category, a.Speciality, FORMAT(a.Date, 'dd-MMMM-yyyy') AS 'Date', a.Time, a.Status, a.Progress, a.Notes FROM Appointments a LEFT JOIN Users u ON u.User_ID = a.User_ID", conn);
                // cmd.Parameters.AddWithValue("@status", '1');
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
        private void LoadAppointmentMeals()
        {
            
                //connections
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                SqlConnection cnn;
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {

                    SqlCommand cmd = new SqlCommand("SELECT * FROM AppointmentMeals ORDER BY mealID Desc ", conn);
                    //cmd.Parameters.AddWithValue("@appId", id);
                    conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind the DataTable to the GridView
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                    conn.Close();

                }

            
        }
        private void LoadPrescriptions()
        {
            
                //connections
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                SqlConnection cnn;
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {

                    SqlCommand cmd = new SqlCommand("SELECT * FROM Prescription ORDER BY preID Desc ", conn);
                    //cmd.Parameters.AddWithValue("@appId", id);
                    conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind the DataTable to the GridView
                    GridView5.DataSource = dt;
                    GridView5.DataBind();
                    conn.Close();

                }
            
        }
        private void LoadXRAY()
        {
           
                //connections
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                SqlConnection cnn;
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {

                    SqlCommand cmd = new SqlCommand("SELECT * FROM XRAY ORDER BY xrayID Desc ", conn);
                    //cmd.Parameters.AddWithValue("@appId", id);
                    conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind the DataTable to the GridView
                    GridView4.DataSource = dt;
                    GridView4.DataBind();
                    conn.Close();

                }
            
        }


    }
}