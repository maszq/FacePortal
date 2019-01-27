using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;

namespace FacePortal
{
    public partial class Raport : System.Web.UI.Page
    {
        SQLDatabase db;
        List<CelebriteAlbum> list_celebrite;
        List<UserAlbum> list_user_album;
        List<Character> list_character;
        List<Double> wyniki_pdf;
        int id, id_celebrite, id_character_celebrite, id_character_user;
        StringBuilder html;
        string nickname;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new SQLDatabase();
            db.Connect();
            id = db.getId((string)Session["id"]);
            list_user_album = db.UserAlbum();
            list_celebrite = db.CelebriteAlbum();
            list_character = db.CharacterList();
            nickname = db.getNickname_email((string)Session["id"]);
            list_user_album = db.UserAlbum();
            db.Disconnect();

            //string name_user = db.getNickname_id()

            html = new StringBuilder();

            if (Request.QueryString["type"] == "one" && Request.QueryString["id_celebrite"]==null)
            {
                
                html.Append("<table border = '1' align='center'>");
                html.Append("<tr>");
                html.Append("<th>Wybierz</th><th>Id</th><th>Id Character</th><th>Name</th><th>Surname</th>");
                html.Append("</tr>");
                for (int i = 0; i < list_celebrite.Count; i++)
                {
                    html.Append("<tr>");
                    html.Append("<td><a href=\"Raport.aspx?type=one&id_celebrite=" + list_celebrite.ElementAt(i).id + "\">Wybierz</a></td>");
                    html.Append("<td>" + list_celebrite.ElementAt(i).id + "</td>");
                    html.Append("<td>" + list_celebrite.ElementAt(i).id_character + "</td>");
                    html.Append("<td>" + list_celebrite.ElementAt(i).name + "</td>");
                    html.Append("<td>" + list_celebrite.ElementAt(i).surname + "</td>");
                    html.Append("</tr>");
                }
                html.Append("</table>");
                html.Append("<br/><br/>");
            }

            if (Request.QueryString["type"] == "one" && Request.QueryString["id_celebrite"] != null)
            {
                //generateDoc.Visible = true;
                id_celebrite = int.Parse(Request.QueryString["id_celebrite"]);
                id_character_celebrite = find_id_character_celebrite().Max();
                id_character_user = find_id_character_user().Max();

                compareCharacters();

            }

            if (Request.QueryString["type"] == "all")
            {
                //generateDoc.Visible = true;
                id_character_user = find_id_character_user().Max();

                compareCharacters();


            }

            PlaceHolder.Controls.Add(new Literal { Text = html.ToString() });
        }

        protected void compareCharacters()
        {
            List<Int32> roznica = new List<Int32>();
            Character celebrite = list_character.ElementAt(id_character_celebrite-1);
            wyniki_pdf = new List<Double>();
            Character user = list_character.ElementAt(id_character_user-1);
            Double wynik = 0;
            
            roznica.Add(Math.Abs(celebrite.wlosy_kolor-user.wlosy_kolor));
            roznica.Add(Math.Abs(celebrite.czolo_wys - user.czolo_wys));
            roznica.Add(Math.Abs(celebrite.czolo_szer-celebrite.czolo_szer));
            roznica.Add(Math.Abs(celebrite.oko_lewe_szer-user.oko_lewe_szer));
            roznica.Add(Math.Abs(celebrite.oko_lewe_wys-user.oko_lewe_wys));
            roznica.Add(Math.Abs(celebrite.oko_prawe_szer-user.oko_prawe_szer));
            roznica.Add(Math.Abs(celebrite.oko_prawe_wys-user.oko_prawe_wys));
            roznica.Add(Math.Abs(celebrite.twarz_szer-user.twarz_szer));
            roznica.Add(Math.Abs(celebrite.twarz_wys-user.twarz_wys));
            roznica.Add(Math.Abs(celebrite.odl_oczy-user.odl_oczy));
            roznica.Add(Math.Abs(celebrite.polik_lewy_szer-user.polik_lewy_szer));
            roznica.Add(Math.Abs(celebrite.polik_prawy_szer-user.polik_prawy_szer));
            roznica.Add(Math.Abs(celebrite.nos_wys-user.nos_wys));
            roznica.Add(Math.Abs(celebrite.broda_szer - user.broda_szer));
            roznica.Add(Math.Abs(celebrite.broda_wys-user.broda_wys));
            roznica.Add(Math.Abs(celebrite.usta_szer-user.usta_szer));
            roznica.Add(Math.Abs(celebrite.usta_wys-user.usta_wys));

            //wlosy
            if (roznica.ElementAt(0) ==0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(0) <= 10) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(0) <= 20) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(0) <= 30) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            wlosy.Text = roznica.ElementAt(0).ToString();
            wlosy.Visible = true;

            //czolo_wys
            if (roznica.ElementAt(1) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(1) <= 5) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(1) <= 10) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(1) <= 15) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            czolo_wys.Text = roznica.ElementAt(1).ToString();
            czolo_wys.Visible = true;

            //czolo_szer
            if (roznica.ElementAt(2) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(2) <= 10) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(2) <= 20) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(2) <= 30) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            czolo_szer.Text = roznica.ElementAt(2).ToString();
            czolo_szer.Visible = true;

            //oko_lewe_szer
            if (roznica.ElementAt(3) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(3) <= 3) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(3) <= 6) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(3) <= 9) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            oko_lewe_szer.Text = roznica.ElementAt(3).ToString();
            oko_lewe_szer.Visible = true;

            //oko_lewe_wys
            if (roznica.ElementAt(4) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(4) <= 3) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(4) <= 6) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(4) <= 9) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            oko_lewe_wys.Text = roznica.ElementAt(4).ToString();
            oko_lewe_wys.Visible = true;

            //oko_prawe_szer
            if (roznica.ElementAt(5) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(5) <= 3) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(5) <= 6) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(5) <= 9) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            oko_prawe_szer.Text = roznica.ElementAt(5).ToString();
            oko_prawe_szer.Visible = true;

            //oko_prawe_wys
            if (roznica.ElementAt(6) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(6) <= 3) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(6) <= 6) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(6) <= 9) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            oko_prawe_wys.Text = roznica.ElementAt(6).ToString();
            oko_prawe_wys.Visible = true;

            //twarz_szer
            if (roznica.ElementAt(7) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(7) <= 10) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(7) <= 20) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(7) <= 30) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            twarz_szer.Text = roznica.ElementAt(7).ToString();
            twarz_szer.Visible = true;

            //twarz_wys
            if (roznica.ElementAt(8) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(8) <= 10) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(8) <= 20) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(8) <= 30) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            twarz_wys.Text = roznica.ElementAt(8).ToString();
            twarz_wys.Visible = true;

            //odl_oczy
            if (roznica.ElementAt(9) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(9) <= 5) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(9) <= 10) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(9) <= 15) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            odl_oczy.Text = roznica.ElementAt(9).ToString();
            odl_oczy.Visible = true;

            //polik_lewy
            if (roznica.ElementAt(10) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(10) <= 10) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(10) <= 20) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(10) <= 30) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            polik_lewy_szer.Text = roznica.ElementAt(10).ToString();
            polik_lewy_szer.Visible = true;

            //polik_prawy
            if (roznica.ElementAt(11) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(11) <= 10) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(11) <= 20) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(11) <= 30) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            polik_prawy_szer.Text = roznica.ElementAt(011).ToString();
            polik_prawy_szer.Visible = true;

            //nos
            if (roznica.ElementAt(12) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(12) <= 5) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(12) <= 10) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(12) <= 15) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            nos_wys.Text = roznica.ElementAt(12).ToString();
            nos_wys.Visible = true;

            //broda_wys
            if (roznica.ElementAt(13) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(13) <= 5) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(13) <= 10) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(13) <= 15) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            broda_wys.Text = roznica.ElementAt(13).ToString();
            broda_wys.Visible = true;

            //broda_szer
            if (roznica.ElementAt(14) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(14) <= 5) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(14) <= 10) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(14) <= 15) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            broda_szer.Text = roznica.ElementAt(014).ToString();
            broda_szer.Visible = true;

            //usta_szer
            if (roznica.ElementAt(15) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(15) <= 3) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(15) <= 6) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(15) <= 9) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            usta_szer.Text = roznica.ElementAt(15).ToString();
            usta_szer.Visible = true;

            //usta_wys
            if (roznica.ElementAt(16) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(16) <= 3) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(16) <= 6) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(16) <= 9) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            usta_wys.Text = roznica.ElementAt(16).ToString();
            usta_wys.Visible = true;

            result.Text = wynik.ToString();
            result.Visible = true;
            Label2.Visible = true;
            Label3.Visible = true;
            Label4.Visible = true;
            Label5.Visible = true;
            Label6.Visible = true;
            Label7.Visible = true;
            Label8.Visible = true;
            Label9.Visible = true;
            Label10.Visible = true;
            Label11.Visible = true;
            Label12.Visible = true;
            Label13.Visible = true;
            Label14.Visible = true;
            Label15.Visible = true;
            Label16.Visible = true;
            Label17.Visible = true;
            Label18.Visible = true;
            Label19.Visible = true;



        }

        protected void generateDoc_Click(object sender, EventArgs e)
        {

            Byte[] bytes;
            MemoryStream ms = new MemoryStream();
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, ms);

            doc.Open();
            string title = "            Raport z porównania użytkownika " + nickname + "\n";
            List<Int32> id_characters = find_id_character_user();
            string character = list_character.ElementAt(id_characters.Max()).oko_prawe_szer.ToString();

            Paragraph par = new Paragraph(title);


            doc.Add(par);
            par = new Paragraph(character);
            doc.Add(par);


            doc.Close();

            bytes = ms.ToArray();

            Response.AddHeader("content-disposition", "attachment;filename=report.pdf");
            Response.ContentType = "application/pdf";

            Response.BinaryWrite(bytes);
            Response.End();

        }

        protected List<Int32> find_id_character_user()
        {
            List<Int32> temp = new List<Int32>();

            for (int i = 0; i < list_user_album.Count(); i++)
            {
                if (list_user_album.ElementAt(i).id_user == id) temp.Add(list_user_album.ElementAt(i).id_character);
            }

            return temp;
        }

        protected List<Int32> find_id_character_celebrite()
        {
            List<Int32> temp = new List<Int32>();

            for (int i = 0; i < list_celebrite.Count(); i++)
            {
                if (list_celebrite.ElementAt(i).id == id_celebrite) temp.Add(list_celebrite.ElementAt(i).id_character);
            }

            return temp;
        }
    }
}