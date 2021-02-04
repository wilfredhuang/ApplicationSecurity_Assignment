using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppSecAssignment_2021
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        // Required for hashing and encryption
        static string new_finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserEmail"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void btn_submitChangePwd_Click(object sender, EventArgs e)
        {
            AS_Service_Reference.Service1Client client = new AS_Service_Reference.Service1Client();

            var current_user = client.GetOneUser(Session["UserEmail"].ToString());

            var new_password = HttpUtility.HtmlEncode(tb_changePwd.Text.Trim());
            var pwdStrength = checkPassword(new_password);

            if (pwdStrength != 5)
            {
                checkPasswordFeedback(new_password);
            }

            else
            {
                salt = current_user.PasswordSalt;
                SHA512Managed hashing = new SHA512Managed();

                string new_pwdWithSalt = new_password + salt;

                byte[] newhashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(new_pwdWithSalt));

                var new_finalHash = Convert.ToBase64String(newhashWithSalt);

                // If new password match old password
                if (new_finalHash == current_user.PasswordHash)
                {
                    lbl_resultMsg.Text = "Error, input same as your current password";
                    lbl_resultMsg.ForeColor = Color.Red;
                    return;
                }

                if (current_user.PasswordChangeCoolDown > DateTime.Now)
                {
                    lbl_resultMsg.Text = "Error, you are not allowed to change passwords repeatedly in a short span of time";
                    lbl_resultMsg.ForeColor = Color.Red;
                    return;
                }

                else
                {
                    // Test whether the salt value are the same
                    /*                    lbl_resultMsg.Text = $"{new_finalHash} | {current_user.PasswordHash}";
                                        lbl_resultMsg.ForeColor = Color.Red;*/

                    if (new_finalHash == current_user.PasswordHash_1 || new_finalHash == current_user.PasswordHash_2)
                    {
                        lbl_resultMsg.Text = "Error, you are not allowed to reuse recent passwords";
                        lbl_resultMsg.ForeColor = Color.Red;
                    }

                    else
                    {
                        client.ChangePassword(current_user.Email, new_finalHash, current_user.PasswordHash, current_user.PasswordHash_1, current_user.PasswordHash_2);
                        lbl_resultMsg.Text = "Password changed successfully";
                        lbl_resultMsg.ForeColor = Color.Green;

                    }



                }


            }


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
            int scores = checkPassword(tb_changePwd.Text);

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