using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Administration_System.Web_Forms.Meal
{
    public partial class OrderMeals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {

            HttpCookie mealCookie = new HttpCookie("mealInfo"); //store cookie
            mealCookie["breakfast"] = null; mealCookie["lunch"] = null; mealCookie["dinner"] = null;
            if (drdBreakfast.SelectedIndex != 0 || drdLunch.SelectedIndex != 0 || drdDinner.SelectedIndex != 0)
            {
                lblErrorMeal.Visible = false;
                if (drdBreakfast.SelectedIndex != 0)
                    mealCookie["breakfast"] = drdBreakfast.SelectedValue.ToString();
                if (drdLunch.SelectedIndex != 0)
                    mealCookie["lunch"] = drdLunch.SelectedValue.ToString();
                if (drdDinner.SelectedIndex != 0)
                    mealCookie["dinner"] = drdDinner.SelectedValue.ToString();
                Response.Cookies.Add(mealCookie);
                Response.Redirect($"/Web_Forms/Meal/ConfirmMeals.aspx");
            }
            else
            {
                lblErrorMeal.Visible = true;
            }

        }
    }
}