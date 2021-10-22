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
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected string button1 = "";
        protected string button2 = "";
        protected string table = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usernameData"] == null)
            {
                Session["err"] = "שגיאה, שם המשתמש לא נמצא";
                Response.Redirect("Error.aspx");
            }
            if (Session["Admin"].ToString() == "True")
            {
                button1 = "<input type=\"submit\" value=\"פרטי כל המשתמשים\" name=\"showAllUsers\" class=\"inputinfo\"/>";
                button2 = "<input name=\"update\" type=\"submit\" value=\"עדכן\" class=\"inputinfo\" style=\"float:right\"/>";
                DataSet data = new DataSet();
                data.ReadXml(MapPath("admins.xml"));
                // thought about using a for loop as showen in DbExample but
                // I wanted to do it seperatily to have all of the values seperated with names
                // The R at the start stands for received
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    if (row["username"].ToString() == Session["usernameData"].ToString())
                    {
                        string Rusername = (string)row["username"];
                        string Rpassword = (string)row["password"];
                        string RfirstName = (string)row["firstName"];
                        string RlastName = (string)row["lastName"];
                        string Remail = (string)row["email"];
                        string Rphone = row["phone"].ToString();
                        if (Rphone == "0")
                            Rphone = "";
                        string Raddress = (string)row["address"];
                        string RGender = (string)row["Gender"];
                        string Rdob = row["dob"].ToString();
                        string Rage = (string)row["age"].ToString();
                        table = string.Format("<table class=\"tableInfo\"><tr><td>שם משתמש:</td><td>{0}</td></tr><tr><td>סיסמה:</td><td>{1}</td></tr><tr><td>שם פרטי:</td><td>{2}</td></tr><tr><td>שם משפחה:</td><td>{3}</td></tr><tr><td>אימייל:</td><td>{4}</td></tr><tr><td>טלפון:</td><td>{5}</td></tr><tr><td>כתובת:</td><td>{6}</td></tr><tr><td>מין:</td><td>{7}</td></tr><tr><td>תאריך לידה:</td><td>{8}</td></tr><tr><td>גיל:</td><td>{9}</td></tr></table>", Rusername, Rpassword, RfirstName, RlastName, Remail, Rphone, Raddress, RGender, Rdob, Rage);
                    }
                }
                if (Request.Form["showAllUsers"] != null)
                {
                    Response.Redirect("ShowUsers.aspx");
                }
                if (Request.Form["update"] != null)
                {
                    Response.Redirect("editInfo.aspx");
                }
            }
            else
            {
                button1 = "<input name=\"delete\" type=\"submit\" value=\"מחק\" class=\"inputinfo\" style=\"float:right\" onclick=\"return makesure()\"/>";
                button2 = "<input name=\"update\" type=\"submit\" value=\"עדכן\" class=\"inputinfo\" style=\"float:right\"/>";
                // connection string
                string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
                //sets connection
                SqlConnection SqlConn = new SqlConnection(connStr);

                string queryStr = string.Format("SELECT * FROM users WHERE (username = N'{0}')", Session["usernameData"]);
                SqlCommand SqlCmd = new SqlCommand(queryStr, SqlConn);
                SqlConn.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(queryStr, connStr);
                DataSet Data = new DataSet();
                dataAdapter.Fill(Data);


                string username = (string)Data.Tables[0].Rows[0]["username"];
                string password = (string)Data.Tables[0].Rows[0]["password"];
                string firstName = (string)Data.Tables[0].Rows[0]["Fname"];
                string lastName = (string)Data.Tables[0].Rows[0]["Lname"];
                string email = (string)Data.Tables[0].Rows[0]["email"];
                string phone = (string)Data.Tables[0].Rows[0]["phone"];
                string address = (string)Data.Tables[0].Rows[0]["address"];
                string Gender = (string)Data.Tables[0].Rows[0]["gender"];
                string dob = (string)Data.Tables[0].Rows[0]["Bdate"];
                string age = (string)Data.Tables[0].Rows[0]["age"];
                SqlConn.Close();
                table = string.Format("<table class=\"tableInfo\"><tr><td>שם משתמש:</td><td>{0}</td></tr><tr><td>סיסמה:</td><td>{1}</td></tr><tr><td>שם פרטי:</td><td>{2}</td></tr><tr><td>שם משפחה:</td><td>{3}</td></tr><tr><td>אימייל:</td><td>{4}</td></tr><tr><td>טלפון:</td><td>{5}</td></tr><tr><td>כתובת:</td><td>{6}</td></tr><tr><td>מין:</td><td>{7}</td></tr><tr><td>תאריך לידה:</td><td>{8}</td></tr><tr><td>גיל:</td><td>{9}</td></tr></table>", username, password, firstName, lastName, email, phone, address, Gender, dob, age);
                if (Request.Form["delete"] != null)
                {
                    string deleteQuery = string.Format("DELETE FROM Users WHERE username = N'{0}'", Session["usernameData"]);
                    SqlCommand sqlDelete = new SqlCommand(deleteQuery, SqlConn);
                    SqlConn.Open();
                    int deleteLines = sqlDelete.ExecuteNonQuery();
                    System.Diagnostics.Debug.WriteLine(deleteLines);
                    SqlConn.Close();
                    if (deleteLines != 0)
                    {
                        Response.Redirect("registration.aspx");
                    }
                }
                if (Request.Form["update"] != null)
                {
                    Response.Redirect("editInfo.aspx");
                }

            }
        }
    }
}