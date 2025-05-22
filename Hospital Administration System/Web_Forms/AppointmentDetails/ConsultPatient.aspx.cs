using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.AppointmentDetails
{
    public partial class ConsultPatient : System.Web.UI.Page
    {
        String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
        SqlConnection cnn;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
                if (userCookieRetriever != null && userCookieRetriever["userType"] == "2")
                {
                    string id = Request.QueryString["appid"];
                    if (id != null)
                    {
                        if (Request.Cookies["redirectInfo"] != null)
                        {
                            HttpCookie myCookie = new HttpCookie("redirectInfo");
                            myCookie.Expires = DateTime.Now.AddDays(-1); // Set to expire in the past
                            Response.Cookies.Add(myCookie);
                            //Response.Redirect("/Web_Forms/Home.aspx");
                        }

                        loadAppDetails(id);
                        if (HasBookedXRAY(id))
                        {
                            btnXRay.Text = "View X-RAY Report";
                            btnXRay.PostBackUrl = $"/Web_Forms/XRAY/ViewXRayReport.aspx?appid={id}";
                        }
                        else
                        {
                            //bookxray(id);
                            //btnXRay.PostBackUrl = $"/Web_Forms/AppointmentDetails/Bookxray.aspx?appid={id}";
                        }
                        if (HasBookedLab(id))
                        {
                            btnLab.Text = "View LAB Tests";
                            btnLab.PostBackUrl = $"/Web_Forms/Lab/ViewLabResults.aspx?appid={id}";
                        }
                        else
                        {
                            //bookLab(id);
                            //btnLab.PostBackUrl = $"/Web_Forms/Lab/AddLabResults.aspx?appid={id}";
                        }
                        if ( HasAdmittedPatient(id) )
                        {
                        

                        btnAdmit.Visible = false;
                            lblAdmitted.Visible = true;

                           
                        }
                        else
                        {
                            btnAdmit.Visible = true;
                            lblAdmitted.Visible = false;
                            btnAdmit.PostBackUrl = $"/Web_Forms/AdmitPatient/AdmitPatient.aspx?appid={id}";
                        }
                        if (WasAdmitted(id))
                        {
                            lblAdmitted.Text = "This patient has been discharged";
                            btnAdmit.Visible = false ;
                            lblAdmitted.Visible = true ;
                        }
                        //if (HasCollectedMed(id))
                        //{
                        //    btnCollectMed.Visible = false;
                        //    btnCollectMed.PostBackUrl = $"/Web_Forms/Payment/CollectMedication.aspx?appid={id}";
                        //}
                        //else
                        //{
                        //    btnCollectMed.Visible = false;
                        //    btnCollectMed.PostBackUrl = $"/Web_Forms/Payment/CollectMedication.aspx?appid={id}";
                        //}
                        //btnXRay.PostBackUrl = $"/Web_Forms/AppointmentDetails/Bookxray.aspx?appid={id}";
                        //cnn = new SqlConnection(connectionstring);
                        //cnn.Open();
                        //SqlCommand cmd = new SqlCommand("SELECT xrayID FROM XRAY WHERE AppID = @id", cnn);
                        //cmd.Parameters.AddWithValue("id", id);
                        //SqlDataReader reader = cmd.ExecuteReader();
                        //while (reader.Read())
                        //{
                        //    btnXRay.Text = "View X-RAY Report";
                        //    btnXRay.PostBackUrl = $"/Web_Forms/XRAY/ViewXRayReport.aspx?appid={id}";

                        //}

                        //reader.Close();//close reader

                    }
                    else
                    {
                        btnXRay.Text = "IS NULL";
                    }
                }
                else
                {
                    string id = Request.QueryString["appid"];
                    if (id != null)
                    {
                        HttpCookie redirectCookie = new HttpCookie("redirectInfo"); //store cookie
                        redirectCookie["redirectPage"] = id;
                        Response.Cookies.Add(redirectCookie);


                    }
                        ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess",
                            "alert('Only Doctors can view this page!'); window.location.href = '/Web_Forms/Login.aspx';", true);
                }
                
            }
        }

        protected void btnXRay_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (HasBookedXRAY(id))
            {
                btnXRay.Text = "View X-RAY Report";
                btnXRay.PostBackUrl = $"/Web_Forms/XRAY/ViewXRayReport.aspx?appid={id}";
            }
            else
            {
                //bookxray(id);
                //string id = Request.QueryString["appid"];
                if (id != null)
                {
                    Response.Redirect($"~/Web_Forms/XRAY/AddXRAY.aspx?appid={id}");
                }
                //ClientScript.RegisterStartupScript(this.GetType(), "alert",
                //   $"alert('X-RAY Successfully Booked!');", true);
                //btnXRay.PostBackUrl = $"/Web_Forms/AppointmentDetails/Bookxray.aspx?appid={id}";
            }
        }
        bool HasBookedXRAY(string id)
        {
            string query = "SELECT xrayID FROM XRAY WHERE AppID = @id";

            cnn = new SqlConnection(connectionstring);
            cnn.Open();
            using (SqlCommand cmd = new SqlCommand(query, cnn))
            {

                cmd.Parameters.AddWithValue("@ID", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows; // Returns true if at least one record exists
                }
            }
        }
        bool HasBookedLab(string id)
        {
            string query = "SELECT labID FROM LabResults WHERE appID = @id";

            cnn = new SqlConnection(connectionstring);
            cnn.Open();
            using (SqlCommand cmd = new SqlCommand(query, cnn))
            {

                cmd.Parameters.AddWithValue("@ID", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows; // Returns true if at least one record exists
                }
            }
        }
        bool HasCollectedMed(string id)
        {
            string query = "SELECT preID FROM Prescription WHERE appID = @id AND Collected = 'No'";

            cnn = new SqlConnection(connectionstring);
            cnn.Open();
            using (SqlCommand cmd = new SqlCommand(query, cnn))
            {

                cmd.Parameters.AddWithValue("@ID", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows; // Returns true if at least one record exists
                }
            }
        }
        bool HasAdmittedPatient(string id)
        {
            string query = "SELECT admitID FROM Admitted_Patients WHERE appID = @id AND discharged = 'No'";

            cnn = new SqlConnection(connectionstring);
            cnn.Open();
            using (SqlCommand cmd = new SqlCommand(query, cnn))
            {

                cmd.Parameters.AddWithValue("@ID", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows; // Returns true if at least one record exists
                }
            }
        }
        bool WasAdmitted(string id)
        {
            string query = "SELECT admitID FROM Admitted_Patients WHERE appID = @id AND discharged = 'Yes'";

            cnn = new SqlConnection(connectionstring);
            cnn.Open();
            using (SqlCommand cmd = new SqlCommand(query, cnn))
            {

                cmd.Parameters.AddWithValue("@ID", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows; // Returns true if at least one record exists
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


                SqlCommand comm = new SqlCommand("SELECT a.Category, a.Speciality, a.Doctor, a.Date, a.Time, a.Progress, a.Notes, u.Name, u.Surname, u.Email  FROM Appointments a LEFT JOIN Users u ON u.User_ID = a.User_ID WHERE AppID = @appID", cnn);
                comm.Parameters.AddWithValue("@appID", id);

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    //txtCategory.Text = reader.GetValue(0).ToString();
                    //txtSpeciality.Text = reader.GetValue(1).ToString();
                    //txtDoctor.Text = reader.GetValue(2).ToString();
                    txtDate.Text = reader.GetValue(3).ToString();
                    txtTime.Text = reader.GetValue(4).ToString();
                    //txtProgress.Text = reader.GetValue(5).ToString();
                    txtNotes.Text = reader.GetValue(6).ToString();
                    txtNameSurname.Text = reader.GetValue(7).ToString() + " " + reader.GetValue(8).ToString();
                    txtEmail.Text = reader.GetValue(9).ToString();


                }
                cnn.Close();

            }
            catch (SqlException error)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
            }
        }

        protected void btnPrescribe_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (id != null)
            {
                Response.Redirect($"~/Web_Forms/AppointmentDetails/PrescribeMedication.aspx?appid={id}");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (id != null)
            {
                try
                {
                    
                    string connectionString = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                    using (SqlConnection cnn = new SqlConnection(connectionString))
                    {
                        cnn.Open();

                        using (SqlCommand cmd = new SqlCommand(
                            "UPDATE Appointments SET Status = @status, Progress = @progress, Notes = @notes WHERE AppID = @appid",
                            cnn))
                        {
                            if ( drdProgress.SelectedIndex == 0 )
                            {
                                cmd.Parameters.AddWithValue("@status", "Completed");
                                lblAdmitted.Text = "This patient has been discharged";
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@status", "Active");
                            }
                            cmd.Parameters.AddWithValue("@progress", drdProgress.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                            cmd.Parameters.AddWithValue("@appid", id);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            // You might want to check rowsAffected to verify the update worked
                        }
                    }
                    dischargeAdmitted(id);
                    //loadAppDetails(id);
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                "alert('Appointment successfully updated!');", true);

                }
                catch (SqlException error)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
                }
            }
        }
        public void dischargeAdmitted(string id)
        {
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "UPDATE Admitted_Patients SET discharged = @status WHERE AppID = @appid",
                        cnn))
                    {
                        if (drdProgress.SelectedIndex == 0)
                        {
                            cmd.Parameters.AddWithValue("@status", "Yes");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@status", "No");
                        }
                        
                        cmd.Parameters.AddWithValue("@appid", id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        
                    }
                }
                
            }
            catch (SqlException error)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
            }
        }

        protected void btnUpdate_Click1(object sender, EventArgs e)
        {

        }

        protected void btnLab_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (HasBookedLab(id))
            {
                btnLab.Text = "View LAB Tests";
                btnLab.PostBackUrl = $"/Web_Forms/Lab/ViewLabResults.aspx?appid={id}";
            }
            else
            {
                bookLab(id);
                btnLab.Text = "View LAB Tests";
                btnLab.PostBackUrl = $"/Web_Forms/Lab/ViewLabResults.aspx?appid={id}";
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        $"alert('Lab Successfully Booked!');", true);
                //btnLab.PostBackUrl = $"/Web_Forms/Lab/AddLabResults.aspx?appid={id}";
            }
        }


        protected void btnCollectMed_Click(object sender, EventArgs e)
        {

        }
        public void bookLab(string id)
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
                        SqlCommand comm = new SqlCommand("INSERT INTO LabResults(appID) VALUES (@appid)", cnn);
                        comm.Parameters.AddWithValue("@appid", id);
                        //comm.Parameters.AddWithValue("@overview", TextBox9.Text);
                        //comm.Parameters.AddWithValue("@report", TextBox10.Text); // Fixed parameter name
                        comm.ExecuteNonQuery();
                    }

                //    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                //        $"alert('Lab Successfully Booked!');", true);
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
        public void bookxray(string id)
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
                    SqlCommand comm = new SqlCommand("INSERT INTO XRAY(appID) VALUES (@appid)", cnn);
                    comm.Parameters.AddWithValue("@appid", id);
                    //comm.Parameters.AddWithValue("@overview", TextBox9.Text);
                    //comm.Parameters.AddWithValue("@report", TextBox10.Text); // Fixed parameter name
                    comm.ExecuteNonQuery();
                }

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

        protected void btnAdmit_Click(object sender, EventArgs e)
        {

        }
    }
}