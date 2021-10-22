using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace register_login
{
    public partial class Page : System.Web.UI.MasterPage
    {
        protected string button0 = "";
        protected string button1 = "";
        protected string button2 = "";
        
        protected string tableList = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usernameData"] !=null){
                button1 = "<button class=\"button register\" onclick=\"window.location.href = \'info.aspx\';\">" + Session["usernameData"] + "</button>";
                button2 = "<button class=\"button login\"  name=\"logOut\" onclick = \"window.location.href = \'logOut.aspx\';\">התנתק</button>";
                
                tableList = "<div class=\"chooseGame\"><a class=\"chooseGame\" href=\"dungeonGame.aspx\">Dungeon Crawler</a><a class=\"chooseGame\" href=\"ticTacToe.aspx\">Tic Tac Toe</a><a class=\"chooseGame\" href=\"game.aspx\">Pong</a><a class=\"chooseGame\" href=\"Jumper.aspx\">Jumper</a></div>";
            }
            else
            {
                button0 = "<button class=\"button register\" onclick=\"window.location.href = \'registration.aspx\';\">הרשמה</button>";
                button1 = "<button class=\"button login\" onclick=\"window.location.href = 'loginPage.aspx';\">כניסה</button>";
                button2 = null;
                tableList = null;
            }
        }
    }
}