using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Account
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if ( TextBox2.Text != TextBox3.Text)
            {
                lblMatch.Visible = true;
                return;
            }
            lblMatch.Visible = false;

            //connections
            String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
            SqlConnection cnn;
            //HttpCookie userCookie = new HttpCookie("userInfo"); //store cookie
            try
            {
                //check if account exists
                cnn = new SqlConnection(connectionstring);
                cnn.Open();
                
                string id = null;
                //read from table, get the email and password
                SqlCommand cmd = new SqlCommand("SELECT User_ID  FROM Users WHERE Email = @email", cnn);
                cmd.Parameters.AddWithValue("email", TextBox1.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetValue(0).ToString();
                }

                reader.Close();//close reader

                if (id != null)
                {
                    HttpCookie resetCookie = new HttpCookie("resetInfo"); //store cookie
                    
                    resetCookie["password"] = TextBox3.Text.Trim();
                    Response.Cookies.Add(resetCookie);
                    Response.Redirect($"~/Web_Forms/Account/ForgotPasswordVerification.aspx?id={id}&user={TextBox1.Text}"); //go to  page
                }
            }
            catch (SqlException error)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
            }
        }

        
    }
}