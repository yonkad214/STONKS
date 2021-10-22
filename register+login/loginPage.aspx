<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="loginPage.aspx.cs" Inherits="register_login.loginPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
    <form action="loginPage.aspx", method="POST">
        <h3 style="padding-right:45.5%;padding-top:10%;">כניסה</h3>
        <table class="tables" >
            <tr>
                <td>שם משתמש</td>
                <td><input type="text" , id="username" name="username" onchange="checkUsername"/></td>
                <td style="color:red" id="usernameErr"></td>
            </tr>
            <tr>
                <td>סיסמה</td>
                <td><input type="password" , id="password" name="password" onchange="checkPassword()"/></td>
                <td style="color:red" id="passwordErr"></td>
            </tr>
        </table>
        <input id="submitlogin" name="submitlogin" type="submit" value="כניסה" class="input" onclick="return checkLogin();" />

    </form>
    <%-- added script cause it wasn't updating --%>
    <script >
        function checkUsername() {
            var username = document.getElementById("username").value;
            if (username.length == 0) {
                document.getElementById("usernameErr").innerHTML = "שם משתמש לא מתאים";
                return false;
            }
            for (var i = 0; i < username.length; i++) {
                var char = username.substring(i, i + 1);
                if (!("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_1234567890".includes(char))) {
                    document.getElementById("usernameErr").innerHTML = "שם משתמש לא מתאים";
                    return false;
                }
            }
            document.getElementById("usernameErr").innerHTML = "";
            return true;
        }//checks if the username only contains letters numbers and "_"
        function checkPassword() {
            var low = 0;
            var upper = 0;
            var pass = document.getElementById("password").value;
            if (!(6 <= pass.length <= 10) || pass.length == 0) {
                document.getElementById("passwordErr").innerHTML = "אורך סיסמה צריך להיות בין 6 ל-10 ספרות";
                return false;
            }
            for (var i = 0; i < pass.length; i++) {
                if (!"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_1234567890".includes(pass.substring(i, i + 1))) {
                    document.getElementById("passwordErr").innerHTML = "מותר רק ספרות אותיות וקו תחתון";
                    return false;
                }
                if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".includes(pass.substring(i, i + 1))) {
                    if (pass.substring(i, i + 1).toLowerCase() == pass.substring(i, i + 1)) {
                        low += 1;
                    }
                    else {
                        upper += 1;
                    }
                }
            }
            if (low == 0 || upper == 0) {
                document.getElementById("passwordErr").innerHTML = "צריך אות גדולה ואות קטנה";
                return false;
            }
            document.getElementById("passwordErr").innerHTML = "";
            return true;
        }//checks if 6<=password.length<=10 and only numbers letters and "_" and checks if one letter is uppercase and one lowercase
        function checkLogin() {
            x = checkUsername();
            y = checkPassword();
            if (x && y) {
                return true;
            }
            return false;
        }
    </script>
</asp:Content>
