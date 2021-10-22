<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="editInfo.aspx.cs" Inherits="register_login.editInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function gender() {
            switch ("<%= oldGender%>") {
                case "זכר,":
                    document.getElementById("male").checked = true;
                    break;
                case "נקבה,":
                    document.getElementById("female").checked = true;
                    break;
                default:
                    document.getElementById("other").checked = true;
                    document.getElementById("notListedGender").style.visibility = "visible";
                    document.getElementById("notListedGender").value = "<%= oldGender%>".slice(4, -1) + "<%= oldGender%>".slice(-1);
                    break;
            }
        }
        function checkLName() {
            var name = document.getElementById("LN").value;
            if (name.length == 0) {
                document.getElementById("LNErr").innerHTML = "טעות בשם";
                return false;
            }
            for (var i = 0; i < name.length; i++) {
                if (!(name[i] >= 'א' && name[i] <= 'ת' || name[i] == ' ')) {
                    document.getElementById("LNErr").innerHTML = "טעות בשם";
                    return false;
                }
            }
            document.getElementById("LNErr").innerHTML = "";
            return true;
        }//Checks if last name has only letters and spaces
        function checkDate() {
            var d = new Date();
            var n = d.getFullYear();
            var date = document.getElementById("BD").value;
            var date = parseInt(date.substring(0, 4));
            if (date > 1960) {
                document.getElementById("age").innerHTML = n - date;
                document.getElementById("BDErr").innerHTML = "";
                return true;
            }
            document.getElementById("age").innerHTML = "";
            document.getElementById("BDErr").innerHTML = "שדה חובה";
            return false;
        }//checks if the age matchs the date
        function checkFName() {

            var name = document.getElementById("FN").value;
            if (name.length == 0) {
                document.getElementById("FNErr").innerHTML = "טעות בשם";
                return false;
            }
            for (var i = 0; i < name.length; i++) {
                if (!(name[i] >= 'א' && name[i] <= 'ת' || name[i] == ' ')) {
                    document.getElementById("FNErr").innerHTML = "טעות בשם";
                    return false;
                }
            }
            document.getElementById("FNErr").innerHTML = "";
            return true;
        }//Checks if first name has only letters and spaces
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
        function checkEmail() {
            var email = document.getElementById("email").value;
            var mailformat = /^w+([.-]?w+)*@w+([.-]?w+)*(.w{2,3})+$/;
            alert('a');
            if (email.length = 0) {
                document.getElementById("emailErr").innerHTML = "תבנית האימייל היא _@_._";
                return false;
            }
            if (email.value.match(mailformat)) {
                window.alert('b')
                document.getElementById("emailErr").innerHTML = "";
                return true;
            }
            else {
                document.getElementById("emailErr").innerHTML = "תבנית האימייל היא _@_._";
                return false;
            }
        }//checks if email fits in _@_._
        function checkPhone() {
            var phone = document.getElementById("phone").value;
            if (phone.length == 10) {
                document.getElementById("phoneErr").innerHTML = "";
                return true;
            }
            document.getElementById("phoneErr").innerHTML = "מספר טלפון צריך להיות 10 ספרות";
            return false;
        }//checks there are 10 numbers

        function checkEmail() {
            var email = document.getElementById("email").value;
            //Check minimum valid length of an Email.
            if (email.length <= 2) {
                document.getElementById("emailErr").innerHTML = "תבנית האימייל היא _@_._";
                return false;
            }
            //If whether email has @ character.
            if (email.indexOf("@") == -1) {
                document.getElementById("emailErr").innerHTML = "תבנית האימייל היא _@_._";
                return false;
            }

            var parts = email.split("@");
            var dot = parts[1].indexOf(".");
            var len = parts[1].length;
            var dotSplits = parts[1].split(".");
            var dotCount = dotSplits.length - 1;


            //Check whether Dot is present, and that too minimum 1 character after @.
            if (dot == -1 || dot < 2 || dotCount > 2) {
                document.getElementById("emailErr").innerHTML = "תבנית האימייל היא _@_._";
                return false;
            }

            //Check whether Dot is not the last character and dots are not repeated.
            for (var i = 0; i < dotSplits.length; i++) {
                if (dotSplits[i].length == 0) {
                    document.getElementById("emailErr").innerHTML = "תבנית האימייל היא _@_._";
                    return false;
                }
            }
            document.getElementById("emailErr").innerHTML = "";
            return true;
        };

        function check() {
            var y = checkDate();
            var x = checkLName();
            var z = checkFName();
            var a = checkPassword();
            var b = checkUsername();
            var d = checkPhone();
            var e = checkEmail
            if (x && y && z && a && b && d && c) {
                return true;
            }
            return false;
        }
        function checkLogin() {
            x = checkUsername();
            y = checkPassword();
            if (x && y) {
                return true;
            }
            return false;
        }
        gender()
    </script>
        <form method="post" action="editInfo.aspx">
        <h3 style="padding-right:45.5%">עדכון</h3>
        <table class="tables">
            <tr>
                <td>שם פרטי</td>
                <td><input type="text" id="FN" name="FN" onchange="checkFName()" value ="<%=oldFirstName %>"/></td>
                <td style="color:red" id="FNErr"></td>
            </tr>
            <tr>
                <td>שם משפחה</td>
                <td><input type="text" , id="LN" name="LN", onchange="checkLName()", value ="<%=oldLastName %>"/></td>
                <td style="color:red" id="LNErr"></td>
            </tr>
            <tr>
                <td>תאריך לידה</td>
                <td><input type="date" , id="BD" name="BD", onchange="checkDate()", value ="<%=oldDob %>"/></td>
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
                <td><input type="text" , id="address" name="address", value ="<%=oldAddress %>"/></td>
                <td style="color: red" id="addressErr"></td>
            </tr>
            <tr>
                <td>אימייל</td>
                <td><input type="text" , id="email" name="email", onchange="checkEmail()", value ="<%=oldEmail %>"/></td>
                <td style="color:red" id="emailErr"></td>
            </tr>
            <tr>
                <td>מספר טלפון</td>
                <td><input type="text" , id="phone" name="phone", onchange="checkPhone()", value ="<%=oldPhone %>"/></td>
                <td style="color:red" id="phoneErr"></td>
            </tr>
            <tr>
                <td>שם משתמש</td>
                <td><input type="text" , id="username" name="username", onchange="checkUsername()", value ="<%=oldUsername %>", disabled="disabled" /></td>
                <td style="color:red" id="usernameErr"></td>
            </tr>
            <tr>
                <td>סיסמה</td>
                <td><input type="password" , id="password" name="password", onchange="checkPassword()" /></td>
                <td style="color:red" id="passwordErr"></td>
            </tr>
        </table>
        <input name="updateReal" type="submit" value="עדכן" class="input" onclick="return check()" />
    </form>
    <script src="JavaScript.js"></script>
</asp:Content>
