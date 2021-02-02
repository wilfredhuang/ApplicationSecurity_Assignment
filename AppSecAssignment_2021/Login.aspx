<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppSecAssignment_2021.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
    .auto-style3 {
        height: 26px;
        width: 206px;
    }
    .auto-style4 {
        width: 206px
    }
    .auto-style5 {
        width: 147px
    }
    .auto-style6 {
        height: 26px;
        width: 147px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <table style="width:100%;">
        <tr>
            <td class="auto-style5">
                <asp:Label ID="lbl_loginTitle" runat="server" Text="Login"></asp:Label>
            </td>
            <td class="auto-style4">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
            <td class="auto-style4">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5">
                <asp:Label ID="lbl_email" runat="server" Text="Email Address:"></asp:Label>
            </td>
            <td class="auto-style4">
                <asp:TextBox ID="tb_email" runat="server" TextMode="Email"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
            <td class="auto-style4">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style6">
                <asp:Label ID="lbl_password" runat="server" Text="Password:"></asp:Label>
            </td>
            <td class="auto-style3">
                <asp:TextBox ID="tb_password" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td class="auto-style1">
                <asp:Label ID="lbl_loginErrMsg" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
            <td class="auto-style4">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
            <td class="auto-style4">
                <asp:Button ID="btn_login" runat="server" Text="Login" OnClick="btn_login_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
        </div>
</asp:Content>
