using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FacePortal
{
    public partial class Comparison : System.Web.UI.Page
    {
        SQLDatabase db;
        List<string> characters;
        List<string> all_characters;
        protected void Page_Load(object sender, EventArgs e)
        {
            Panel1.BackImageUrl = "~/res/img/face.JPG";
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            characters = new List<string>();
            all_characters = new List<string>();
           
            db = new SQLDatabase();
            db.Connect();
            int amount_character = 0;
            db.InsertCharacter(-1);
            amount_character = db.AmountCharacters();
            db.Disconnect();

            foreach (HttpPostedFile postedFile in FileUploadControl.PostedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                string contentType = postedFile.ContentType;
                using (Stream fs = postedFile.InputStream)
                {
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
                                cmd.Parameters.AddWithValue("@id_user", 6);
                                cmd.Parameters.AddWithValue("@id_character", amount_character);
                                cmd.Parameters.AddWithValue("@image", bytes);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
            }
            StatusLabel.Text = "File Uploaded!";

            checkCharacter();

            StatusLabel.Text = characters.Count.ToString();
        }

        protected void checkCharacter()
        {
            if (włosy.Checked) characters.Add("włosy");
            if (czoło.Checked) characters.Add("czoło");
            if (lewaBrew.Checked) characters.Add("lewaBrew");
            if (prawaBrew.Checked) characters.Add("prawaBrew");
            if (leweUcho.Checked) characters.Add("leweUcho");
            if (leweOko.Checked) characters.Add("leweOko");
            if (praweOko.Checked) characters.Add("praweOko");
            if (praweUcho.Checked) characters.Add("praweUcho");
            if (lewyPoliczek.Checked) characters.Add("lewyPoliczek");
            if (nos.Checked) characters.Add("nos");
            if (prawyPoliczek.Checked) characters.Add("prawyPoliczek");
            if (usta.Checked) characters.Add("usta");
            if (broda.Checked) characters.Add("broda");
            
            if (all.Checked) 
                {
                    allCharacter();
                    foreach (string i in all_characters)
                    {
                        characters.Add(i);
                    }
                } 
        }

        protected void allCharacter()
        {
            all_characters.Add("włosy");
            all_characters.Add("czoło");
            all_characters.Add("lewaBrew");
            all_characters.Add("prawaBrew");
            all_characters.Add("leweUcho");
            all_characters.Add("leweOko");
            all_characters.Add("praweOko");
            all_characters.Add("praweUcho");
            all_characters.Add("lewyPoliczek");
            all_characters.Add("nos");
            all_characters.Add("prawyPoliczek");
            all_characters.Add("usta");
            all_characters.Add("broda");
        }
    }
}