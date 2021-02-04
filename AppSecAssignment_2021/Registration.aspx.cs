using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

// Done by Admin No: 191589S

namespace AppSecAssignment_2021
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // Required for hashing and encryption
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        //
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
                return 0;
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
                case 0:
                    status = "Insufficient length";
                    break;
                case 1:
                    status = "Very Weak";
                    break;
                case 2:
                    status = "Weak";
                    break;
                case 3:
                    status = "Weak";
                    break;
                case 4:
                    status = "Insufficient Strength";
                    break;
                case 5:
                    status = "Excellent";
                    break;

                default:
                    break;
            }

            lbl_pwdchecker.Visible = true;
            lbl_pwdchecker.Text = "Strength: " + status;

            if (scores < 5)
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
            lbl_fnFeedback.Visible = false;
            lbl_lnFeedback.Visible = false;
            lbl_pwdchecker.Visible = false;
            lbl_emailFeedback.Visible = false;
            lbl_dobFeedback.Visible = false;
            lbl_cciFeedback.Visible = false;
            bool parse_result;
            long cciTest;
            DateTime dob;
            // Prevent XSS by sanitizing the user input with htmlencode
            var firstName = HttpUtility.HtmlEncode(tb_firstName.Text.Trim());
            var lastName = HttpUtility.HtmlEncode(tb_lastName.Text.Trim());
            var password = HttpUtility.HtmlEncode(tb_password.Text.Trim());
            var emailAddress = HttpUtility.HtmlEncode(tb_emailAddress.Text.Trim());
            var creditCardInfo = HttpUtility.HtmlEncode(tb_creditCardInfo.Text.Trim());


            var pwdStrength = checkPassword(password);

            // Validation

            var check = true;

            if (firstName.Length == 0)
            {
                lbl_fnFeedback.Text = "Input required";
                lbl_fnFeedback.ForeColor = Color.Red;
                lbl_fnFeedback.Visible = true;
                check = false;
            }
            if (lastName.Length == 0)
            {
                lbl_lnFeedback.Text = "Input required";
                lbl_lnFeedback.ForeColor = Color.Red;
                lbl_lnFeedback.Visible = true;
                check = false;
            }

            if (emailAddress.Length == 0)
            {
                lbl_emailFeedback.Text = "Input required";
                lbl_emailFeedback.ForeColor = Color.Red;
                lbl_emailFeedback.Visible = true;
                check = false;
            }

            parse_result = DateTime.TryParse(tb_dob.Text, out dob);
            if (!parse_result)
            {
                lbl_dobFeedback.Text = "Birth Date is invalid!";
                lbl_dobFeedback.ForeColor = Color.Red;
                lbl_dobFeedback.Visible = true;
                check = false;
            }
            if (tb_creditCardInfo.Text.Trim().Length != 16)
            {
                lbl_cciFeedback.Text = $"Invalid credit card details, 16 digits required {tb_creditCardInfo.Text.Trim().Length}";
                lbl_cciFeedback.ForeColor = Color.Red;
                lbl_cciFeedback.Visible = true;
                check = false;
            }

            var cc_test_result = Int64.TryParse(tb_creditCardInfo.Text.Trim(), out cciTest);

            if (cc_test_result == false)
            {
                lbl_cciFeedback.Text = $"Invalid credit card details, only 16 digits allowed {tb_creditCardInfo.Text.Trim()}";
                lbl_cciFeedback.ForeColor = Color.Red;
                lbl_cciFeedback.Visible = true;
                check = false;
            }

            if (pwdStrength != 5)
            {
                checkPasswordFeedback(password);
                check = false;
            }



            // ENSURE ALL USERS USE EXCELLENT PASSWORDS!

            // If no problems, create user
            if (check == true)
            {
                // Generate random "salt"
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] saltByte = new byte[8];

                // Fills array of bytes with a cryptographically strong sequence of random values
                rng.GetBytes(saltByte);
                salt = Convert.ToBase64String(saltByte);

                SHA512Managed hashing = new SHA512Managed();

                string pwdWithSalt = password + salt;

                byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(password));

                byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));

                finalHash = Convert.ToBase64String(hashWithSalt);

                RijndaelManaged cipher = new RijndaelManaged();

                cipher.GenerateKey();

                Key = cipher.Key;

                IV = cipher.IV;

                // Encrypt Credit Card info
                var encryptedCCInfo = Convert.ToBase64String(encryptData(creditCardInfo));

                AS_Service_Reference.Service1Client client = new AS_Service_Reference.Service1Client();
                int result = client.CreateUser(firstName, lastName, finalHash, salt, emailAddress, encryptedCCInfo, Convert.ToBase64String(IV), Convert.ToBase64String(Key), dob);
                if (result == 1)
                {
                    lbl_alertmsg.Text = "You have successfully been registered";
                    Response.Redirect("~/Success.aspx");

                }
                else
                {
                    lbl_alertmsg.Text = "Registration failed";
                }
            }









        }

        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0,
               plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }


    }
}