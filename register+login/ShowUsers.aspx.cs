using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace register_login
{
    public partial class ShowUsers : System.Web.UI.Page
    {
        protected string Table = "";
        protected string filterWhere = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"].ToString() == "True")
            {
                
                string cmdStr = string.Format("SELECT * FROM users");
                bool continueFlag = true;
                if (Request.Form["filterSubmit"] != null) {
                    string value1 = Request.Form["value1"].ToString();
                    string value2 = Request.Form["value2"].ToString();
                    string filter1 = Request.Form["filter1"].ToString();
                    string filter2 = Request.Form["filter2"].ToString();

                    if (value1 == "" && value1 == "")
                    {

                    }
                    else if (value1 != "" && value2 != "")
                    {
                        if (filter1 != filter2)
                        {
                            cmdStr += " WHERE (" + filter1 + " = N'" + value1 + "')";
                            cmdStr += " AND (" + filter2 + " = N'" + value2 + "')";
                        }
                        else
                        {
                            Table = "<table border='1' id=\"tbl1\" name=\"tbl1\"><span style='color:red; font-weight:bold'>על שני החתכים להיות שונים</span>";
                            continueFlag = false;
                        }
                    }
                    else if (value1 != "" && value2 == "")
                    {
                        cmdStr += " WHERE (" + filter1 + " = N'" + value1 + "')";
                    }
                    // נבחר התנאי השני
                    else if (value1 == "" && value2 != "")
                    {
                        cmdStr += " WHERE (" + filter2 + " = N'" + value2 + "')";
                    }
                    
                    //cmdStr = string.Format("SELECT * FROM Users WHERE {0} = \'{1}\';", filter1, value1);
                }
                if (Request.Form["deleteSubmit"] != null && Request.Form["demo"] != null)
                {
                    string sqlStr = string.Format(Request.Form["demo"]);
                    string ConnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
                    SqlConnection SqlConn = new SqlConnection(ConnStr);
                    
                    SqlCommand SQLCommand = new SqlCommand(sqlStr, SqlConn);
                    SqlConn.Open();
                    SQLCommand.ExecuteNonQuery();
                    SqlConn.Close();

                }

                Table += "<table id = \"tbl1\" class = \"allUsers\"><tr><th class=\"cell\">שם משתמש</th><th class=\"cell\">סיסמה</th><th class=\"cell\">שם פרטי</th><th class=\"cell\">שם משפחה</th><th class=\"cell\">כתובת אימייל</th><th class=\"cell\">מספר טלפון</th><th class=\"cell\">כתובת</th><th class=\"cell\">מין</th><th class=\"cell\">תאריך לידה</th><th class=\"cell\">גיל</th><th class=\"cell\">מחיקה</th></tr>";

                // connection string
                string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
                if (continueFlag) {
                    SqlConnection SqlConn = new SqlConnection(connStr);
                    SqlConn.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdStr, connStr);
                    DataSet Data = new DataSet();
                    dataAdapter.Fill(Data);
                    int i = 1;
                    foreach (DataRow row in Data.Tables[0].Rows)
                    {
                        string Rusername = (string)row["username"];
                        string Rpassword = (string)row["password"];
                        string RfirstName = (string)row["Fname"];
                        string RlastName = (string)row["Lname"];
                        string Remail = (string)row["email"];
                        string Rphone = row["phone"].ToString();
                        if (Rphone == "0")
                            Rphone = "";
                        string Raddress = (string)row["address"];
                        string RGender = (string)row["gender"];
                        string Rdob = row["Bdate"].ToString();
                        string Rage = (string)row["age"].ToString();
                        string tempRow = string.Format("<tr><td id=\"user" + i + "\" name=\"user" + i + "\" class=\"cell\">{0}</td><td class=\"cell\">{1}</td><td class=\"cell\">{2}</td><td class=\"cell\">{3}</td><td class=\"cell\">{4}</td><td class=\"cell\">{5}</td><td class=\"cell\">{6}</td><td class=\"cell\">{7}</td><td class=\"cell\">{8}</td><td class=\"cell\">{9}</td><td><input class=\"cell\" type =\"checkbox\" name =\"chk" + i + "\" id =\"chk" + i + "\" </td></tr>", Rusername, Rpassword, RfirstName, RlastName, Remail, Rphone, Raddress, RGender, Rdob, Rage);
                        Table += tempRow;
                        i++;
                    }
                }
                Table += "</table>";
                //Response.Redirect("ShowUsers.aspx");
            }
            else
            {
                Session["Err"] = "קרתה שגיאה נסה להכנס מחדש";
                Response.Redirect("Error.aspx");
            }
        }
    }
}