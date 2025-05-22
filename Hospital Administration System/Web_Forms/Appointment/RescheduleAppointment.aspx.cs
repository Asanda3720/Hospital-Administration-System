using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Appointment
{
    public partial class RescheduleAppointment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (id != null)
                loadAppDetails(id);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["appid"];
            if (calAppDate.SelectedDate < DateTime.Now)
            {
                lblErrorDate.Visible = true;
            }
            else {
                lblErrorDate.Visible = false;
                changeAppDate(id);
                    }
        }
        public void changeAppDate(string id)
        {
            if (calAppDate.SelectedDate > DateTime.Now)
            {
                lblErrorDate.Visible = false;
                try
                {
                    String connectionstring = ConfigurationManager.ConnectionStrings["HospitalAdminDB"].ConnectionString;
                    //SqlConnection cnn;
                    //SqlDataAdapter adapter;
                    //SqlCommand cmd;
                    SqlConnection cnn = new SqlConnection(connectionstring);
                    cnn.Open();


                    SqlCommand cmd = new SqlCommand("UPDATE Appointments SET Date = @appDate, Time = @time, Rescheduled = @rescheduled WHERE  AppID = @appid", cnn);
                    cmd.Parameters.AddWithValue("@appDate", calAppDate.SelectedDate);
                    cmd.Parameters.AddWithValue("@time", drdAppTime.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@rescheduled", 1);
                    cmd.Parameters.AddWithValue("@appid", id);

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = cmd;
                    adapter.UpdateCommand.ExecuteNonQuery();

                    //addDriverAcceptedOrder(numOrder);

                    lblErrorDate.Visible = true;


                    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess",
                    "alert('Appointment rescheduled successfully!'); window.location.href = '/Web_Forms/DirectAppointmentBookings.aspx';", true);
                    cnn.Close();
                }
                catch (SqlException error)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
                }
            }
            else
            {
                lblErrorDate.Visible = true;
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


                SqlCommand comm = new SqlCommand("SELECT Category, Speciality, Doctor, Time FROM Appointments WHERE AppID = @appID", cnn);
                comm.Parameters.AddWithValue("@appID", id);
                
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    txtCategory.Text = reader.GetValue(0).ToString();
                    txtSpeciality.Text = reader.GetValue(1).ToString();
                    txtDocName.Text = reader.GetValue(2).ToString();
                    //txtTime.Text = reader.GetValue(3).ToString();
                   

                }
                cnn.Close();

            }
            catch (SqlException error)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + error.Message + "');", true);
            }
        }
    }
}