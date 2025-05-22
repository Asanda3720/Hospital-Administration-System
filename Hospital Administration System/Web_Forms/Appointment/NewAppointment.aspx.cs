using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Appointment
{
    public partial class NewAppointment : System.Web.UI.Page
    {
        String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
        SqlConnection cnn;
        protected void Page_Load(object sender, EventArgs e)
        {
           // txtName1.Text = "Albert Einstein";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (calAppDate.SelectedDate > DateTime.Now)
            {
                lblErrorDate.Visible = false;
                string id = Request.QueryString["id"];
                if (id != null)
                {
                    HttpCookie appCookie = new HttpCookie("appInfo"); //store cookie
                    appCookie["category"] = drdAppCategory.SelectedValue.ToString();
                    appCookie["speciality"] = drdDocSpeciality.SelectedValue.ToString();
                    appCookie["date"] = calAppDate.SelectedDate.ToString();
                    appCookie["time"] = drdAppTime.SelectedValue.ToString();
                    appCookie["userid"] = id;
                    Response.Cookies.Add(appCookie);
            //        Response.Redirect($"/Web_Forms/Payment/Payment.aspx?id={id}");
                    Response.Redirect($"/Web_Forms/Payment/Paymentss.aspx?id={id}");
                    //cnn = new SqlConnection(connectionstring);
                    //cnn.Open(); //open connection

                    //SqlCommand comm = new SqlCommand($"INSERT INTO Appointments(Category, Speciality, Doctor, Date, Time, User_ID, Status, Rescheduled) VALUES (@category, @speciality, @doctor, @date, @time, @userid, @status, @rescheduled)", cnn);
                    //comm.Parameters.AddWithValue("@category", drdAppCategory.SelectedValue.ToString());
                    //comm.Parameters.AddWithValue("@speciality", drdDocSpeciality.SelectedValue.ToString());
                    //comm.Parameters.AddWithValue("@doctor", txtName1.Text);


                    //comm.Parameters.AddWithValue("@date", calAppDate.SelectedDate);
                    //comm.Parameters.AddWithValue("@time", drdAppTime.SelectedValue.ToString());
                    //comm.Parameters.AddWithValue("@userid", id);
                    //comm.Parameters.AddWithValue("@status", "Active");
                    //comm.Parameters.AddWithValue("@rescheduled", 0);
                    //comm.ExecuteNonQuery();
                    //cnn.Close(); //close connection

                    //ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    //        "alert('Appointment Successfully Booked!'); window.location='/Web_Forms/DirectAppointmentBooking.aspx';", true);

                }
                else
                {
                    Response.Redirect("~/Web_Forms/Login.aspx");
                }
            }
            {
                lblErrorDate.Visible = true;
            }
        }

        protected void txtName0_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

//Doctor&#39;s Name:    On the textbox