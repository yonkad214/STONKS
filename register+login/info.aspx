<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="info.aspx.cs" Inherits="register_login.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" />
    <style>
    .tableInfo,td {
    font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
    font-size: 16px;
    padding: 14px,14px,14px,14px;
    margin-top: 1%;
    height: 30px;
    width: 360px;
    background-color: lightgray;
}
    .inputinfo {
    float: right;
    border: hidden;
    background-color: inherit;
    padding: 12px 24px;
    font-size: 16px;
    cursor: pointer;
    display: inline-block;
    border-radius: 5px;
}
        .inputinfo:hover {
        color: red;
        border: 2px solid red;
        background: white;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>פרטי משתמש:</h3>
    <%=table %> 
    <form method="post" action="info.aspx">
<%--    <input name="delete" type="submit" value="מחק" class="inputinfo" style="float:right" onclick="return makesure()"/>
    <input name="update" type="submit" value="עדכן" class="inputinfo" style="float:right"/>--%>
        <%=button1 %>
        <%=button2 %>
    </form>
        <script>
        function makesure() {
            var bool;
            if (confirm("אתה בטוח שתרצה למחוק את המשתמש שלך?")) {
                bool = true;
            } else {
                bool = false;
            }
            return bool;
        }
</script>
</asp:Content>
