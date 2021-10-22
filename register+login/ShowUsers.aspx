<%@ Page Title="" Language="C#" MasterPageFile="~/Page.Master" AutoEventWireup="true" CodeBehind="ShowUsers.aspx.cs" Inherits="register_login.ShowUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table{
            border-collapse: collapse;
            width: 100%;
            text-align:right;
            font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
        }

        th, td {
            padding: 8px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        tr:hover {
            background-color: #f5f5f5;
        }
        .cell{
            text-align: right;
        }
        #filter{
            width: 33%;
            border: hidden;
        }
        .tbrow:hover{
            background-color:white;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form name="filterForm" action="ShowUsers.aspx" method="post">
        <table id="filter">
            <tr>
                <td>סנן 1:</td>
                <td> 
                    <select name="filter1">
                        <%--<option value="select">בחר</option>--%> 
                        <option value="Fname">שם פרטי</option>
                        <option value="Lname">שם משפחה</option>
                        <option value="phone">מספר טלפון</option>
                        <option value="email">אמייל</option>
                        <option value="username">שם משתמש</option>
                        <option value="password">סיסמה</option>
                        <option value="gender">מין</option>
                        <option value="address">כתובת</option>
                        <option value="age">גיל</option>
                    </select>
                </td>
                <td>
                    <input type="text" name="value1" id="value1" />
                </td>
            </tr>
            <tr>
                <td>סנן 2:</td>
                <td> 
                    <select name="filter2">
                        <%--<option value="select">בחר</option>--%> 
                        <option value="Fname">שם פרטי</option>
                        <option value="Lname">שם משפחה</option>
                        <option value="phone">מספר טלפון</option>
                        <option value="email">אמייל</option>
                        <option value="username">שם משתמש</option>
                        <option value="password">סיסמה</option>
                        <option value="gender">מין</option>
                        <option value="address">כתובת</option>
                        <option value="age">גיל</option>
                    </select>
                </td>
                <td>
                    <input type="text" name="value2" id="value2" />
                </td>
            </tr>
            <tr class="tbrow">
                <td class="tbrow"><input class="input" value="סנן משתמשים" type="submit" name="filterSubmit"></td>
                <td class="tbrow"><input class="input" onclick="Navigate()" value="מחק משתמשים" type="submit" name="deleteSubmit"></td>
            </tr>
        </table>
        <div runat="server">
                <%=Table %>
        </div>
        <p>
        <input type="text" id="delthing" name="demo" size="20" style="visibility: hidden" />
        </p>
        </form>
    <script type="text/javascript">
    
        function Navigate() {
        
        var length = document.getElementById("tbl1").rows.length;
        var chkStr = "chk";
        var temp = "";
        var flag = true;
        var users = [];
        var counter = 0;

        for (var i = 1; i < length; i++) {
            temp = chkStr + i;
            flag = document.getElementById(temp).checked;
            if (flag) {
                users[counter] = document.getElementById("user"+i).innerHTML;
                counter++;
            }
        }
        sqlStr = "DELETE FROM Users WHERE (";
        for (var i = 0; i < counter; i++) {
            sqlStr += "username=N'" + users[i] + "'";
            if (i < counter - 1) {
                sqlStr += " OR ";
            }
            }
            sqlStr+=")"
        // על הדף demo התנאי למחיקה נשמר עך אלמנט בשם 
        // invisible אלמנט שהוא 
        
        document.getElementById("delthing").value = sqlStr;
        return true;
    }

    </script>
</asp:Content>
