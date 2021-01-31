<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="AppSecAssignment_2021.WebForm1" ValidateRequest="false" ClientIDMode="Static"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title> My Registration </title>
    <script> 

        var prog = 0

        function password_strength_feedback() {

            // Password Strength Feedback
            var password = document.getElementById('<%= tb_password.ClientID %>')
            var pwd = password.value

            // Reset if password length is zero
            if (pwd.length === 0) {
                document.getElementById("progresslabel").innerHTML = "";
                document.getElementById("progress").value = "0";
                return;
            }

            // Check progress
            prog = [/[$@$!%*#?&]/, /[A-Z]/, /[0-9]/, /[a-z]/]
                .reduce((accm, currentValue) => accm + currentValue.test(pwd), 0);

            // Length must be at least 8 chars
            if (prog > 2 && pwd.length > 7) {
                prog++;
            }

            // Display it
            var progress = "";
            var strength = "";
            switch (prog) {
                case 0:
                case 1:
                case 2:
                    strength = "25%";
                    progress = "25";
                    break;
                case 3:
                    strength = "50%";
                    progress = "50";
                    break;
                case 4:
                    strength = "75%";
                    progress = "75";
                    break;
                case 5:
                    strength = "100% - Eligble for registration";
                    progress = "100";
                    break;
            }
            document.getElementById("progresslabel").innerHTML = strength;
            document.getElementById("progress").value = progress;


            

        }

        function client_side_validation() {
            // This function will prevent the form from doing postback (return false)
            // if certain condition are not met

            var firstName = document.getElementById("tb_firstName").value
            var lastName = document.getElementById("tb_lastName").value
            var emailAddress = document.getElementById("tb_emailAddress").value
            var password = document.getElementById("tb_password").value
            var dob = document.getElementById("tb_dob").value
            var creditCardInfo = document.getElementById("tb_creditCardInfo").value


            if (firstName.length == 0) {
               return false
            }

            if (lastName.length == 0) {
                return false
            }

            if (emailAddress.length == 0) {
                return false
            }

            if (password.length == 0 || prog < 5) {
                return false
            }

            if (dob.length == 0) {
                return false
            }

            if (creditCardInfo.length < 16 && creditCardInfo.length > 16) {
                return false
            }

            else {
  /*              alert("Foo")
                console.log("Foo")*/
                return true
            }

            return false

        }


        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lbl_pwdchecker.ClientID %>").style.display = "none";
        }, seconds * 1000);
        };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <table class="auto-style1">
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Registration"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_firstName" runat="server" Text="First Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_firstName" runat="server"></asp:TextBox>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lbl_firstName0" runat="server" Text="Last Name"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="tb_lastName" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style2"></td>
            </tr>
            <tr>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lbl_emailAddress" runat="server" Text="Email address"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="tb_emailAddress" runat="server" TextMode="Email"></asp:TextBox>
                </td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lbl_password" runat="server" Text="Password"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="tb_password" runat="server" TextMode="Password" CssClass="auto-style3"></asp:TextBox>
                </td>
                <td class="auto-style2">
                    <asp:Label ID="lbl_pwdchecker" runat="server"></asp:Label>
                                                        <progress id="progress" value="0" max="100">70</progress>
    <span id="progresslabel"></span>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">
                                &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                                <asp:Label ID="lbl_dob" runat="server" Text="Date of Birth"></asp:Label>
                    </td>
                <td class="auto-style2">
                    <asp:TextBox ID="tb_dob" runat="server" TextMode="DateTime" CssClass="auto-style3"></asp:TextBox>
                </td>
                <td class="auto-style2"></td>
            </tr>
            <tr>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lbl_creditCardInfo" runat="server" Text="Credit Card Info"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="tb_creditCardInfo" runat="server" CssClass="auto-style4"></asp:TextBox>
                </td>

                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;<td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">
               <asp:Button ID="btn_register" runat="server" Text="Register" OnClick="btn_RegisterClick" OnClientClick="return client_side_validation();" />
         
                </td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
