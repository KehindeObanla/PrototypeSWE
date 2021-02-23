﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace PrototypeSWE
{
    internal class Security
    {
        private  string connectString = Properties.Settings.Default.Connection_String;
        
        public  string HashSHA1(string value)
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
        public  bool AddUser(string username, string password, string Answer)
        {
            Guid userGuid = Guid.NewGuid();
            string hashedPassword = HashSHA1(password + userGuid.ToString());
            string hashedseq = HashSHA1(Answer + userGuid.ToString());
             SqlConnection con = new SqlConnection(connectString);
        string queryInsert = "INSERT INTO tbl_login ([Username],[Password], [UserGuid],[SecureAnswer])  VALUES (@username, @password,@userguid,@ans )";
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
        public  int GetUserIdByUsernameAndPassword(string username, string password)
        {
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
                        string hashedPassword = HashSHA1(password + dbUserGuid);

                        // if its correct password the result of the hash is the same as in the database
                        if (dbPassword == hashedPassword)
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
            // Return the user id which is 0 if we did not found a user.
            return userId;
        }
        public  int CheckAnswer(string username,string answer)
        {
            
            int userId = 0;
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
                        string dbUserGuid = Convert.ToString(dr["UserGuid"]);
                        string dbSeqAnswer = Convert.ToString(dr["SecureAnswer"]);
                        string hashedans = HashSHA1(answer + dbUserGuid);
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
            return userId;
        }
        public  bool Updatetable (string newpass,string username,int id)
        {
            SqlConnection con = new SqlConnection(connectString);
            string hashedans = HashSHA1(newpass + id);
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE  [tbl_login] SET Password = @Password WHERE Username = @username", con))
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

    }
}
