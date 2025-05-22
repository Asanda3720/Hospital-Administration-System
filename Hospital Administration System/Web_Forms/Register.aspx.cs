using System;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web;


namespace Hospital_Administration_System.Web_Forms
{
    public partial class Register : System.Web.UI.Page
    {
        //connections
        String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
        SqlConnection cnn;

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void lnkbtnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx"); //go to page
        }

        

        protected void btnLogin0_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx"); //go to page
        }

        public bool getExistingEmail()
        {
            bool exists = false;

            // Should probably be txtEmail.Text instead of txtbxName.Text
            string emailToCheck = txtEmail.Text;

            if (string.IsNullOrWhiteSpace(emailToCheck))
            {
                return false;
            }

            try
            {
                using (var connection = new SqlConnection(connectionstring))
                {
                    connection.Open();

                    using (var cmd = new SqlCommand(
                        "SELECT 1 FROM Users WHERE Email = @email", connection))
                    {
                        cmd.Parameters.AddWithValue("@email", emailToCheck);

                        // More efficient than using DataReader
                        exists = cmd.ExecuteScalar() != null;
                    }
                }
            }
            catch (SqlException error)
            {
                // Log error properly in production
                ClientScript.RegisterStartupScript(this.GetType(), "myalert",
                    "alert('An error occurred while checking email.');", true);
                // Consider returning true to prevent duplicate submissions on error
            }

            return exists;
        }


        protected void dlstGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnRegister_Click1(object sender, EventArgs e)
        {
            if (getExistingEmail())
            {
                lblEmailError.Visible = true;
                return;
            }
            lblEmailError.Visible = false;

            if ( dlstGender.SelectedIndex == 0 )
            {
                lblMatch0.Visible = true;
                return;
            }
            lblMatch0.Visible = false;

            if (txtbxPassword.Text != txtbxPassword1.Text)
            {
                lblMatch.Visible = true;
                return;
            }
            lblMatch.Visible = false;

            HttpCookie registrationCookie = new HttpCookie("registerInfo"); //store cookie
            registrationCookie["name"] = txtbxName.Text;
            registrationCookie["surname"] = txtbxSurname.Text;
            registrationCookie["gender"] = dlstGender.SelectedValue.ToString();
            registrationCookie["email"] = txtEmail.Text;
            registrationCookie["phone"] = txtPhone.Text;
            registrationCookie["password"] = txtbxPassword1.Text;
            Response.Cookies.Add(registrationCookie);

            Response.Redirect($"/Web_Forms/Account/RegistrationVerification.aspx?user={txtEmail.Text}"); //go to  page
            //try
            //{
            //    using (cnn = new SqlConnection(connectionstring))
            //    {
            //        cnn.Open();
            //        // Define default user type ID
            //        int defaultUserTypeID = 1;
            //        SqlCommand comm = new SqlCommand(
            //            "INSERT INTO Users (Name, Surname, Gender, Email, Phone, UserType_ID, Password) " +
            //            "VALUES (@name, @surname, @gender, @email, @phone, @usertypeid, @password)", cnn);

            //        comm.Parameters.AddWithValue("@name", txtbxName.Text);
            //        comm.Parameters.AddWithValue("@surname", txtbxSurname.Text);
            //        comm.Parameters.AddWithValue("@gender", dlstGender.SelectedItem.Value.ToString());
            //        comm.Parameters.AddWithValue("@email", txtEmail.Text);
            //        comm.Parameters.AddWithValue("@phone", txtPhone.Text);
            //        comm.Parameters.AddWithValue("@usertypeid", defaultUserTypeID);
            //        comm.Parameters.AddWithValue("@password", txtbxPassword1.Text);

            //        comm.ExecuteNonQuery();



            //        cnn.Close();
            //    }
            //}
            //catch (SqlException error)
            //{
            //    // Simple error handling - replace with your preferred logging method
            //    System.Diagnostics.Debug.WriteLine("SQL Error: " + error.Message);
            //    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
            //    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('An error occurred during registration.');", true);
            //}

        }
    }
}