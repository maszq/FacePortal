using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FacePortal
{
    public partial class AddCelebrite : System.Web.UI.Page
    {
        SQLDatabase db;
        string recepta;
        int id, amount_character;
        List<CelebriteAlbum> celebrite_list;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            FileUploadControl.Dispose();
            db = new SQLDatabase();
            FileUploadControl.Dispose();
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            db.Connect();
            id = db.getId((string)Session["id"]);
            db.InsertCharacter();
            
            amount_character = db.AmountCharacters();
            addCelebrite();

            celebrite_list = db.CelebriteAlbum();
            db.Disconnect();

            allCharacter();

            exec(id, recepta);
            //Response.Redirect("CelebriteList.aspx");
        }

        protected void addCelebrite()
        {
            Stream fs = FileUploadControl.PostedFile.InputStream;
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                using (SqlConnection con = new SqlConnection("Data Source=fp-server.database.windows.net;" +
              "Initial Catalog=fp-database;User ID=fp-admin;Password=Cebula1."))
                {
                    string query = "insert into CELEBRITE_ALBUM values (@id_character, @name, @surname, @image)";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@id_character", amount_character);
                        cmd.Parameters.AddWithValue("@name", name.Text);
                        cmd.Parameters.AddWithValue("@surname", surname.Text);
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
        }

        protected void allCharacter()
        {
            recepta = "";
            recepta = recepta + "wlosy,";
            recepta = recepta + "czolo,";
            recepta = recepta + "brewlewa,";
            recepta = recepta + "ucholewe,";
            recepta = recepta + "okolewe,";
            recepta = recepta + "okoprawe,";
            recepta = recepta + "uchoprawe,";
            recepta = recepta + "poliklewy,";
            recepta = recepta + "nos,";
            recepta = recepta + "polikprawy,";
            recepta = recepta + "usta,";
            recepta = recepta + "broda,";
        }

        protected void exec(int id, string recepta)
        {
            string html = string.Empty;
            //api/values/{id}/{recepta}
            string url = @"http://faceengine.azurewebsites.net/api/values/a/" + getId_celebrite_album() + "/" + recepta;
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

        protected Int32 getId_celebrite_album()
        {
            int temp = 0;
            for (int i = 0; i<celebrite_list.Count; i++)
            {
                if (amount_character == celebrite_list.ElementAt(i).id_character) temp = celebrite_list.ElementAt(i).id;
            }
            return temp;
        }
    }
}