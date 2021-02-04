<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PasswordExpired.aspx.cs" Inherits="AppSecAssignment_2021.WebForm8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">

        <asp:Label ID="lbl_pwdExpireNotify" runat="server" Text="Your Password has expired please change it"></asp:Label>
        <asp:Button ID="btn_redirectChgPwd" runat="server" OnClick="btn_redirectChgPwd_Click" Text="Change Password" />

    </div>
</asp:Content>
