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
    /// <summary>
    /// Klasa obliczająca wynik porównania.
    /// </summary>
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
        bool ranking;
        List<Result> lista_wynikow;
        bool all;
        bool pdf_one = false;

        Byte[] bytes;
        MemoryStream ms;
        Document doc;
        PdfWriter wri;

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
            ranking = false;
            lista_wynikow = new List<Result>();
            all = false;

            ms = new MemoryStream();
            doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            wri = PdfWriter.GetInstance(doc, ms);



            html = new StringBuilder();
            if (Request.QueryString["ranking"] == "true") ranking = true;

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
                pdf_one = true;
                generateDoc.Visible = true;
                id_celebrite = int.Parse(Request.QueryString["id_celebrite"]);
                id_character_celebrite = find_id_character_celebrite(id_celebrite-1);
                id_character_user = find_id_character_user().Max();
                all = false;
                compareCharacters(id_character_celebrite, id_celebrite-1, true, 0);
                name.Visible = true;
                Surname.Visible = true;
                name_value.Visible = true;
                surname_value.Visible = true;
                name_value.Text = list_celebrite.ElementAt(id_celebrite-1).name ;
                surname_value.Text = list_celebrite.ElementAt(id_celebrite-1).surname;
            }

            if (Request.QueryString["type"] == "all")
            {
                generateDoc.Visible = true;
                pdf_one = false;
                if(Request.QueryString["id_celebrite"]!=null) id_celebrite = int.Parse(Request.QueryString["id_celebrite"]);
                id_character_user = find_id_character_user().Max();

                all = true;
                int t = 0;
                for (int i = 0; i <list_celebrite.Count();i++)
                {
                    compareCharacters(list_celebrite.ElementAt(i).id_character,list_celebrite.ElementAt(i).id, false, 0);
                    t++;
                }

                all = false;
                int max_id_cel = find_idcel_z_max_lista_wynikow();
                name.Visible = true;
                Surname.Visible = true;
                name_value.Visible = true;
                surname_value.Visible = true;

                name_value.Text =list_celebrite.ElementAt(max_id_cel).name;
                //name_value.Text = lista_wynikow.ElementAt(0).percent_result.ToString();
                surname_value.Text =list_celebrite.ElementAt(max_id_cel).surname;

                compareCharacters(find_id_character_celebrite(max_id_cel), max_id_cel+1, true, max_id_cel);

            }

            PlaceHolder.Controls.Add(new Literal { Text = html.ToString() });
        }
        /// <summary>
        /// Metoda obliczająca różnice w cechach.
        /// </summary>
        protected void compareCharacters(int id_char_cel, int id_cel, bool pdf, int max_id_cel)
        {
            List<Int32> roznica = new List<Int32>();
            Character celebrite = list_character.ElementAt(id_char_cel);
            wyniki_pdf = new List<Double>();
            Character user = list_character.ElementAt(id_character_user-1);
            Double wynik = 0;
            int ilosc_cech = 0;
            
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
            if (pdf)
            {
                doc.Open();
                Paragraph par = new Paragraph("Raport porównania użytkownika " + nickname+"\n");
                doc.Add(par);
                if (pdf_one) par = new Paragraph("Celebryta z porównania " + list_celebrite.ElementAt(id_cel).name + " " + list_celebrite.ElementAt(id_cel).surname + "\n");
                else par = new Paragraph("Celebryta z porównania " + list_celebrite.ElementAt(id_cel - 1).name + " " + list_celebrite.ElementAt(id_cel - 1).surname + "\n");

                doc.Add(par);
                par = new Paragraph("\n");
                doc.Add(par);
            }
            //wlosy
            if (user.wlosy_kolor != -1)
            {
                 if (roznica.ElementAt(0) ==0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
                else if (roznica.ElementAt(0) <= 10) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
                else if (roznica.ElementAt(0) <= 20) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
                else if (roznica.ElementAt(0) <= 30) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
                else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
                // wlosy.Text = roznica.ElementAt(0).ToString();
                wlosy.Text = (wyniki_pdf.Last()*100).ToString()+"%";
                wlosy.Visible = true;
                ilosc_cech++;
                Label2.Visible = true;
                if (pdf)
                {
                    Paragraph par = new Paragraph("Wlosy = " + (wyniki_pdf.Last() * 100).ToString()+"\n");
                    doc.Add(par);
                }
            }

            //czolo_wys
            if (user.czolo_wys != -1)
            {
                if (roznica.ElementAt(1) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
                else if (roznica.ElementAt(1) <= 5) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
                else if (roznica.ElementAt(1) <= 10) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
                else if (roznica.ElementAt(1) <= 15) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
                else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
                czolo_wys.Text = (wyniki_pdf.Last() * 100).ToString() + "%";
                czolo_wys.Visible = true;
                ilosc_cech++;
                Label3.Visible = true;
                if (pdf)
                {
                    Paragraph par = new Paragraph("Czolo wys = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                    doc.Add(par);
                }
            }

            //czolo_szer
            if (user.czolo_szer != -1)
            {
                if (roznica.ElementAt(2) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
                else if (roznica.ElementAt(2) <= 10) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
                else if (roznica.ElementAt(2) <= 20) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
                else if (roznica.ElementAt(2) <= 30) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
                else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
                czolo_szer.Text = (wyniki_pdf.Last() * 100).ToString() + "%";
                czolo_szer.Visible = true;
                ilosc_cech++;
                Label4.Visible = true;
                if (pdf)
                {
                    Paragraph par = new Paragraph("Czolo szer = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                    doc.Add(par);
                }
            }

            //oko_lewe_szer
            if (user.oko_lewe_szer != -1)
            {
                if (roznica.ElementAt(3) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
                else if (roznica.ElementAt(3) <= 3) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
                else if (roznica.ElementAt(3) <= 6) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
                else if (roznica.ElementAt(3) <= 9) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
                else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
                oko_lewe_szer.Text = (wyniki_pdf.Last() * 100).ToString() + "%";
                oko_lewe_szer.Visible = true;
                ilosc_cech++;
                Label5.Visible = true;
                if (pdf)
                {
                    Paragraph par = new Paragraph("Oko lewe szer = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                    doc.Add(par);
                }
            }


            //oko_lewe_wys
            if (user.oko_lewe_wys != -1)
            {
                if (roznica.ElementAt(4) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
                else if (roznica.ElementAt(4) <= 3) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
                else if (roznica.ElementAt(4) <= 6) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
                else if (roznica.ElementAt(4) <= 9) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
                else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
                oko_lewe_wys.Text = (wyniki_pdf.Last() * 100).ToString() + "%"; 
                oko_lewe_wys.Visible = true;
                ilosc_cech++;
                Label6.Visible = true;
                if (pdf)
                {
                    Paragraph par = new Paragraph("Oko lewe wys = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                    doc.Add(par);
                }
            }

            //oko_prawe_szer
            if (user.oko_prawe_szer != -1)
            {
                if (roznica.ElementAt(5) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
                else if (roznica.ElementAt(5) <= 3) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
                else if (roznica.ElementAt(5) <= 6) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
                else if (roznica.ElementAt(5) <= 9) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
                else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
                oko_prawe_szer.Text = (wyniki_pdf.Last() * 100).ToString() + "%";
                oko_prawe_szer.Visible = true;
                ilosc_cech++;
                Label7.Visible = true;
                if (pdf)
                {
                    Paragraph par = new Paragraph("Oko prawe szer = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                    doc.Add(par);
                }
            }

            //oko_prawe_wys
            if (user.oko_prawe_wys != -1)
            {
                if (roznica.ElementAt(6) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
                else if (roznica.ElementAt(6) <= 3) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
                else if (roznica.ElementAt(6) <= 6) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
                else if (roznica.ElementAt(6) <= 9) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
                else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
                oko_prawe_wys.Text = (wyniki_pdf.Last() * 100).ToString() + "%";
                oko_prawe_wys.Visible = true;
                ilosc_cech++;
                Label8.Visible = true;
                if (pdf)
                {
                    Paragraph par = new Paragraph("Oko prawe wys = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                    doc.Add(par);
                }
            }

            //twarz_szer
            if (roznica.ElementAt(7) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(7) <= 10) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(7) <= 20) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(7) <= 30) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            twarz_szer.Text = (wyniki_pdf.Last() * 100).ToString() + "%";
            twarz_szer.Visible = true;
            ilosc_cech++;
            Label9.Visible = true;
            if (pdf)
            {
                Paragraph par = new Paragraph("Twarz szer = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                doc.Add(par);
            }

            //twarz_wys
            if (roznica.ElementAt(8) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
            else if (roznica.ElementAt(8) <= 10) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
            else if (roznica.ElementAt(8) <= 20) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
            else if (roznica.ElementAt(8) <= 30) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
            else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
            twarz_wys.Text = (wyniki_pdf.Last() * 100).ToString() + "%";
            twarz_wys.Visible = true;
            ilosc_cech++;
            Label10.Visible = true;
            if (pdf)
            {
                Paragraph par = new Paragraph("Twarz wys = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                doc.Add(par);
            }

            //odl_oczy
            if (user.oko_prawe_wys != -1 && user.oko_prawe_szer != -1 && user.oko_lewe_szer != -1 && user.oko_lewe_wys != -1)
            {
                if (roznica.ElementAt(9) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
                else if (roznica.ElementAt(9) <= 5) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
                else if (roznica.ElementAt(9) <= 10) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
                else if (roznica.ElementAt(9) <= 15) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
                else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
                odl_oczy.Text = (wyniki_pdf.Last() * 100).ToString() + "%";
                odl_oczy.Visible = true;
                ilosc_cech++;
                Label11.Visible = true;
                if (pdf)
                {
                    Paragraph par = new Paragraph("Odl. oczy = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                    doc.Add(par);
                }
            }

            //polik_lewy
            if (user.polik_lewy_szer != -1)
            {
                if (roznica.ElementAt(10) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
                else if (roznica.ElementAt(10) <= 10) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
                else if (roznica.ElementAt(10) <= 20) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
                else if (roznica.ElementAt(10) <= 30) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
                else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
                polik_lewy_szer.Text = (wyniki_pdf.Last() * 100).ToString() + "%";
                polik_lewy_szer.Visible = true;
                ilosc_cech++;
                Label12.Visible = true;
                if (pdf)
                {
                    Paragraph par = new Paragraph("Polik lewy = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                    doc.Add(par);
                }
            }

            //polik_prawy
            if (user.polik_prawy_szer != -1)
            {
                if (roznica.ElementAt(11) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
                else if (roznica.ElementAt(11) <= 10) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
                else if (roznica.ElementAt(11) <= 20) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
                else if (roznica.ElementAt(11) <= 30) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
                else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
                polik_prawy_szer.Text = (wyniki_pdf.Last() * 100).ToString() + "%";
                polik_prawy_szer.Visible = true;
                ilosc_cech++;
                Label13.Visible = true;
                if (pdf)
                {
                    Paragraph par = new Paragraph("Polik prawy = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                    doc.Add(par);
                }
            }

            //nos
            if (user.nos_wys != -1)
            {
                if (roznica.ElementAt(12) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
                else if (roznica.ElementAt(12) <= 5) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
                else if (roznica.ElementAt(12) <= 10) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
                else if (roznica.ElementAt(12) <= 15) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
                else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
                nos_wys.Text = (wyniki_pdf.Last() * 100).ToString() + "%";
                nos_wys.Visible = true;
                ilosc_cech++;
                Label14.Visible = true;
                if (pdf)
                {
                    Paragraph par = new Paragraph("Nos = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                    doc.Add(par);
                }
            }

            //broda_wys
            if (user.broda_wys != -1)
            {
                if (roznica.ElementAt(13) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
                else if (roznica.ElementAt(13) <= 5) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
                else if (roznica.ElementAt(13) <= 10) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
                else if (roznica.ElementAt(13) <= 15) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
                else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
                broda_wys.Text = (wyniki_pdf.Last() * 100).ToString() + "%";
                broda_wys.Visible = true;
                ilosc_cech++;
                Label15.Visible = true;
                if (pdf)
                {
                    Paragraph par = new Paragraph("Broda wys = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                    doc.Add(par);
                }
            }

            //broda_szer
            if (user.broda_szer != -1)
            {
                if (roznica.ElementAt(14) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
                else if (roznica.ElementAt(14) <= 5) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
                else if (roznica.ElementAt(14) <= 10) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
                else if (roznica.ElementAt(14) <= 15) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
                else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
                broda_szer.Text = (wyniki_pdf.Last() * 100).ToString() + "%";
                broda_szer.Visible = true;
                ilosc_cech++;
                Label16.Visible = true;
                if (pdf)
                {
                    Paragraph par = new Paragraph("Broda szer = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                    doc.Add(par);
                }
            }

            //usta_szer
            if (user.usta_szer != -1)
            {
                if (roznica.ElementAt(15) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
                else if (roznica.ElementAt(15) <= 3) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
                else if (roznica.ElementAt(15) <= 6) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
                else if (roznica.ElementAt(15) <= 9) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
                else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
                usta_szer.Text = (wyniki_pdf.Last() * 100).ToString() + "%";
                usta_szer.Visible = true;
                ilosc_cech++;
                Label17.Visible = true;
                if (pdf)
                {
                    Paragraph par = new Paragraph("Usta szer = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                    doc.Add(par);
                }
            }

            //usta_wys
            if (user.usta_wys != -1)
            {
                if (roznica.ElementAt(16) == 0) { wynik = wynik + 1; wyniki_pdf.Add(1); }
                else if (roznica.ElementAt(16) <= 3) { wynik = wynik + 0.9; wyniki_pdf.Add(0.9); }
                else if (roznica.ElementAt(16) <= 6) { wynik = wynik + 0.7; wyniki_pdf.Add(0.7); }
                else if (roznica.ElementAt(16) <= 9) { wynik = wynik + 0.5; wyniki_pdf.Add(0.5); }
                else { wynik = wynik + 0.3; wyniki_pdf.Add(0.3); }
                usta_wys.Text = (wyniki_pdf.Last() * 100).ToString() + "%";
                usta_wys.Visible = true;
                ilosc_cech++;
                Label18.Visible = true;
                if (pdf)
                {
                    Paragraph par = new Paragraph("Usta wys = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                    doc.Add(par);
                }
            }
            wyniki_pdf.Add(wynik / ilosc_cech);
            result.Text = ((wyniki_pdf.Last())*100).ToString()+"%";
            if (pdf)
            {
                Paragraph par = new Paragraph("Ogólny wynik = " + (wyniki_pdf.Last() * 100).ToString() + "\n");
                doc.Add(par);
                doc.Close();
            }
            result.Visible = true;
            Label19.Visible = true;

            db.Connect();
            db.InsertResult(id, id_cel, (int)((wynik / ilosc_cech) * 100), ranking);
            if (all) lista_wynikow.Add(new Result(id, id_cel, (int)((wynik / ilosc_cech) * 100), ranking));
            db.Disconnect();
        }
        
        /// <summary>
        /// Metoda tworząca raport PDF.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void generateDoc_Click(object sender, EventArgs e)
        {

            bytes = ms.ToArray();

            Response.AddHeader("content-disposition", "attachment;filename=Raport.pdf");
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

        protected int find_id_character_celebrite(int id_cel)
        {
            int temp = 0;

            for (int i = 0; i < list_celebrite.Count(); i++)
            {
                if (list_celebrite.ElementAt(i).id == id_cel) temp=list_celebrite.ElementAt(i).id_character;
            }

            return temp;
        }

        protected Int32 find_idcel_z_max_lista_wynikow()
        {
            int temp_id_cel = 0;
            int temp_result = 0;
            for (int i = 0; i < lista_wynikow.Count(); i++)
            {
                if (lista_wynikow.ElementAt(i).percent_result > temp_result)
                {
                    temp_id_cel = lista_wynikow.ElementAt(i).id_celebrite;
                    temp_result = lista_wynikow.ElementAt(i).percent_result;
                }
            }

            return temp_id_cel;
        }


        /*protected void 1generateDoc_Click(object sender, EventArgs e)
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
        }*/


    }
}