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
                    document.getElementById("lbl_pwdchecker").innerText = "Very Weak";
                    document.getElementById("lbl_pwdchecker").style.color = "red";
                    document.getElementById("lbl_pwdchecker").style.display = "inline";
                    setTimeout(function () {
                        document.getElementById("lbl_pwdchecker").style.display = "none";
                    }, 5000)

                    break;
                case 3:
                    strength = "50%";
                    progress = "50";
                    document.getElementById("lbl_pwdchecker").innerText = "Weak";
                    document.getElementById("lbl_pwdchecker").style.color = "red";
                    document.getElementById("lbl_pwdchecker").style.display = "inline";
                    setTimeout(function () {
                        document.getElementById("lbl_pwdchecker").style.display = "none";
                    }, 5000)
                    break;
                case 4:
                    strength = "75%";
                    progress = "75";
                    document.getElementById("lbl_pwdchecker").innerText = "Insufficient Strength";
                    document.getElementById("lbl_pwdchecker").style.color = "red";
                    document.getElementById("lbl_pwdchecker").style.display = "inline";
                    setTimeout(function () {
                        document.getElementById("lbl_pwdchecker").style.display = "none";
                    }, 5000)
                    //document.getElementById('progress').style.backgroundColor = 'yellow';
                    break;
                case 5:
                    strength = "100% - Eligble for registration";
                    progress = "100";
                    document.getElementById("lbl_pwdchecker").style.display = "none";
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

            var check = true;



            var lettersOnlyRegex = /^[A-Za-z]+$/;
            if (lettersOnlyRegex.test(firstName) == 0) {
                document.getElementById("lbl_fnFeedback").innerText = "Invalid input found! Only alphabets allowed"
                document.getElementById("lbl_fnFeedback").style.color = "red";
                document.getElementById("lbl_fnFeedback").style.display = "inline";
                check = false;
            }

            if (lettersOnlyRegex.test(lastName) == 0) {
                document.getElementById("lbl_lnFeedback").innerText = "Invalid input found! Only alphabets allowed"
                document.getElementById("lbl_lnFeedback").style.color = "red";
                document.getElementById("lbl_lnFeedback").style.display = "inline";
                check = false;
            }


            if (firstName.length == 0) {
                document.getElementById("lbl_fnFeedback").innerText = "No input found!"
                document.getElementById("lbl_fnFeedback").style.color = "red";
                document.getElementById("lbl_fnFeedback").style.display = "inline";
                check = false;
            }

            if (lastName.length == 0) {
                document.getElementById("lbl_lnFeedback").innerText = "No input found!"
                document.getElementById("lbl_lnFeedback").style.color = "red";
                document.getElementById("lbl_lnFeedback").style.display = "inline";
                check = false;
            }


            if (emailAddress.length == 0) {
                document.getElementById("lbl_emailFeedback").innerText = "No input found!"
                document.getElementById("lbl_emailFeedback").style.color = "red";
                document.getElementById("lbl_emailFeedback").style.display = "inline";
                check = false;
            }

            if (prog < 5) {
                document.getElementById("lbl_pwdFeedback").innerText = "Password requires at least 8 characters, with a combination of uppercase, lowercase, digits & symbols"
                document.getElementById("lbl_pwdFeedback").style.color = "red";
                document.getElementById("lbl_pwdFeedback").style.display = "inline";
                check = false;
            }


            if (dob == "") {
                document.getElementById("lbl_dobFeedback").innerText = "Please choose a date"
                document.getElementById("lbl_dobFeedback").style.color = "red";
                document.getElementById("lbl_dobFeedback").style.display = "inline";
                check = false;
            }

            if (creditCardInfo.length != 16) {
                document.getElementById("lbl_cciFeedback").innerText = "Only 16 digits are allowed"
                document.getElementById("lbl_cciFeedback").style.color = "red";
                document.getElementById("lbl_cciFeedback").style.display = "inline";
                check = false;
            }


            if (/^\d+$/.test(creditCardInfo) == false) {
                document.getElementById("lbl_cciFeedback").innerText = "Only 16 digits are allowed"
                document.getElementById("lbl_cciFeedback").style.color = "red";
                document.getElementById("lbl_cciFeedback").style.display = "inline";
                check = false;
            }

            return check;

        }


        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lbl_pwdchecker.ClientID %>").style.display = "none";
        }, seconds * 1000);
        };
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <table class="auto-style1">
            <tr>
                <td class="auto-style7">
                    <asp:Label ID="Label3" runat="server" Text="Registration"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8"></td>
                <td class="auto-style2"></td>
                <td class="auto-style3"></td>
                <td class="auto-style5"></td>
                <td class="auto-style3">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">
                    <asp:Label ID="lbl_firstName" runat="server" Text="First Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_firstName" runat="server"></asp:TextBox>
                    </td>
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style6">
                    <asp:Label ID="lbl_fnFeedback" runat="server"></asp:Label>
                                                        </td>
                <td class="auto-style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">
                    <asp:Label ID="lbl_firstName0" runat="server" Text="Last Name"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="tb_lastName" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style6">
                    <asp:Label ID="lbl_lnFeedback" runat="server"></asp:Label>
                                                        </td>
                <td class="auto-style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">
                    <asp:Label ID="lbl_emailAddress" runat="server" Text="Email address"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="tb_emailAddress" runat="server" TextMode="Email"></asp:TextBox>
                </td>
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style6">
                    <asp:Label ID="lbl_emailFeedback" runat="server"></asp:Label>
                                                        </td>
                <td class="auto-style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lbl_password" runat="server" Text="Password"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="tb_password" runat="server" TextMode="Password" CssClass="auto-style3"></asp:TextBox>
                </td>
                <td class="auto-style1">
                    <asp:Label ID="lbl_pwdchecker" runat="server"></asp:Label>
                                                        <progress id="progress" value="0" max="100">70</progress>
    <span id="progresslabel"></span>
                </td>
                <td class="auto-style1">
                    </td>
                <td class="auto-style1">
                    <asp:Label ID="lbl_pwdFeedback" runat="server"></asp:Label>
                                                        </td>
            </tr>
            <tr>
                <td class="auto-style8">
                    &nbsp;</td>
                <td class="auto-style2">
                                &nbsp;</td>
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style6">
                    &nbsp;</td>
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">
                                <asp:Label ID="lbl_dob" runat="server" Text="Date of Birth"></asp:Label>
                    </td>
                <td class="auto-style2">
                    <asp:TextBox ID="tb_dob" runat="server" placeholder="Date of Birth" type="date" min="1903-01-02" max="2004-02-05" ></asp:TextBox>
                </td>
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style6">
                    <asp:Label ID="lbl_dobFeedback" runat="server"></asp:Label>
                                                        </td>
                <td class="auto-style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">
                    <asp:Label ID="lbl_creditCardInfo" runat="server" Text="Credit Card Info  "></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="tb_creditCardInfo" runat="server" CssClass="auto-style4"></asp:TextBox>
                </td>

                <td class="auto-style1">
                    &nbsp;</td>

                <td class="auto-style6">
                    <asp:Label ID="lbl_cciFeedback" runat="server"></asp:Label>
                                                        </td>

                <td class="auto-style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;<td class="auto-style1">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">
                    </td>
                <td class="auto-style2">
               <asp:Button ID="btn_register" runat="server" Text="Register" OnClick="btn_RegisterClick" OnClientClick="return client_side_validation();" />
         
                </td>
                <td class="auto-style3">
                                                        </td>
                <td class="auto-style5">
                    <asp:Label ID="lbl_alertmsg" runat="server"></asp:Label>
                                                        </td>
                <td class="auto-style3">
                    </td>
            </tr>
            <tr>
                <td class="auto-style8">
                    </td>
                <td class="auto-style2">
                    </td>
                <td class="auto-style3"></td>
                <td class="auto-style5"></td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td class="auto-style8">
                    &nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
