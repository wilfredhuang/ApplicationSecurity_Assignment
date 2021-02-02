using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppSecAssignment_2021
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();


            // Fix LogoutMe method, remove cookie from user's browser and expire the authtoken cookie when user logout

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);

            }

            if (Request.Cookies["AuthCookie"] != null)
            {
                Response.Cookies["AuthCookie"].Value = string.Empty;
                Response.Cookies["AuthCookie"].Expires = DateTime.Now.AddMonths(-20);

            }
        }
    }
}