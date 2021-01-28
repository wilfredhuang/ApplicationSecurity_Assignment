<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="AppSecAssignment_2021.WebForm1" ValidateRequest="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title> My Registration </title>
    <script> 
        function test() {
            //alert("foo")
            var password = document.getElementById('<%= tb_password.ClientID %>')
            var pwd = password.value

            // Reset if password length is zero
            if (pwd.length === 0) {
                document.getElementById("progresslabel").innerHTML = "";
                document.getElementById("progress").value = "0";
                return;
            }

            // Check progress
            var prog = [/[$@$!%*#?&]/, /[A-Z]/, /[0-9]/, /[a-z]/]
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
                    strength = "100% - Password strength is good";
                    progress = "100";
                    break;
            }
            document.getElementById("progresslabel").innerHTML = strength;
            document.getElementById("progress").value = progress;
        }
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
                    <asp:TextBox ID="tb_emailAddress" runat="server"></asp:TextBox>
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
                    <asp:TextBox ID="tb_dob" runat="server" TextMode="Password" CssClass="auto-style3"></asp:TextBox>
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
                    <!-- <asp:Button ID="pwdcheckerBtn" runat="server" OnClick="pwdcheckerBtn_Click" Text="Check Password" /> -->
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
               <asp:Button ID="btn_register" runat="server" OnClick="btn_loginClick" Text="Register" />
         
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
