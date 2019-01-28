using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FacePortal
{
    /// <summary>
    /// Klasa odpowiadająca za wyświetlanie listy celebrytów.
    /// </summary>
    public partial class CelebriteList : System.Web.UI.Page
    {

        SQLDatabase db;
        List<CelebriteAlbum> list_celebrite;

        StringBuilder html;

        protected void Page_Load(object sender, EventArgs e)
        {
                db = new SQLDatabase();
                db.Connect();
                list_celebrite = db.CelebriteAlbum();

                html = new StringBuilder();

                    html.Append("<table border = '1' align='center'>");
                    html.Append("<tr>");
                    html.Append("<th>Id</th><th>Id Character</th><th>Name</th><th>Surname</th>");
                    html.Append("</tr>");
                    for (int i = 0; i < list_celebrite.Count; i++)
                    {
                        html.Append("<tr>");
                        html.Append("<td>" + list_celebrite.ElementAt(i).id + "</td>");
                        html.Append("<td>" + list_celebrite.ElementAt(i).id_character + "</td>");
                        html.Append("<td>" + list_celebrite.ElementAt(i).name + "</td>");
                        html.Append("<td>" + list_celebrite.ElementAt(i).surname + "</td>");
                        html.Append("</tr>");
                    }
                    html.Append("</table>");
                    html.Append("<br/><br/>");
                
                PlaceHolder.Controls.Add(new Literal { Text = html.ToString() });
 
            if (db.getType_User(db.getId((string) Session["id"])).Equals("administrator"))
                {
                addCelebrite.Visible = true;
                }
            db.Disconnect();
        }

        protected void addCelebrite_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCelebrite.aspx");
           
        }
    }
}