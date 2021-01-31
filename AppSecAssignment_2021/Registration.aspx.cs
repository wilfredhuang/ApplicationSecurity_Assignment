using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppSecAssignment_2021
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            // Add Client-Side check for password strength, triggers when user input in the textbox
            tb_password.Attributes.Add("onkeyup", "password_strength_feedback();");
        }

        private int checkPassword(string password)
        {
            int score = 0;

            // include your implementation here

            // Score 0
            if (password.Length < 8)
            {
                return 1;
            }
            else
            {
                score = 1;
            }
            // Score 2
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }
            // Score 3
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }
            // Score 4
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }
            // Score 5
            if (Regex.IsMatch(password, "[^a-zA-z0-9]"))
            {
                score++;
            }

            return score;


        }

        private void checkPasswordFeedback(string password)
        {
            int scores = checkPassword(tb_password.Text);

            string status = "";
            switch (scores)
            {
                case 1:
                    status = "Very Weak";
                    break;
                case 2:
                    status = "Weak";
                    break;
                case 3:
                    status = "Medium";
                    break;
                case 4:
                    status = "Strong";
                    break;
                case 5:
                    status = "Excellent";
                    break;

                default:
                    break;
            }

            lbl_pwdchecker.Text = "Status: " + status;

            if (scores < 4)
            {
                lbl_pwdchecker.ForeColor = Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                return;

            }
            lbl_pwdchecker.ForeColor = Color.Green;
            return;
        }

        protected void btn_RegisterClick(object sender, EventArgs e)
        {
            // Prevent XSS by sanitizing the user input with htmlencode
            var firstName = HttpUtility.HtmlEncode(tb_firstName.Text);
            var lastName = HttpUtility.HtmlEncode(tb_lastName.Text);
            var emailAddress = HttpUtility.HtmlEncode(tb_emailAddress.Text);
            var password = HttpUtility.HtmlEncode(tb_password.Text);
            var dob = HttpUtility.HtmlEncode(tb_dob.Text);
            var creditCardInfo = HttpUtility.HtmlEncode(tb_creditCardInfo.Text);

            var pwdStrength = checkPassword(password);

            // ENSURE ALL USERS USE EXCELLENT PASSWORDS!
            if (pwdStrength != 5)
            {
                checkPasswordFeedback(password);
            }

            else
            {
                Response.Redirect("~/Login.aspx");
            }






        }


    }
}