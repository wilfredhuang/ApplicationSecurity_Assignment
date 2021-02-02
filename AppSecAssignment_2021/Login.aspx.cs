using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppSecAssignment_2021
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;

        protected void Page_Load(object sender, EventArgs e)
        {
            // If theres a ongoing session and user try to go to this page, redirect them away
            if (Session["UserEmail"] != null && Session["AuthCookie"] != null && Request.Cookies["AuthCookie"] != null)
            {
                if (Session["AuthCookie"].ToString().Equals(Request.Cookies["AuthCookie"].Value))
                {
                    Response.Redirect("Home.aspx");
                }

                else
                {

                }
            }
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            string email = HttpUtility.HtmlEncode(tb_email.Text.ToString().Trim());
            string pwd = HttpUtility.HtmlEncode(tb_password.Text.ToString().Trim());
            SHA512Managed hashing = new SHA512Managed();

            // Get our DB service
            AS_Service_Reference.Service1Client client = new AS_Service_Reference.Service1Client();
            string dbHash = client.getDBHash(email);
            string dbSalt = client.getDBSalt(email);

            //string dbHash = getDBHash(email);
            //string dbSalt = getDBSalt(email);
            try
            {
                if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    string pwdWithSalt = pwd + dbSalt;
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    string userHash = Convert.ToBase64String(hashWithSalt);
                    if (userHash.Equals(dbHash))
                    {
                        Session["UserEmail"] = email;


                        // Create a new GUID and save into the session
                        string guidToken = Guid.NewGuid().ToString();
                        Session["AuthCookie"] = guidToken;

                        // now create a new cookie with this guid value
                        Response.Cookies.Add(new HttpCookie("AuthCookie", guidToken));

                        Response.Redirect("Success.aspx", false);

                    }


                    else
                    {
                        //Response.Redirect("Login.aspx", false);
                    }
                }

                else
                {
                    lbl_loginErrMsg.Text = "Invalid Email or Password";
                    lbl_loginErrMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
        }

    }




}