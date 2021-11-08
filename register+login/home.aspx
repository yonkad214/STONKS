<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="register_login.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body{
            font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
        }
        .homeHeader {
            padding: 60px;
            text-align: center;
            background: #1abc9c;
            color: white;
            font-size: 30px;
        }
        .contentHome{
            font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
            text-align:right;
            font-size: 40px;
            float:right;
            margin-right: 2%;
        }
        img{
            float: left;
            width: 50%;

        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="homeHeader">
        <h1>STONKS</h1>
    </div>
</asp:Content>
