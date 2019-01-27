using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FacePortal
{
    public partial class MyAccount : System.Web.UI.Page
    {
        SQLDatabase db;
        StringBuilder html;
        List<User> list_user;
        String email;
        String pass;
        int id, id_i;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new SQLDatabase();
            html = new StringBuilder();
            db.Connect();
            list_user = db.UserList();
            id = db.getId((string)Session["id"]);
            html.Append("<table border = '1' align='center'>");

            for (int i = 0; i < list_user.Count; i++)
            {
                if (list_user.ElementAt(i).id.Equals(id))
                {
                    html.Append("<tr><td>Id</td> <td>" + list_user.ElementAt(i).id + "</td></tr>");
                    html.Append("<tr><td>Nickname</td> <td>" + list_user.ElementAt(i).nickname + "</td></tr>");
                    html.Append("<tr><td>E-mail</td> <td>" + list_user.ElementAt(i).email + "</td></tr>");
                    html.Append("<tr><td>Imię</td> <td>" + list_user.ElementAt(i).name + "</td></tr>");
                    html.Append("<tr><td>Nazwisko</td> <td>" + list_user.ElementAt(i).surname + "</td></tr>");
                    html.Append("<tr><td>Płeć</td> <td>" + list_user.ElementAt(i).sex + "</td></tr>");
                    email = list_user.ElementAt(i).email;
                    pass = list_user.ElementAt(i).password;
                    id_i = i;
                }
            }

            html.Append("</table>");
            html.Append("<br/><br/>");
            PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });

            db.Disconnect();
        }
        protected void Form_Data(object sender, EventArgs e)
        {
            data_form.Visible = true;
            password_form.Visible = false;
            db = new SQLDatabase();
            db.Connect();
            list_user = db.UserList();
            nickname.Value = list_user.ElementAt(id_i).nickname;
            inputEmail.Value = list_user.ElementAt(id_i).email;
            name.Value = list_user.ElementAt(id_i).name;
            surname.Value = list_user.ElementAt(id_i).surname;
            db.Disconnect();
        }

        protected void Edit_Data(object sender, EventArgs e)
        {
            db = new SQLDatabase();
            db.Connect();
            list_user = db.UserList();
            string sex = "";
            if (!male.Checked && !female.Checked) sex = list_user.ElementAt(id_i).sex;
            else
            {
                if (male.Checked) sex = "male";
                else sex = "female";
            }
           
            db.EditAccount(id, nickname.Value, inputEmail.Value, name.Value, surname.Value, sex);

            if (!inputEmail.Value.Equals(email))
            {
                Session.RemoveAll();
                Response.Redirect("Login.aspx");
            }
            else Response.Redirect("MyAccount.aspx");
            db.Disconnect();
        }

        protected void Edit_Password(object sender, EventArgs e)
        {
            password_form.Visible = true;
            data_form.Visible = false;
            if (password != null && new_password != null)
            {
                db = new SQLDatabase();
                db.Connect();
                if (pass.Equals(password.Value))
                {
                    db.EditPassword(id, new_password.Value);
                    Session.RemoveAll();
                    Response.Redirect("Login.aspx");
                }
                db.Disconnect();
            }
        }





    }
}