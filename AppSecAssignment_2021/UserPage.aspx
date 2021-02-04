<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="AppSecAssignment_2021.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 272px
        }
        .auto-style2 {
            width: 231px
        }
        .auto-style3 {
            width: 272px;
            height: 20px;
        }
        .auto-style4 {
            width: 231px;
            height: 20px;
        }
        .auto-style5 {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">

        <table style="width:100%;">
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lbl_userPageTitle" runat="server" Text="User Page"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:Button ID="btn_redirect_changePwd" runat="server" OnClick="btn_redirect_changePwd_Click" Text="Change Password" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3"></td>
                <td class="auto-style4"></td>
                <td class="auto-style5"></td>
            </tr>
        </table>

    </div>
</asp:Content>
