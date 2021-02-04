using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppSecAssignment_2021
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Keep redirect
            if (Request.Url.LocalPath != "/PasswordExpired.aspx")
            {
                {
                    if (Session["UserEmail"] != null)
                    {
                        AS_Service_Reference.Service1Client client = new AS_Service_Reference.Service1Client();
                        var current_user = client.GetOneUser(Session["UserEmail"].ToString());

                        if (current_user.PasswordAge < DateTime.Now)
                        {
                            if (Request.Url.LocalPath != "/ChangePassword.aspx")
                            {
                                Response.Redirect("PasswordExpired.aspx");
                            }
                        }

                        else
                        {
                            //Response.Write(current_user.PasswordAge + "Foo");

                        }
                    }
                }
            }

        }
    }
}