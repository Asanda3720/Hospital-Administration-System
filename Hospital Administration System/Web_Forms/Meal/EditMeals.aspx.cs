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
    public partial class EditMeals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
           /* if (drdBreakfast.SelectedIndex != 0 || drdLunch.SelectedIndex != 0 || drdDinner.SelectedIndex != 0)
            {
                lblErrorMeal.Visible = false;
                //string breakfast = null, lunch = null, dinner = null;

                string id = Request.QueryString["appid"];
                try
                {
                    //String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdministrationSystem"].ConnectionString;
                    ////SqlConnection cnn;
                    ////SqlDataAdapter adapter;
                    ////SqlCommand cmd;
                    //SqlConnection cnn = new SqlConnection(connectionstring);
                    //cnn.Open();

                    //SqlCommand cmd = new SqlCommand("UPDATE AppointmentMeals SET Breakfast = @breakfast, Lunch = @lunch, Dinner = @dinner WHERE  AppID = @appid", cnn);
                    //if (drdBreakfast.SelectedIndex != 0)
                    //    breakfast = drdBreakfast.SelectedValue.ToString();
                    //if (drdLunch.SelectedIndex != 0)
                    //    lunch = drdLunch.SelectedValue.ToString();
                    //if (drdDinner.SelectedIndex != 0)
                    //    dinner = drdDinner.SelectedValue.ToString();

                    //cmd.Parameters.AddWithValue("@breakfast", breakfast);
                    //cmd.Parameters.AddWithValue("@lunch", lunch);
                    //cmd.Parameters.AddWithValue("@dinner", dinner);
                    //cmd.Parameters.AddWithValue("@appid", id);
                    //SqlDataAdapter adapter = new SqlDataAdapter();
                    //adapter.UpdateCommand = cmd;
                    //adapter.UpdateCommand.ExecuteNonQuery();
                    string connectionString = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;

                    // Initialize with default values (null or empty string depending on your DB schema)
                    string breakfast = null;
                    string lunch = null;
                    string dinner = null;

                    if (drdBreakfast.SelectedIndex != 0)
                        breakfast = drdBreakfast.SelectedValue.ToString();
                    if (drdLunch.SelectedIndex != 0)
                        lunch = drdLunch.SelectedValue.ToString();
                    if (drdDinner.SelectedIndex != 0)
                        dinner = drdDinner.SelectedValue.ToString();

                    using (SqlConnection cnn = new SqlConnection(connectionString))
                    {
                        cnn.Open();

                        using (SqlCommand cmd = new SqlCommand(
                            "UPDATE AppointmentMeals SET Breakfast = @breakfast, Lunch = @lunch, Dinner = @dinner WHERE AppID = @appid",
                            cnn))
                        {
                            cmd.Parameters.AddWithValue("@breakfast", breakfast ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@lunch", lunch ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@dinner", dinner ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@appid", id);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            // You might want to check rowsAffected to verify the update worked
                        }
                    }
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                $"alert('Meals Successfully Updated!'); window.location='/Web_Forms/Meal/YourMeals.aspx?appID={id}';", true);

                }
                catch (SqlException error)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
                }
            }
            else
            {
                lblErrorMeal.Visible = true;
            }*/
        }
    }
}