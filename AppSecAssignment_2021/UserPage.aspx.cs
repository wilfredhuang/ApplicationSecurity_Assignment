using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppSecAssignment_2021
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserEmail"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            else
            {
                AS_Service_Reference.Service1Client client = new AS_Service_Reference.Service1Client();


                var current_user = client.GetOneUser(Session["UserEmail"].ToString());
                var fn = current_user.FirstName;
                var ln = current_user.LastName;

                lbl_userPageTitle.Text = $"Welcome {fn} {ln}";

            }
        }

        protected void btn_redirect_changePwd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ChangePassword.aspx");
        }
    }
}