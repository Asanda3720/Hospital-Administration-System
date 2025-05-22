using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Emergency
{
    public partial class ConfirmedEmergency : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie emergencyCookie = Request.Cookies["emergencyInfo"];
            if ( emergencyCookie != null )
            {
                Label1.Text = emergencyCookie["location"].ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void Label1_DataBinding(object sender, EventArgs e)
        {

        }
    }
}