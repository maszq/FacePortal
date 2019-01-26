using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FacePortal
{
    public class User
    {
        public int id;
        public string nickname;
        public string password;
        public string email;
        public string name;
        public string surname;
        public string sex;
        public string type;

        public User(SqlDataReader dr)
        {
            id = dr.GetInt32(0);
            nickname = dr.GetString(1);
            password = dr.GetString(2);
            email = dr.GetString(3);
            name = dr.GetString(4);
            surname = dr.GetString(5);
            sex = dr.GetString(6);
            type = dr.GetString(7);
        }
    }
}