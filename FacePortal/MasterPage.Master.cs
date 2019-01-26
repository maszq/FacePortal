using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FacePortal
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        SQLDatabase db;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.countMe();
            DataSet tmpDs = new DataSet();
            tmpDs.ReadXml(Server.MapPath("~/res/xml/counter.xml"));
            visitcounter.Text = tmpDs.Tables[0].Rows[0]["hitcounter"].ToString();
            //CHECK COOKIES CONSENT
            if (Request.Cookies["CookieConsent"] != null)
            {
                cookieConsent.Visible = false;
            }

            if (Session["id"] == null)
            {
                login.Visible = true;
                logout.Visible = false;
                account.Visible = false;
                list_users.Visible = false;
                comparison.Visible = false;
                hello_user.Visible = false;
            }
            else
            {
                db = new SQLDatabase();
                db.Connect();
                login.Visible = false;
                logout.Visible = true;
                account.Visible = true;
                reg.Visible = false;
                list_users.Visible = false;
                comparison.Visible = true;
                hello_user.Visible = true;
                hello_user.Text = "Hello " + db.getNickname_email((string)Session["id"]);
                if (db.getType_User(db.getId((string)Session["id"])).Equals("administrator"))
                {
                    list_users.Visible = true;
                    comparison.Visible = true;
                }
                db.Disconnect();
            }

        }

        protected void Accept_Cookies(object sender, EventArgs e)
        {
            HttpCookie cookie_consent = new HttpCookie("CookieConsent");
            cookie_consent.Value = "yes";
            cookie_consent.Expires = DateTime.Now.AddMinutes(5);
            Response.Cookies.Add(cookie_consent);
            cookieConsent.Visible = false;
        }

        private void countMe()
        {
            DataSet tmpDs = new DataSet();
            tmpDs.ReadXml(Server.MapPath("~/res/xml/counter.xml"));
            int hits = Int32.Parse(tmpDs.Tables[0].Rows[0]["hitcounter"].ToString());
            hits += 1;
            tmpDs.Tables[0].Rows[0]["hitcounter"] = hits.ToString();
            tmpDs.WriteXml(Server.MapPath("~/res/xml/counter.xml"));
        }
    }
}