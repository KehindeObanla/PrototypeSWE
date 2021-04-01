using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Newtonsoft.Json;


namespace PrototypeSWE
{
    internal class Security
    {
        //database connection string
        private  string connectString = @"Data Source=DESKTOP-R0I7FGT\SQLEXPRESS;Initial Catalog=Advisingtool;Integrated Security=True";
        //creates a hasvalue given a string
        public  string Hashvalue(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        //Adds a user to the database
        public  bool AddUser(string username, string password, string Answer)
        {
            Guid userGuid = Guid.NewGuid();
            string guid = userGuid.ToString().ToUpper();
            string hashedPassword = Hashvalue(password + guid);
            string hashedseq = Hashvalue(Answer + userGuid.ToString());
             SqlConnection con = new SqlConnection(connectString);
        string queryInsert = "INSERT INTO tbl_login ([username],[Password], [UserGuid],[SecureAnswer])  VALUES (@username, @password,@userguid,@ans )";
            try
            {
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", hashedPassword); // store the hashed value
                    cmd.Parameters.AddWithValue("@userguid", userGuid); // store the Guid
                    cmd.Parameters.AddWithValue("@ans", hashedseq);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;

        }
        public bool prsentuser(string username) 
        {
            // this is the value we will return
            bool user = false;
            SqlConnection con = new SqlConnection(connectString);
            try
            {
                string query = "SELECT Username FROM[tbl_login] WHERE username = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        user = true;
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            // Return the user id which is 0 if we did not found a user.
            return user;
        }
    
        //checks if a user is in the database
        public  int GetUserIdByUsernameAndPassword(string username, string password)
        {
            Console.WriteLine("useid", username);
            Console.WriteLine("pass", password);
            // this is the value we will return
            int userId = 0;
            SqlConnection con = new SqlConnection(connectString);
            try
            {
                string query = "SELECT Id, Password, UserGuid FROM[tbl_login] WHERE username = @username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // dr.Read() = we found user(s) with matching username!

                        int dbUserId = Convert.ToInt32(dr["Id"]);
                        string dbPassword = Convert.ToString(dr["Password"]);
                        string dbUserGuid = Convert.ToString(dr["UserGuid"]);
                       
                        // Now we hash the UserGuid from the database with the password we wan't to check
                        // In the same way as when we saved it to the database in the first place. (see AddUser() function)
                        string hashedPassword = Hashvalue(password + dbUserGuid);

                        // if its correct password the result of the hash is the same as in the database
                         if (dbPassword == hashedPassword.ToUpper())
                        {
                            Console.WriteLine("i was here");
                            // The password is correct
                            userId = dbUserId;
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            // Return the user id which is 0 if we did not found a user.
            return userId;
        }
        //checks if the sequrity question is correct
        public  Tuple<int,string> CheckAnswer(string username,string answer)
        {
            
            int userId = 0;
            string dbUserGuid ="";
            SqlConnection con = new SqlConnection(connectString);
            try
            {
                string query = "SELECT Id, Password, UserGuid,SecureAnswer FROM [tbl_login] WHERE username=@username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // dr.Read() = we found user(s) with matching username!

                        int dbUserId = Convert.ToInt32(dr["Id"]);
                        string dbPassword = Convert.ToString(dr["Password"]);
                         dbUserGuid = Convert.ToString(dr["UserGuid"]);
                        string dbSeqAnswer = Convert.ToString(dr["SecureAnswer"]);
                        string hashedans = Hashvalue(answer + dbUserGuid);
                        if (dbSeqAnswer == hashedans)
                        {
                            // The password is correct
                            userId = dbUserId;
                        }
                    }
                    con.Close();
                }
            }
           
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            var ans = Tuple.Create(userId, dbUserGuid);
            return ans;
        }
        public  bool Updatetable (string newpass,string username,string id)
        {
            SqlConnection con = new SqlConnection(connectString);
            string hashedans = Hashvalue(newpass + id);
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE  [tbl_login] SET Password = @Password WHERE username = @username", con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@Password", hashedans);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            return true;
        }
        public void updatesavedsettingBs(string settingssave,string username)
        {
            SqlConnection con = new SqlConnection(connectString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE  [tbl_login] SET settings = @settingssave WHERE Username = @username", con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@settingssave", settingssave);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }
        public void updatesavedsettingBA(string settingssaveBA, string username)
        {
            SqlConnection con = new SqlConnection(connectString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE  [tbl_login] SET settingsBA = @settingssaveBA WHERE username = @username", con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@settingssaveBA", settingssaveBA);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        public Tuple<string,string> getusersettingsBaandbs(string username)
        {
           
            string ba = "";
            string bs = "";
            SqlConnection con = new SqlConnection(connectString);
            try
            {
                string query = "SELECT settings, settingsBA FROM [tbl_login] WHERE username=@username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // dr.Read() = we found user(s) with matching username!
                       ba= Convert.ToString(dr["settingsBA"]);
                       bs= Convert.ToString(dr["settings"]);
                       
                    }
                    con.Close();
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            Properties.Settings.Default.BaSetting = ba;
            Properties.Settings.Default.bsSetting= bs;
            var ans = Tuple.Create(ba, bs);
            return ans;
            
        }
    }
}
