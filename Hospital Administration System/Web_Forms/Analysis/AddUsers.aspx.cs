using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Analysis
{
    public partial class AddUsers : System.Web.UI.Page
    {
        String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
        SqlConnection cnn;
        protected void Page_Load(object sender, EventArgs e)
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

            if (dlstGender.SelectedIndex == 0)
            {
                lblMatch0.Visible = true;
                return;
            }
            lblMatch0.Visible = false;

            int defaultUserTypeID = 2;
            if (drdUserType.SelectedIndex == 0)
                defaultUserTypeID = 2;
            else if (drdUserType.SelectedIndex == 1)
                defaultUserTypeID = 3;
            else if (drdUserType.SelectedIndex == 2)
                defaultUserTypeID = 4;
            else if (drdUserType.SelectedIndex == 3)
                defaultUserTypeID = 5;
            else if (drdUserType.SelectedIndex == 4)
                defaultUserTypeID = 1;
            else if (drdUserType.SelectedIndex == 5)
                defaultUserTypeID = 6;

            string password = txtbxName.Text + "@123";
            try
            {
                using (cnn = new SqlConnection(connectionstring))
                {
                    cnn.Open();
                    // Define default user type ID
                   
                    SqlCommand comm = new SqlCommand(
                        "INSERT INTO Users (Name, Surname, Gender, Email, Phone, UserType_ID, Password) " +
                        "VALUES (@name, @surname, @gender, @email, @phone, @usertypeid, @password)", cnn);

                    comm.Parameters.AddWithValue("@name", txtbxName.Text);
                    comm.Parameters.AddWithValue("@surname", txtbxSurname.Text);
                    comm.Parameters.AddWithValue("@gender", dlstGender.SelectedItem.Value.ToString());
                    comm.Parameters.AddWithValue("@email", txtEmail.Text);
                    comm.Parameters.AddWithValue("@phone", txtPhone.Text);
                    comm.Parameters.AddWithValue("@usertypeid", defaultUserTypeID);
                    comm.Parameters.AddWithValue("@password", password);

                    comm.ExecuteNonQuery();



                    cnn.Close();
                    sendMail(txtEmail.Text, password);
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('User Successfully Added!');", true);
                    //empty items
                    txtbxName.Text = null; txtbxSurname = null; drdUserType.SelectedIndex = 0; dlstGender.SelectedIndex = 0; txtEmail.Text = null; txtPhone.Text = null; password = null;
                }
            }
            catch (SqlException error)
            {
                // Simple error handling - replace with your preferred logging method
                System.Diagnostics.Debug.WriteLine("SQL Error: " + error.Message);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('An error occurred during registration.');", true);
            }

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
        public void sendMail(string email, string password)
        {
            try
            {
                string subject = "Your MediConnect Account Credentials";
                string body = $@"
<html>
<head>
    <style>
        body {{
            font-family: 'Arial', sans-serif;
            line-height: 1.6;
            color: #333333;
            margin: 0;
            padding: 0;
            background-color: #f5f5f5;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            padding: 30px;
            background-color: #ffffff;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }}
        .header {{
            color: #2a9d8f;
            padding-bottom: 15px;
            text-align: center;
            border-bottom: 1px solid #eeeeee;
            margin-bottom: 20px;
        }}
        .logo {{
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 10px;
        }}
        .credentials {{
            background-color: #f8f9fa;
            padding: 20px;
            border-radius: 5px;
            margin: 20px 0;
            border-left: 4px solid #2a9d8f;
        }}
        .credential-row {{
            margin-bottom: 10px;
        }}
        .label {{
            font-weight: bold;
            color: #264653;
            display: inline-block;
            width: 100px;
        }}
        .value {{
            color: #2a9d8f;
            font-weight: 500;
        }}
        .footer {{
            margin-top: 30px;
            font-size: 12px;
            color: #7f8c8d;
            border-top: 1px solid #eeeeee;
            padding-top: 15px;
            text-align: center;
        }}
        .security-note {{
            background-color: #fff8e1;
            padding: 15px;
            border-radius: 5px;
            margin: 20px 0;
            border-left: 4px solid #ffc107;
            font-size: 14px;
        }}
        .button {{
            display: inline-block;
            padding: 10px 20px;
            background-color: #2a9d8f;
            color: white;
            text-decoration: none;
            border-radius: 5px;
            margin: 15px 0;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <div class='logo'>Medi<span style='color:#264653'>Connect</span></div>
            <h2>Your Account Has Been Created</h2>
        </div>
        
        <p>Dear User,</p>
        
        <p>Thank you for registering with MediConnect. Below are your login credentials:</p>
        
        <div class='credentials'>
            <div class='credential-row'>
                <span class='label'>Email:</span>
                <span class='value'>{email}</span>
            </div>
            <div class='credential-row'>
                <span class='label'>Password:</span>
                <span class='value'>{password}</span>
            </div>
        </div>
        
        <div class='security-note'>
            <strong>Important Security Information:</strong>
            <ul>
                <li>This is a system-generated password. Please change it after your first login.</li>
                <li>Never share your credentials with anyone.</li>
                <li>Our support team will never ask for your password.</li>
            </ul>
        </div>
        
        <p>You can now login to your account using the button below:</p>
        
        <center>
            <a href='https://yourmediconnecturl.com/Web_Forms/Login.aspx' class='button'>Login to Your Account</a>
        </center>
        
        <p>If you did not request this account, please contact our support team immediately.</p>
        
        <div class='footer'>
            <p>For your security, this email was automatically generated. Please do not reply.</p>
            <p>&copy; {DateTime.Now.Year} MediConnect. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("sibisibornface@gmail.com", "MediConnect Support");
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("sibisibornface@gmail.com", "ofleicpmxgjnfrlr");
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", $"alert('{ex.Message}');", true);
            }
        }
    }
}