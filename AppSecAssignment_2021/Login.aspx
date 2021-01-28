<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppSecAssignment_2021.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%;">
        <tr>
            <td>
                <asp:Label ID="lbl_loginTitle" runat="server" Text="Login"></asp:Label>
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
                <asp:Label ID="lbl_email" runat="server" Text="Email Address:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tb_email" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:Label ID="lbl_password" runat="server" Text="Password:"></asp:Label>
            </td>
            <td class="auto-style1">
                <asp:TextBox ID="tb_password" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style1"></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btn_login" runat="server" Text="Login" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
