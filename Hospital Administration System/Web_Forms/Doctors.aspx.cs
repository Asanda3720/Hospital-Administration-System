using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms
{
    public partial class Doctors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllSpecialtyDoctors();
                loadDocs();
            }
        }

        private void LoadAllSpecialtyDoctors()
        {
            // Dictionary mapping specialties to their GridViews
            Dictionary<string, GridView> specialtyGrids = new Dictionary<string, GridView>
            {
                {"Cardiology", GridView1},
                {"Dermatology", GridView2},
                {"Neurology", GridView3},
                {"Pediatrics", GridView4},
                {"Gynecology", GridView5},
                {"Orthopedics", GridView6},
                {"Ophthalmology", GridView7},
                {"Psychiatry", GridView8},
                {"Oncology", GridView9},
                {"Radiology", GridView10}
            };

            string connectionString = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Get all specialties to ensure we have them all
                List<string> specialties = new List<string>();
                using (SqlCommand cmd = new SqlCommand("SELECT Speciality FROM Speciality", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            specialties.Add(reader["Speciality"].ToString());
                        }
                    }
                }

                // For each specialty, load its doctors
                foreach (string specialty in specialties)
                {
                    if (specialtyGrids.ContainsKey(specialty))
                    {
                        DataTable dt = GetDoctorsBySpecialty(conn, specialty);
                        specialtyGrids[specialty].DataSource = dt;
                        specialtyGrids[specialty].DataBind();
                    }
                }
            }
        }

        private DataTable GetDoctorsBySpecialty(SqlConnection conn, string specialty)
        {
            DataTable dt = new DataTable();

            string query = @"SELECT d.Name, d.Surname, d.Gender, d.Room, s.Speciality, s.Description 
                            FROM Doctors d 
                            JOIN Speciality s ON s.SpecID = d.SpecID 
                            WHERE s.Speciality = @Specialty
                            ORDER BY d.Surname, d.Name";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Specialty", specialty);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }
        private void loadDocs()
        {
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("SELECT Name, Surname, Gender FROM Users WHERE UserType_ID = 1", conn);
                    conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind the DataTable to the GridView
                    //GridView1.DataKeyNames = new string[] { "AppID" }; // Set the primary key field
                    GridView11.DataSource = dt;
                    GridView11.DataBind();
                    conn.Close();
                }
            }
        }
    }
}