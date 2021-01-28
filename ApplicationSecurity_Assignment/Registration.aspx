<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="WebForm1.aspx.vb" Inherits="ApplicationSecurity_Assignment.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>
        Registration Page
    </title>
    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tb_password.ClientID%>').value;

            if (str.length < 8) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password Length Must be at least 8 characters";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("too_short");
            }

            // search for numbers 0-9 in the str
            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 number";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_number");
            }


            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Need at least 1 uppercase letter";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_uppercase");
            }

            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Need at least 1 lowercase letter";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_lowercase");
            }

            else if (str.search(/[^a-zA-Z0-9 ]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Need at least 1 special character";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_special_char");
            }

            
            

            document.getElementById("lbl_pwdchecker").innerHTML = "Excellent!";
            document.getElementById("lbl_pwdchecker").style.color = "Blue";
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
                    <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_password" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                <td>
                    <asp:Label ID="lbl_pwdchecker" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="pwdcheckerBtn" runat="server" OnClick="pwdcheckerBtn_Click" Text="Check Password" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
