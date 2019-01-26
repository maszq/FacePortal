using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FacePortal
{
    public class SQLDatabase
    {

        /// <summary>
        /// The class responsible for handling the connection to the database. 
        /// It contains methods for adding, editing and deleting data from the database. 
        /// It also includes methods for creating lists of objects on which it is easier to work.
        /// </summary>
        private SqlConnection connection;
        private SqlCommand cmd;
        /// <summary>
        /// The method calls the Init method.
        /// </summary>
        public SQLDatabase()
        {
            Init();
        }

        /// <summary>
        /// The method includes a connection to the database.
        /// </summary>
        private void Init()
        {
            connection = new SqlConnection("Data Source=fp-server.database.windows.net;" +
                "Initial Catalog=fp-database;User ID=fp-admin;Password=Cebula1.");
        }

        /// <summary>
        /// The method that initiates the connection to the database.
        /// </summary>
        public void Connect()
        {
            connection.Open();
        }


        public void Disconnect()
        {
            connection.Close();
        }

        public void InsertUser(string nickname, string password, string email, string name, string surname, string sex, string type)
        {
            string query =
                "INSERT INTO USERS VALUES " +
                "('"
                + nickname + "', '"
                + password + "', '"
                + email + "', '"
                + name + "', '"
                + surname + "', '"
                + sex + "', '"
                + type + "');";

            cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
        }


        public void InsertCharacter(int character)
        {
            string query =
                "INSERT INTO CHARACTERS VALUES " +
                "("
                + character + ");";

            cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
        }


        public int AmountCharacters()
        {
            int temp = 0;

            cmd = new SqlCommand("SELECT * FROM CHARACTERS;", connection);
            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                temp++;
            }
            dataReader.Close();
            return temp;
        }

        public String getNickname_email(String email)
        {
            string nick = "";
            List<User> lista = this.UserList();
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista.ElementAt(i).email == email)
                { nick = lista.ElementAt(i).nickname; }
            }
            return nick;
        }

        public string getType_User(int id_user)
        {
            String type = "";
            List<User> lista = this.UserList();
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista.ElementAt(i).id == id_user)
                { type = lista.ElementAt(i).type; }
            }
            return type;
        }
        public int getId(String email)
        {
            int id = 0;
            List<User> lista = this.UserList();
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista.ElementAt(i).email == email)
                { id = lista.ElementAt(i).id; }
            }
            return id;
        }

        public SqlDataReader getLogin(String email, String pass)
        {
            
            cmd = new SqlCommand("SELECT email, password FROM users where email='" + email + "' and password='" + pass + "'", connection);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        /// <summary>
        /// The method terminating the connection to the database.
        /// </summary>
      

        public List<User> UserList()
        {
            List<User> list = new List<User>();

            cmd = new SqlCommand("SELECT * FROM USERS;", connection);
            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                list.Add(new User(dataReader));
            }
            dataReader.Close();
            return list;
        }
    }
}