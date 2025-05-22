using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Meal
{
    public partial class ConfirmMeals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie mealCookieRetriever = Request.Cookies["mealInfo"];
            if (mealCookieRetriever != null)
            {
                TextBox1.Text = mealCookieRetriever["breakfast"].ToString();
                TextBox2.Text = mealCookieRetriever["lunch"].ToString();
                TextBox3.Text = mealCookieRetriever["dinner"].ToString();
            }
            
        }

        
        
        public string getAppMealID(string email)
        {
            string id = null;
            String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
            SqlConnection cnn = new SqlConnection(connectionstring);
            cnn.Open();

            SqlCommand cmd = new SqlCommand("SELECT u.User_ID, a.AppID FROM Users u LEFT JOIN Appointments a ON a.User_ID = u.User_ID WHERE Email = @email AND a.Status = 'Active'", cnn);
            cmd.Parameters.AddWithValue("@email", email);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetValue(1).ToString();
            }

            reader.Close();//close reader

            return id;
        }

 
        protected void btnBack_Click1(object sender, EventArgs e)
        {
            
                Response.Redirect($"~/Web_Forms/Meal/OrderMeals.aspx");
            
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                string email = userCookieRetriever["email"];

                
                    try
                    {
                        String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                        SqlConnection cnn = new SqlConnection(connectionstring);
                        cnn.Open(); //open connection
                        string appID = getAppMealID(email);
                        SqlCommand comm = new SqlCommand($"INSERT INTO AppointmentMeals(Breakfast, Lunch, Dinner, AppID) VALUES (@breakfast, @lunch, @dinner, @appID)", cnn);
                        comm.Parameters.AddWithValue("@breakfast", TextBox1.Text);
                        comm.Parameters.AddWithValue("@lunch", TextBox2.Text);
                        comm.Parameters.AddWithValue("@dinner", TextBox3.Text);
                        comm.Parameters.AddWithValue("@appID", appID);
                        comm.ExecuteNonQuery();
                        cnn.Close(); //close connection



                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                $"alert('Meals Successfully Ordered!'); window.location='/Web_Forms/Meal/YourMeals.aspx?appID={appID}';", true);
                    }
                    catch (SqlException error)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
                    }
                
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('Your Session has expired! Login again.'); window.location='/Web_Forms/Login.aspx';", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            HttpCookie userCookieRetriever = Request.Cookies["userInfo"];
            if (userCookieRetriever != null)
            {
                string email = userCookieRetriever["email"];


                try
                {
                    String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                    SqlConnection cnn = new SqlConnection(connectionstring);
                    cnn.Open(); //open connection
                    string appID = getAppMealID(email);
                    SqlCommand comm = new SqlCommand($"INSERT INTO AppointmentMeals(Breakfast, Lunch, Dinner, AppID) VALUES (@breakfast, @lunch, @dinner, @appID)", cnn);
                    comm.Parameters.AddWithValue("@breakfast", TextBox1.Text);
                    comm.Parameters.AddWithValue("@lunch", TextBox2.Text);
                    comm.Parameters.AddWithValue("@dinner", TextBox3.Text);
                    comm.Parameters.AddWithValue("@appID", appID);
                    comm.ExecuteNonQuery();
                    cnn.Close(); //close connection



                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            $"alert('Meals Successfully Ordered!'); window.location='/Web_Forms/Meal/YourMeals.aspx?appID={appID}';", true);
                }
                catch (SqlException error)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
                }

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('Your Session has expired! Login again.'); window.location='/Web_Forms/Login.aspx';", true);
            }
        }
    }
}