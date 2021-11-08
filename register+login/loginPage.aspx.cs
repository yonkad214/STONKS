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
    public partial class loginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["submitlogin"] != null)
            {
                //gets username and email from form
                string username = Request.Form["username"];
                string password = Request.Form["password"];

                if (IsAdmin(username, password))
                {
                    Session["usernameData"] = username;
                    Session["passwordData"] = password;
                    Session["Admin"] = true;
                    Response.Redirect("info.aspx");
                }
                password = HashLib.GetHashString(password);
                if (exists(username, password))
                {
                    Session["usernameData"] = username;
                    Session["passwordData"] = password;
                    Session["Admin"] = false;
                    Response.Redirect("info.aspx");
                }
                else
                {
                    Session["err"] = "שגיאה, נסה להתחבר מחדש";
                    Response.Redirect("Error.aspx");
                }
            }
        }
        public bool exists(string user, string pass)
        {
            bool exist = false;

            // static connectionStr from database properties
            string SqlConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";

            //sets sql query
            string SQLQuery = string.Format("SELECT * FROM Users WHERE username =N'{0}' and password=N'{1}'", user, pass);
            //sets connection to sql database
            SqlConnection sqlConnection = new SqlConnection(SqlConnectionString);

            SqlCommand sqlCommand = new SqlCommand(SQLQuery, sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlData = sqlCommand.ExecuteReader();
            if (sqlData.HasRows)
                exist = true;
            sqlConnection.Close();
            return exist;
        }
        public bool IsAdmin(string username, string pass)
        {
            bool isAdmin = false;
            DataSet xmlDs = new DataSet();
            xmlDs.ReadXml(System.Web.HttpContext.Current.Server.MapPath("admins.xml"));
            foreach (DataRow row in xmlDs.Tables[0].Rows)
            {
                if (username.Equals(row[0]) && pass.Equals(row[1]))
                {
                    isAdmin = true;
                }
            }
            return isAdmin;
        }
    }
}