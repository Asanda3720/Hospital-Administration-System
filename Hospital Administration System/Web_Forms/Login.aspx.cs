using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialize any necessary components
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            
            //connections
            String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
            SqlConnection cnn;
            HttpCookie userCookie = new HttpCookie("userInfo"); //store cookie
            try
            {
                //check if account exists
                cnn = new SqlConnection(connectionstring);
                cnn.Open();
                //assign variables for validation
                string password_hash = "";
                bool validate = false;
                string id = null;
                //read from table, get the email and password
                SqlCommand cmd = new SqlCommand("SELECT User_ID, Password FROM Users WHERE Email = @email", cnn);
                cmd.Parameters.AddWithValue("email", txtEmail.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetValue(0).ToString();
                    validate = true; //assign true value
                    password_hash = reader.GetString(1); //assign password to string
                    //userCookie["userType"] = reader.GetValue(1).ToString();
                    //userCookie["userLogged"] = "True";

                }

                reader.Close();//close reader

                if (txtPassword.Text == password_hash && validate == true)
                {
                    //assign value to stay logged in
                    //userCookie["email"] = txtEmail.Text;
                    //Response.Cookies.Add(userCookie);
                    lblMatch.Visible = false; //set false if true


                    //Response.Redirect($"~/Web_Forms/VerificationModal.aspx?id={id}&user={txtEmail.Text}"); //go to  page -- ROLLBACK : UNCOMMENT
                    loginProcess(id);    //COMMENT THIS
                }

                else
                {
                    lblMatch.Visible = true; //feedback to user
                }
            
            }
            catch (SqlException error)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
        }
    
            
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnResend_Click(object sender, EventArgs e)
        {
            
        }
        public void loginProcess(string id)
        {
            String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
            SqlConnection cnn;
            HttpCookie userCookie = new HttpCookie("userInfo"); //store cookie
            string num = "1";
            cnn = new SqlConnection(connectionstring);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Email, UserType_ID FROM Users WHERE User_ID = @userID", cnn);
            cmd.Parameters.AddWithValue("@userID", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                userCookie["email"] = reader.GetString(0); //assign password to string
                userCookie["userType"] = reader.GetValue(1).ToString();
                num = reader.GetValue(1).ToString();
                userCookie["userLogged"] = "1";

            }


            Response.Cookies.Add(userCookie);
            Session["IsVerified"] = true;
            HttpCookie redirectCookieRetriever = Request.Cookies["redirectInfo"];
            if (num == "2")
            {
                if (redirectCookieRetriever != null)
                {
                    Response.Redirect($"/Web_Forms/AppointmentDetails/ConsultPatient.aspx?appid={redirectCookieRetriever["redirectPage"]}");

                }
            }
            Response.Redirect("~/Web_Forms/Home.aspx");
        }
        

        
    }
}