<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="registration.aspx.cs" Inherits="register_login.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form method="post" action="registration.aspx">
        <h3 style="padding-right:45.5%">הרשמה</h3>
        <table class="tables">
            <tr>
                <td>שם פרטי</td>
                <td><input type="text", id="FN", name="FN", onchange="checkFName()"/></td>
                <td style="color:red" id="FNErr"></td>
            </tr>
            <tr>
                <td>שם משפחה</td>
                <td><input type="text" , id="LN" name="LN", onchange="checkLName()"/></td>
                <td style="color:red" id="LNErr"></td>
            </tr>
            <tr>
                <td>תאריך לידה</td>
                <td><input type="date" , id="BD" name="BD", onchange="checkDate()"/></td>
                <td style="color: red" id="BDErr"></td>
            </tr>
            <tr>
                <td>גיל</td>
                <td id="age"></td>
            </tr>
            <tr>
                <td>מין</td>
                <td>               
                    <input type="radio" id="male" name="gender" value="זכר"/>
                    <label for="male">זכר</label>
                    <input type="radio" id="female" name="gender" value="נקבה"/>
                    <label for="female">נקבה</label>
                    <input type="radio" id="other" name="gender" value="אחר"/>
                    <label for="other">אחר</label>
                </td>
            </tr>
            <tr>
                <td>כתובת</td>
                <td><input type="text" , id="address" name="address"/></td>
                <td style="color: red" id="addressErr"></td>
            </tr>
            <tr>
                <td>אימייל</td>
                <td><input type="text" , id="email" name="email", onchange="checkEmail()"/></td>
                <td style="color:red" id="emailErr"></td>
            </tr>
            <tr>
                <td>מספר טלפון</td>
                <td><input type="text" , id="phone" name="phone", onchange="checkPhone()"/></td>
                <td style="color:red" id="phoneErr"></td>
            </tr>
            <tr>
                <td>שם משתמש</td>
                <td><input type="text" , id="username" name="username", onchange="checkUsername()" /></td>
                <td style="color:red" id="usernameErr"></td>
            </tr>
            <tr>
                <td>סיסמה</td>
                <td><input type="password" , id="password" name="password", onchange="checkPassword()" /></td>
                <td style="color:red" id="passwordErr"></td>
            </tr>
        </table>
        <input name="register" type="submit" value="הרשמה" class="input" onclick="return check()" />
    </form>
    <script src="JavaScript.js"></script>
</asp:Content>
