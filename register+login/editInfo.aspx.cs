using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace register_login
{
    public partial class editInfo : System.Web.UI.Page
    {
        public string oldUsername = "";
        public string oldPassword = "";
        public string oldFirstName = "";
        public string oldLastName = "";
        public string oldEmail = "";
        public string oldPhone = "";
        public string oldAddress = "";
        public string oldGender = "";
        public string oldDob = "";
        public string oldAge = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usernameData"] == null)
            {
                Session["err"] = "שגיאה, שם המשתמש לא נמצא";
                Response.Redirect("Error.aspx");
            }
            if (Session["Admin"].ToString() != "True")
            {
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

                oldUsername = (string)Data.Tables[0].Rows[0]["username"];
                System.Diagnostics.Debug.WriteLine(oldUsername);
                oldPassword = (string)Data.Tables[0].Rows[0]["password"];
                oldFirstName = (string)Data.Tables[0].Rows[0]["Fname"];
                oldLastName = (string)Data.Tables[0].Rows[0]["Lname"];
                oldEmail = (string)Data.Tables[0].Rows[0]["email"];
                oldPhone = (string)Data.Tables[0].Rows[0]["phone"];
                oldAddress = (string)Data.Tables[0].Rows[0]["address"];
                oldGender = (string)Data.Tables[0].Rows[0]["gender"];
                oldDob = (string)Data.Tables[0].Rows[0]["Bdate"];
                oldAge = (string)Data.Tables[0].Rows[0]["age"];
                SqlConn.Close();
            }
            else
            {
                DataSet xmlSet = new DataSet();
                xmlSet.ReadXml(System.Web.HttpContext.Current.Server.MapPath("admins.xml"));
                foreach (DataRow row in xmlSet.Tables[0].Rows)
                {
                    if (Session["usernameData"].ToString() == row["username"].ToString())
                    {
                        oldUsername = (string)row["username"];
                        oldPassword = (string)row["password"];
                        oldFirstName = (string)row["firstName"];
                        oldLastName = (string)row["lastName"];
                        oldEmail = (string)row["email"];
                        oldPhone = row["phone"].ToString();
                        oldAddress = (string)row["address"];
                        oldGender = (string)row["Gender"];
                        oldDob = row["dob"].ToString();
                        oldAge = (string)row["age"].ToString();

                    }
                }
            }
            if (Request.Form["updateReal"] != null)
            {
                string phone = Request.Form["phone"];
                string gender = Request.Form["gender"];
                string address = Request.Form["address"];
                string firstName = Request.Form["FN"];
                string lastName = Request.Form["LN"];
                string birthDate = Request.Form["BD"];
                string username = oldUsername;
                string password = HashLib.GetHashString(Request.Form["password"]);
                string email = Request.Form["email"];
                //calculates the age
                DateTime now = DateTime.Now;
                string age = (now.Year - int.Parse(birthDate.Substring(0, 4))).ToString();

                // static connectionStr from database properties
                string SqlConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";

                //sets query to check email
                string SqLQueryEmail = string.Format("SELECT * FROM Users WHERE (email = N'{0}')", email);

                //sets query to check username
                string SqLQueryUsername = string.Format("SELECT * FROM Users WHERE (username = N'{0}')", username);

                //query to update user info
                string sqlq = string.Format("UPDATE Users SET username = N'{0}', password = N'{1}', Fname = N'{2}', Lname = N'{3}', email = N'{4}', phone = N'{5}', address = N'{6}', gender = N'{7}', Bdate = N'{8}', age = N'{9}' WHERE username = N'{10}'", username, password, firstName, lastName, email, phone, address, gender, birthDate, age, username);

                //connects to the databae with the given path
                SqlConnection SQLConnection = new SqlConnection(SqlConnectionString);

                //sets command that will run in sql to check email
                SqlCommand SQLCommandEmail = new SqlCommand(SqLQueryEmail, SQLConnection);
                SQLConnection.Open();//opens connection to database
                SqlDataReader emailExists = SQLCommandEmail.ExecuteReader();//executes sql command and saves output
                if (!(oldEmail == email))
                {
                    if (emailExists.HasRows)
                    {
                        Session["Err"] = "כתובת האימייל הזאת כבר קיימת במערכת נסה להרשם מחדש";
                        Response.Redirect("Error.aspx");
                    }
                }
                SQLConnection.Close();//closes connection to database

                if (Session["Admin"].ToString() != "True")
                {
                    //sets command that will run in sql to insert into table
                    SqlCommand SQLCommandInsert = new SqlCommand(sqlq, SQLConnection);
                    SQLConnection.Open(); //opens connection to database
                    int affect = SQLCommandInsert.ExecuteNonQuery(); //executes sql command and returns rows affected
                    SQLConnection.Close(); //closes connection to database
                    if (affect == 1) //checks if it worked and if it did redirects to info.aspx
                        Response.Redirect("info.aspx");
                    else
                    {
                        Session["Err"] = "הייתה שגיאה נסה לעדכן מחדש";
                        Response.Redirect("Error.aspx");
                    }
                }
                else
                {
                    var xmlFile = XDocument.Load(MapPath("admins.xml"));
                    var userNode = xmlFile.Descendants("adminUser").FirstOrDefault(adminUser => adminUser.Element("username").Value == username);
                    userNode.SetElementValue("password", password);
                    userNode.SetElementValue("firstName", firstName);
                    userNode.SetElementValue("lastName", lastName);
                    userNode.SetElementValue("email", email);
                    userNode.SetElementValue("phone", phone);
                    userNode.SetElementValue("address", address);
                    userNode.SetElementValue("Gender", gender);
                    userNode.SetElementValue("dob", birthDate);
                    userNode.SetElementValue("age", age);
                    xmlFile.Save(MapPath("admins.xml"));
                    Response.Redirect("Home.aspx");
                }
            }
        }
    }
}