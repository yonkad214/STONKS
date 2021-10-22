using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// imports for sql
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;



namespace register_login
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //iji
            if (Request.Form["register"] != null)
            { //checks if form was submited
                //requests all the values from the form submition
                string phone = Request.Form["phone"];
                string gender = Request.Form["gender"];
                string address = Request.Form["address"];
                string firstName = Request.Form["FN"];
                string lastName = Request.Form["LN"];
                string birthDate = Request.Form["BD"];
                string username = Request.Form["username"];
                string password = Request.Form["password"];
                string email = Request.Form["email"];
                //calculates the age
                DateTime now = DateTime.Now;
                string age = (now.Year - int.Parse(birthDate.Substring(0, 4))).ToString();
                
                // static connectionStr from database properties
                string SqlConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
                
                //sets query to check email
                string SqLQueryEmail = string.Format("SELECT * FROM users WHERE (email = N'{0}')", email);
                
                //sets query to check username
                string SqLQueryUsername = string.Format("SELECT * FROM users WHERE (username = N'{0}')", username);
                
                // sets insert into users table query that will run in sql
                string SqLQueryInsert = string.Format("INSERT INTO users " +
                   "(phone, email, Fname, Lname, Bdate, age, gender, address, username, password) " +
                   "VALUES (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', N'{9}')", phone, email, firstName, lastName, birthDate, age, gender, address, username, password);
                
                //connects to the databae with the given path
                SqlConnection SQLConnection = new SqlConnection(SqlConnectionString);

                //sets command that will run in sql to check email
                SqlCommand SQLCommandEmail = new SqlCommand(SqLQueryEmail, SQLConnection);
                SQLConnection.Open();//opens connection to database
                SqlDataReader emailExists = SQLCommandEmail.ExecuteReader();//executes sql command and saves output
                if (emailExists.HasRows)
                {
                    Session["Err"] = "כתובת האימייל הזאת כבר קיימת במערכת נסה להרשם מחדש";
                    Response.Redirect("Error.aspx");
                }
                SQLConnection.Close();//closes connection to database

                //sets command that will run in sql to check username
                SqlCommand SQLCommandUsername = new SqlCommand(SqLQueryUsername, SQLConnection);
                SQLConnection.Open();//opens connection to database
                SqlDataReader usernameExists = SQLCommandUsername.ExecuteReader();//executes sql command and saves output
                if (usernameExists.HasRows)
                {
                    Session["Err"] = "שם המשתמש הזה כבר קיימת במערכת נסה להרשם מחדש";
                    Response.Redirect("Error.aspx");
                }
                SQLConnection.Close();//closes connection to database

                //sets command that will run in sql to insert into table
                SqlCommand SQLCommandInsert = new SqlCommand(SqLQueryInsert, SQLConnection);
                SQLConnection.Open(); //opens connection to database
                int affect = SQLCommandInsert.ExecuteNonQuery(); //executes sql command and returns rows affected
                SQLConnection.Close(); //closes connection to database
                if (affect == 1) //checks if it worked and if it did redirects to login.aspx
                    Response.Redirect("home.aspx");
                else
                {
                    Session["Err"] = "הייתה שגיאה נסה להרשם מחדש";
                    Response.Redirect("Error.aspx");
                }


            }
        }
    }
}