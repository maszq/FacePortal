using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FacePortal
{
    public class SQLDatabase
    {
        /// <summary>
        /// Klasa odpowiedzialna za utworzenie i utrzymanie połaczenia z bazą danych.
        /// Zawiera metody dodawania, edycji i usuwania danych.
        /// IPrzechowuje również funkcje, które tworzą listy obiektów z bazy danych.
        /// </summary>
        private SqlConnection connection;
        private SqlCommand cmd;
       
        public SQLDatabase()
        {
            Init();
        }

        /// <summary>
        /// Metoda tworząca connectionString.
        /// </summary>
        private void Init()
        {
            connection = new SqlConnection("Data Source=fp-server.database.windows.net;" +
                "Initial Catalog=fp-database;User ID=fp-admin;Password=Cebula1.");
        }

        /// <summary>
        /// Metoda inicjująca połaczenie z bazą danych.
        /// </summary>
        public void Connect()
        {
            connection.Open();
        }

        public void Disconnect()
        {
            connection.Close();
        }
        /// <summary>
        /// Metoda dodająca użytkownika do bazy danych.
        /// </summary>
        /// <param name="nickname"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="sex"></param>
        /// <param name="type"></param>
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

        public void InsertResult(int id_user, int id_celebrite, int result, bool ranking)
        {
            int ranking_int = 0;
            if (ranking) ranking_int = 1;
            string query =
                "INSERT INTO RESULT VALUES " +
                "("
                + id_user + ", "
                + id_celebrite + ", "
                + result + ", "
                + ranking_int + ");";

            cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Metoda obsługująca edycje hasła użytkownika.
        /// </summary>
        /// <param name="id_u"></param>
        public void ResetPassword(int id_u)
        {
            string query =
                "UPDATE USERS SET "
                + "password='password'"
                + " WHERE id=" + id_u;

            cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Funkcja zwracająca email użytkownika na podstawie jego id.
        /// </summary>
        /// <param name="id_u"></param>
        /// <returns></returns>
        public string getEmail(int id_u)
        {
            String email = "";
            List<User> lista = this.UserList();
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista.ElementAt(i).id == id_u)
                { email = lista.ElementAt(i).email; }
            }
            return email;
        }

        /// <summary>
        /// Funkcja zwracająca nickname użytkownika na podstawie jego id.
        /// </summary>
        /// <param name="id_u"></param>
        /// <returns></returns>
        public String getNickname_id(int id_u)
        {
            string nick = "";
            List<User> lista = this.UserList();
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista.ElementAt(i).id == id_u)
                { nick = lista.ElementAt(i).nickname; }
            }
            return nick;
        }

        /// <summary>
        /// Metoda dodająca cechy użytkownika. Domyślnie równe -1, silnik po wyekstrachowaniu je aktualizuje.
        /// </summary>
        public void InsertCharacter()
        {
            string query =
                "INSERT INTO CHARACTERS VALUES " +
                "(-1, -1, -1,-1, -1, -1,-1, -1, -1,-1, -1, -1,-1, -1, -1,-1,-1);";

            cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
        }

        public int AmountCharacters()
        {
            int temp = 0;

            cmd = new SqlCommand("SELECT * FROM CHARACTERS;", connection);
            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            { temp++; }
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
        /// Funkcja zwracająca listę obiektów User.
        /// </summary>
        /// <returns></returns>
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
    
        public List<Character> CharacterList()
        {
            List<Character> list = new List<Character>();

            cmd = new SqlCommand("SELECT * FROM CHARACTERS;", connection);
            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                list.Add(new Character(dataReader));
            }
            dataReader.Close();
            return list;
        }

        public void EditAccount(int id, String nickname, String email , String name, String surname , String sex )
        {
            string query =
                "UPDATE USERS SET "
                + "nickname='" + nickname + "', email='"
                + email + "', name='"
                + name + "', surname='"
                + surname + "', sex='"
                + sex + "'  WHERE id=" + id + ";";

            cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
        }

        public void EditPassword(int id, String new_password)
        {
            string query =
                "UPDATE USERS SET "
                + "password='" + new_password + "' WHERE id=" + id + ";";

            cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
        }

        public List<UserAlbum> UserAlbum()
        {
            List<UserAlbum> list = new List<UserAlbum>();

            cmd = new SqlCommand("SELECT * FROM USER_ALBUM;", connection);
            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                list.Add(new UserAlbum(dataReader));
            }
            dataReader.Close();
            return list;
        }

        public List<CelebriteAlbum> CelebriteAlbum()
        {
            List<CelebriteAlbum> list = new List<CelebriteAlbum>();

            cmd = new SqlCommand("SELECT * FROM CELEBRITE_ALBUM;", connection);
            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                list.Add(new CelebriteAlbum(dataReader));
            }
            dataReader.Close();
            return list;
        }
    }
}