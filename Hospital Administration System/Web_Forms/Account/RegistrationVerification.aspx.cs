using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Remoting.Messaging;

namespace Hospital_Administration_System.Web_Forms.Account
{
    public partial class RegistrationVerification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialize the verification code on first load
                if (Session["VerificationCode"] == null)
                {
                    //lblMessage.Visible = true;
                    //lblMessage.Text = "I am null";
                    string user = Request.QueryString["user"];
                    Session["VerificationCode"] = GenerateVerificationCode();
                    sendMail(user, Session["VerificationCode"].ToString());
                    //Label1.Text = Session["VerificationCode"].ToString();

                }
               // lblMessage.Visible = false;
            }
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
            SqlConnection cnn;
            string enteredCode = txtVerificationCode.Text.Trim();
            string storedCode = Session["VerificationCode"] as string;
            HttpCookie userCookie = new HttpCookie("userInfo"); //store cookie
            string id = Request.QueryString["id"];


            if (string.IsNullOrEmpty(enteredCode))
            {
                ShowErrorMessage("Please enter the verification code");
                return;
            }

            if (enteredCode == storedCode)
            {

                HttpCookie registrationCookieRetriever = Request.Cookies["registerInfo"];
                if (registrationCookieRetriever != null)
                {
                   
                    try
                    {
                        using (cnn = new SqlConnection(connectionstring))
                        {
                            cnn.Open();
                            // Define default user type ID
                            int defaultUserTypeID = 1;
                            SqlCommand comm = new SqlCommand(
                                "INSERT INTO Users (Name, Surname, Gender, Email, Phone, UserType_ID, Password) " +
                                "VALUES (@name, @surname, @gender, @email, @phone, @usertypeid, @password)", cnn);

                            comm.Parameters.AddWithValue("@name", registrationCookieRetriever["name"].ToString());
                            comm.Parameters.AddWithValue("@surname", registrationCookieRetriever["surname"].ToString());
                            comm.Parameters.AddWithValue("@gender", registrationCookieRetriever["gender"].ToString());
                            comm.Parameters.AddWithValue("@email", registrationCookieRetriever["email"].ToString());
                            comm.Parameters.AddWithValue("@phone", registrationCookieRetriever["phone"].ToString());
                            comm.Parameters.AddWithValue("@usertypeid", defaultUserTypeID);
                            comm.Parameters.AddWithValue("@password", registrationCookieRetriever["password"].ToString());
                            comm.ExecuteNonQuery();
                            cnn.Close();

                            
                            userCookie["email"] = registrationCookieRetriever["email"].ToString(); //assign password to string
                            userCookie["userType"] = "1";
                            userCookie["userLogged"] = "1";
                            Response.Cookies.Add(userCookie);
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Account Successfully Created!'); window.location='/Web_Forms/Home.aspx';", true);

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
            }
            else
            {
                ShowErrorMessage("Incorrect verification code. Please try again.");
            }
        }

        protected void btnResend_Click(object sender, EventArgs e)
        {
            // Disable the button immediately
            btnResend.Enabled = false;

            // Generate and store new code
            string newCode = GenerateVerificationCode();
            Session["VerificationCode"] = newCode;

            // In a real application, you would send this code via email
            // SendVerificationEmail(newCode);

            // Show success message
            lblMessage.Text = "New verification code sent to your email!";
            lblMessage.CssClass = "success-message";
            lblMessage.Visible = true;

            // Register script to restart timer on client side
            ScriptManager.RegisterStartupScript(this, GetType(), "restartTimer",
                "timeLeft = 120; startTimer(); document.getElementById('" + timerContainer.ClientID + "').style.display = 'block';", true);
        }

        private string GenerateVerificationCode()
        {
            // Generate a 6-digit random number
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private void ShowErrorMessage(string message)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = "error-message";
            lblMessage.Visible = true;
        }

        [WebMethod]
        public static string ResendVerificationCode()
        {
            // This method can be called from client-side via PageMethods
            // Implement your actual resend logic here
            return "Verification code resent successfully";
        }
        public void sendMail(string email, string verificationCode)
        {
            try
            {


                string subject = "Your Secure Verification Code";
                string body = $@"
<html>
<head>
    <style>
        body {{ 
            font-family: 'Arial', sans-serif; 
            line-height: 1.6;
            color: #333333;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            padding: 20px;
        }}
        .header {{
            color: #2c3e50;
            border-bottom: 1px solid #eeeeee;
            padding-bottom: 10px;
        }}
        .code {{
            font-size: 24px;
            font-weight: bold;
            color: #e74c3c;
            letter-spacing: 2px;
            margin: 20px 0;
            padding: 10px 15px;
            background-color: #f9f9f9;
            border-left: 4px solid #e74c3c;
            display: inline-block;
        }}
        .footer {{
            margin-top: 30px;
            font-size: 12px;
            color: #7f8c8d;
            border-top: 1px solid #eeeeee;
            padding-top: 10px;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h2>Account Verification</h2>
        </div>
        
        <p>Dear Valued User,</p>
        
        <p>Thank you for using our services. Please use the following verification code to complete your authentication:</p>
        
        <div class='code'>{verificationCode}</div>
        
        <p>This code is valid for a limited time and should not be shared with anyone, including our support team.</p>
        
        <p>If you did not request this code, please ignore this message or contact our support team immediately.</p>
        
        <div class='footer'>
            <p>For your security, this email was automatically generated. Please do not reply.</p>
            <p>&copy; {DateTime.Now.Year} MediConnect. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("sibisibornface@gmail.com", "Hospital Administration");
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                // Attach the PDF from the memory stream
                //Attachment pdfAttachment = new Attachment(pdfStream, "Invoice.pdf", "application/pdf");
                //mail.Attachments.Add(pdfAttachment);

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)) // Change to 587
                {
                    smtp.Credentials = new NetworkCredential("sibisibornface@gmail.com", "ofleicpmxgjnfrlr");
                    smtp.EnableSsl = true; // Required for STARTTLS on port 587
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