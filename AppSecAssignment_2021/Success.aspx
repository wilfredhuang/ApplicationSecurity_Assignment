<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="AppSecAssignment_2021.Success" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://www.google.com/recaptcha/api.js?render=6Ld0mEkaAAAAALzsXZ0s4biheu5L4ENYCR7bzFiw"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">

        <asp:Label ID="lbl_registerSuccess" runat="server" Text="You have  successfully registered"></asp:Label>

    </div>
</asp:Content>
