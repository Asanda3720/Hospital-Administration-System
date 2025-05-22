using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.XRAY
{
    public partial class AddXRAY : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ( !IsPostBack)
            {
                PopulateDoctors();
                string id = Request.QueryString["appid"];
                if (id != null)
                    LoadFullName(id);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (id != null)
                Response.Redirect($"/Web_Forms/AppointmentDetails/ConsultPatient.aspx?appid={id}");

        }

        protected void btnBook_Click(object sender, EventArgs e)
        {
            if (drdBodyPart.SelectedIndex == 0)
            {
                lblError.Visible = true;
                return;
            }
            lblError.Visible = false;

            
            if ( calAppDate.SelectedDate <= DateTime.Now)
            {
                lblErrorCalendar.Visible = true;
                return;
            }
            lblErrorCalendar.Visible = false;

            string priority;
            if (RadioButton1.Checked)
            {
                priority = RadioButton1.Text;
            }
            else if (RadioButton2.Checked)
            {
                priority = RadioButton2.Text;
            }
            else
            {
                lblErrorPriority.Visible = true;
                return;
            }
            lblErrorPriority.Visible = false;

           if ( drdDoctor.SelectedIndex <= 0)
            {
                lblErrorDoctor.Visible = true;
                return;
            }
           lblErrorDoctor.Visible = false;

            


                string id = Request.QueryString["appid"];
            if (id != null)
            {
                addInvoice(id);
                bookxray(id, priority);

               
            }
        }
        public void bookxray(string id, string priority)
        {
            //string id = Request.QueryString["appid"];
            //if (id != null)
            //{
            try
            {
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(connectionstring))
                {
                    cnn.Open();
                    SqlCommand comm = new SqlCommand("INSERT INTO XRAY(appID, Body_Part, Date, Priority, Doctor, Notes) VALUES (@appid, @part, @date, @priority, @doctor, @notes)", cnn);
                    comm.Parameters.AddWithValue("@appid", id);
                    comm.Parameters.AddWithValue("@part", drdBodyPart.SelectedItem.ToString());
                    comm.Parameters.AddWithValue("@date", calAppDate.SelectedDate);
                    comm.Parameters.AddWithValue("@priority", priority);
                    comm.Parameters.AddWithValue("@doctor", drdDoctor.SelectedValue.ToString());
                    comm.Parameters.AddWithValue("@notes", txtNotes.Text);
                    //comm.Parameters.AddWithValue("@report", TextBox10.Text); // Fixed parameter name
                    comm.ExecuteNonQuery();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess",
                            $"alert('X-Ray Successfully Booked!'); window.location.href = '/Web_Forms/AppointmentDetails/ConsultPatient.aspx?appid={id}';", true);

                //ClientScript.RegisterStartupScript(this.GetType(), "alert",
                //    $"alert('X-RAY Successfully Booked!');", true);
            }
            catch (Exception ex)
            {
                // Log the error (consider using a logging framework)
                System.Diagnostics.Trace.TraceError($"Error adding Lab Results: {ex.Message}");

                // Show error message to user
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Error adding Lab Results: {ex.Message}');", true);
            }
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert",
            //        "alert('Invalid Url. Rescan QR CODE!'); window.location='/Web_Forms/Login.aspx';", true);
            //}
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        public void addInvoice(string id)
        {
            try
            {
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(connectionstring))
                {
                    decimal cost = 650.00m;
                    char settled = 'N';
                    cnn.Open();

                    // Corrected SQL command (parameter order matches VALUES clause)
                    SqlCommand comm = new SqlCommand(
                        "INSERT INTO BILLINGS(appID, Category, Product, Cost, Settled) " +
                        "VALUES (@appid, @category, @product, @cost, @settled)", cnn);

                    // Parameters in correct order
                    comm.Parameters.AddWithValue("@appid", id);
                    comm.Parameters.AddWithValue("@category", "X-RAY");
                    comm.Parameters.AddWithValue("@product", "-");
                    comm.Parameters.AddWithValue("@cost", cost);
                    comm.Parameters.AddWithValue("@settled", settled);

                    int rowsAffected = comm.ExecuteNonQuery();

                    //if (rowsAffected > 0)
                    //{
                    //    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    //        "alert('X-RAY Invoice Successfully Added!');", true);
                    //}
                }
            }
            catch (Exception ex)
            {
                // Log the error
                System.Diagnostics.Trace.TraceError($"Error adding invoice: {ex.Message}");

                // Show error message to user
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    $"alert('Error adding invoice: {ex.Message}');", true);
            }
        }
        private void LoadFullName(string id)
        {
            
                String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        @"SELECT 
            u.Name,
            u.Surname,
            a.appID
            
        FROM
            Appointments a
        JOIN 
            Users u ON a.User_ID = u.User_ID
        WHERE
        a.appID = @appid
       ",
                        conn);
                    cmd.Parameters.AddWithValue("@appid", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        txtFullName.Text = reader.GetString(0) + " " + reader.GetString(1);
                    }
                }
            
        }
        public void PopulateDoctors()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
            string query = "SELECT Name, Surname FROM Users WHERE UserType_ID = 4";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    //clear existing data
                    drdDoctor.Items.Clear();
                    drdDoctor.Items.Add("--Select Doctor--");

                    while (reader.Read())
                    {
                        drdDoctor.Items.Add(reader.GetValue(0).ToString() + " " + reader.GetValue(1).ToString());
                    }
                    reader.Close(); //close reader

                }
                catch (SqlException error)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
                }

            }
        }


    }
}