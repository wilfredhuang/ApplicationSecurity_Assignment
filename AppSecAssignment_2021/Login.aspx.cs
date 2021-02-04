using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
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
            // Generate a session variable to count the number of login attempts
            // Only generates on the initial start-up of the page
            if (!IsPostBack)
            {
                Session["LoginAttempts"] = 0;
            }



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
            // First Layer, check if attempts more than 3 for current session
            // Second Layer, check if the account is locked

            // Get our DB service
            AS_Service_Reference.Service1Client client = new AS_Service_Reference.Service1Client();

            if (ValidateCaptcha_v3())
            {


                // If Login Attempt < 3
                if (Convert.ToInt32(Session["LoginAttempts"]) < 3)
                {

                    string email = HttpUtility.HtmlEncode(tb_email.Text.ToString().Trim());
                    string pwd = HttpUtility.HtmlEncode(tb_password.Text.ToString().Trim());
                    SHA512Managed hashing = new SHA512Managed();

                    string dbHash = client.getDBHash(email);
                    string dbSalt = client.getDBSalt(email);

                    try
                    {
                        if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                        {
                            string pwdWithSalt = pwd + dbSalt;
                            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                            string userHash = Convert.ToBase64String(hashWithSalt);
                            if (userHash.Equals(dbHash))
                            {

                                if (client.GetOneUser(email).AccountLockExpiry < DateTime.Now)
                                {
                                    client.RemoveAccountLockOut(email);

                                    Session["UserEmail"] = email;


                                    // Create a new GUID and save into the session
                                    string guidToken = Guid.NewGuid().ToString();
                                    Session["AuthCookie"] = guidToken;

                                    // now create a new cookie with this guid value
                                    Response.Cookies.Add(new HttpCookie("AuthCookie", guidToken));

                                    Response.Redirect("UserPage.aspx", false);
                                }

                                else
                                {
                                    lbl_loginErrMsg.Text = $"Your account has been temporarily locked due to multiple failed attempts,\n It will be available after {client.GetOneUser(email).AccountLockExpiry}";
                                    lbl_loginErrMsg.ForeColor = System.Drawing.Color.Red;
                                }


                            }



                            else
                            {
                                // When password is wrong, but we still provide a generic error message
                                lbl_loginErrMsg.Text = "Invalid Email or Password";
                                lbl_loginErrMsg.ForeColor = System.Drawing.Color.Red;
                                Session["LoginAttempts"] = Convert.ToInt32(Session["LoginAttempts"]) + 1;
                            }
                        }

                        else
                        {
                            // When email is wrong
                            lbl_loginErrMsg.Text = "Invalid Email or Password";
                            lbl_loginErrMsg.ForeColor = System.Drawing.Color.Red;
                            Session["LoginAttempts"] = Convert.ToInt32(Session["LoginAttempts"]) + 1;
                            //Response.Write(Session["LoginAttempts"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                    finally { }

                }


                // When there is no more login attempts available
                else
                {
                    lbl_loginErrMsg.Text = "You are temporarily locked from accessing the login system due to multiple failed attempts, try again later.";

                    var search_user = client.GetOneUser(tb_email.Text.Trim());

                    if (search_user != null)
                    {
                        client.SetAccountLockOut(search_user.Email);
                        //Response.Write($"SET LOG OUT FOR {search_user.FirstName} {search_user.LastName} {search_user.AccountLocked} {search_user.AccountLockExpiry}");
                    }
                }

            }

            else
            {
                lbl_captchaScore.Text = "You did not pass the captcha validation";
            }
        }
        public class MyObject
        {
            public string success { get; set; }
            public List<string> ErrorMessage { get; set; }
        }
        public bool ValidateCaptcha_v3()
        {
            bool result = true;

            string captchaResponse = Request.Form["g-recaptcha-response"];

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
                ("https://www.google.com/recaptcha/api/siteverify?secret=6Ld0mEkaAAAAAO-cLbvGeYkd7QyaB-aGXcLzAV1t &response=" + captchaResponse);
            try
            {
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();

                        lbl_captchaScore.Text = jsonResponse.ToString();

                        JavaScriptSerializer js = new JavaScriptSerializer();

                        MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);

                        result = Convert.ToBoolean(jsonObject.success);

                    }
                }
                return result;
            }

            catch (WebException ex)
            {
                throw ex;
            }
        }

    }




}