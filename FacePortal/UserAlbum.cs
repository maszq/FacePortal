using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FacePortal
{
    public class UserAlbum
    {

        public int id;
        public int id_user;
        public int id_character;
        public byte image;

        public UserAlbum(SqlDataReader dr)
        {
            id = dr.GetInt32(0);
            id_user = dr.GetInt32(1);
            id_character = dr.GetInt32(2);
            //image = dr.GetByte(3);
          
        }
    }
}