using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FacePortal
{
    /// <summary>
    /// Klasa Comparison odpowiada za możliwość zamodelowania twarzy do porównania, wskazanie zdjęcia do porównania oraz wybranie typu porównania.
    /// </summary>
    public partial class Comparison : System.Web.UI.Page
    {
        SQLDatabase db;
        string recepta;
        int id, amount_character;
        List<string> characters;
        List<string> all_characters;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Panel1.BackImageUrl = "~/res/img/face.JPG";
            db = new SQLDatabase();
            characters = new List<string>();
            all_characters = new List<string>();
            FileUploadControl.Dispose();
        }
        /// <summary>
        /// Metoda wywoływana po wciśnięciu przycisku Wykonaj. Wstawia wskazane zdjęcie do bazy danych. Następnie zczytuje zamodelowaną twarz i wysyła przepis do silnika.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            db.Connect();
            id = db.getId((string)Session["id"]);
            db.InsertCharacter();
            amount_character = db.AmountCharacters();
            db.Disconnect();

            Stream fs = FileUploadControl.PostedFile.InputStream;
            using (BinaryReader br = new BinaryReader(fs))
            {
                 byte[] bytes = br.ReadBytes((Int32)fs.Length);
                  using (SqlConnection con = new SqlConnection("Data Source=fp-server.database.windows.net;" +
                "Initial Catalog=fp-database;User ID=fp-admin;Password=Cebula1."))
                        {
                            string query = "insert into USER_ALBUM values (@id_user, @id_character, @image)";
                            using (SqlCommand cmd = new SqlCommand(query))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@id_user", id);
                                cmd.Parameters.AddWithValue("@id_character", amount_character);
                                cmd.Parameters.AddWithValue("@image", bytes);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        bytes = null;
                        br.Dispose();
                        br.Close();
                    }
                    fs.Dispose();
                    fs.Close();
            FileUploadControl.Dispose();
            
            checkCharacter();
            exec(id, recepta);

            if (oneCelebrite.Checked)
            {
                if (ranking.Checked) Response.Redirect("Raport.aspx?type=one&ranking=true");
                else Response.Redirect("Raport.aspx?type=one&ranking=false");
            }
            else if (allCelebrite.Checked)
            {
                if(ranking.Checked) Response.Redirect("Raport.aspx?type=all&ranking=true");
                else Response.Redirect("Raport.aspx?type=all&ranking=false");
            }    
        }

        protected void checkCharacter()
        {
            if (włosy.Checked) { recepta = recepta + "wlosy,"; characters.Add("włosy"); }
            if (czoło.Checked) { recepta = recepta + "czolo,"; characters.Add("czoło"); }
            if (lewaBrew.Checked) { recepta = recepta + "brewlewa,"; characters.Add("lewaBrew"); }
            if (prawaBrew.Checked) { recepta = recepta + "brewprawa,"; characters.Add("prawaBrew"); }
            if (leweUcho.Checked) { recepta = recepta + "ucholewe,"; characters.Add("leweUcho"); }
            if (leweOko.Checked) { recepta = recepta + "okolewe,"; characters.Add("leweOko"); }
            if (praweOko.Checked) {recepta = recepta + "okoprawe,"; characters.Add("praweOko"); }
            if (praweUcho.Checked) {recepta = recepta + "uchoprawe,"; characters.Add("praweUcho");}
            if (lewyPoliczek.Checked) {recepta = recepta + "poliklewy,"; characters.Add("lewyPoliczek");}
            if (nos.Checked){ recepta = recepta + "nos,"; characters.Add("nos");}
            if (prawyPoliczek.Checked) {recepta = recepta + "polikprawy,"; characters.Add("prawyPoliczek");}
            if (usta.Checked) {recepta = recepta + "usta,"; characters.Add("usta");}
            if (broda.Checked) {recepta = recepta + "broda,"; characters.Add("broda");}

            if (all.Checked) 
                {
                    allCharacter();
                    /*foreach (string i in all_characters) {characters.Add(i);}*/
                } 
        }

        protected void allCharacter()
        {
            recepta = "";
            recepta = recepta + "wlosy,"; all_characters.Add("włosy"); 
            recepta = recepta + "czolo,"; all_characters.Add("czoło"); 
            recepta = recepta + "brewlewa,"; all_characters.Add("lewaBrew"); 
            recepta = recepta + "brewprawa,"; all_characters.Add("prawaBrew"); 
            recepta = recepta + "ucholewe,"; all_characters.Add("leweUcho"); 
            recepta = recepta + "okolewe,"; all_characters.Add("leweOko"); 
            recepta = recepta + "okoprawe,"; all_characters.Add("praweOko"); 
            recepta = recepta + "uchoprawe,"; all_characters.Add("praweUcho"); 
            recepta = recepta + "poliklewy,"; all_characters.Add("lewyPoliczek"); 
            recepta = recepta + "nos,"; all_characters.Add("nos"); 
            recepta = recepta + "polikprawy,"; all_characters.Add("prawyPoliczek"); 
            recepta = recepta + "usta,"; all_characters.Add("usta"); 
            recepta = recepta + "broda,"; all_characters.Add("broda"); 
        }

        protected void exec(int id, string recepta)
        {
        string html = string.Empty;
        //api/values/{id}/{recepta}
        string url = @"http://faceengine.azurewebsites.net/api/characteristics/u/"+id+"/"+recepta;
            Console.WriteLine(url);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.AutomaticDecompression = DecompressionMethods.GZip;

             using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
             using (Stream stream = response.GetResponseStream())
             using (StreamReader reader = new StreamReader(stream))
             {
                 html = reader.ReadToEnd();
             }
             Console.WriteLine(html);
        }
    }
}