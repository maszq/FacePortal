using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FacePortal
{
    public class CelebriteAlbum
    {

        public int id;
        public int id_character;
        public string name;
        public string surname;
        public byte[] image;

        public CelebriteAlbum(SqlDataReader dr)
        {
            id = dr.GetInt32(0);
            id_character = dr.GetInt32(1);
            name = dr.GetString(2);
            surname = dr.GetString(3);
            //image = dr.GetBytes(4, image, 0, image.Length);
        }
    }


}