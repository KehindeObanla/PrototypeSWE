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
        /* TO CREATE USER MENU COUNT*/
        public Tuple<string, string> createtCOUNT()
        {
            var value = new Dictionary<string, string>(){
            {"CAT", "0"},{"AHistoryT", "0"},{"GPST", "0"},{"SBST", "0"},
            {"CommT", "0"},{"LPST", "0"},{"LPCT", "0"},{"UICT", "0"},{"CGUT", "0"}
            };
            var value1 = new Dictionary<string, string>(){
            {"CAT", "0"},{"AHistoryT", "0"},{"GPST", "0"},{"SBST", "0"},
            {"CommT", "0"},{"LPST", "0"},{"LPCT", "0"},{"UICT", "0"},{"CGUT", "0"},
                {"ADDRT","0"},{"MMT","0" }
            };
            string jsonsavedba = JsonConvert.SerializeObject(value, Formatting.Indented);
            string jsonsavedbs = JsonConvert.SerializeObject(value1, Formatting.Indented);
            var ans = Tuple.Create(jsonsavedba, jsonsavedbs);
            return ans;
        }
        /* create a new user setting for ba and bs*/
        public Tuple<string, string> createtemplatenew()
        {
            var value = new Dictionary<string, bool>(){
            {"CMPS3233", true},{"CMPS2433", true},{"CMPS1044", true},{"CMPS2143", true},
            {"CMPS1063", true},{"CMPS4143", true},{"MATH1534", true},{"MATH1233", true},{"CMPS3023", true},{"CMPS4991", true},{"CMPS4103", true},
            {"CMPS4113", true},{"CMPS3013", true},{"CMPS2084", true},{"CSAE3", true},{"CSAE6", true},{"CSAE1", true},
            {"CSAE7", true},{"CSAE4", true},{"CSAE2", true},{"CSAE5", true},{"MATH1443", true},{"STAT3573", true},{"Communication", true},
            {"LPS", true},{"LPC", true},{"CreativeArts", true},{"AHistory", true},{"GPS", true},{"SBS", true},{"CGU", true},
            {"UIC", true}
            };

            var value2 = new Dictionary<string, bool>(){
            {"CMPS3233", true},{"CMPS2433", true},{"CMPS1044", true},{"CMPS2143", true},
            {"CMPS1063", true},{"CMPS4143", true},{"MATH1534", true},{"MATH1233", true},{"CMPS3023", true},
            {"CMPS4991", true},{"CMPS4103", true},{"CMPS4113", true},{"CMPS3013", true},{"CMPS2084", true},
            {"CSAE3", true},{"CSAE6", true},{"CSAE1", true},{"CSAE7", true},{"CSAE4", true},
            {"CSAE2", true},{"CSAE5", true},{"MATH1443", true},{"STAT3573", true},{"Communication", true},
            {"LPS", true},{"LPC", true},{"CreativeArts", true},{"AHistory", true},{"GPS", true},{"SBS", true},
            {"CGU", true},{"UIC", true},{"ADDR", true},{"MM", true}};
            string jsonsavedba = JsonConvert.SerializeObject(value, Formatting.Indented);
            string jsonsavedbs = JsonConvert.SerializeObject(value2, Formatting.Indented);
            var ans = Tuple.Create(jsonsavedba, jsonsavedbs);
            return ans;
        }
        //Adds a user to the database
        public  bool AddUser(string username, string password, string Answer)
        {
            Guid userGuid = Guid.NewGuid();
            string guid = userGuid.ToString().ToUpper();
            string hashedPassword = Hashvalue(password + guid);
            string hashedseq = Hashvalue(Answer + guid);
            var babs = createtemplatenew();
            var baset = babs.Item1;
            var bsset = babs.Item2;
            var dbas = createtCOUNT();
            var dba = dbas.Item1;
            var dbs = dbas.Item2;

             SqlConnection con = new SqlConnection(connectString);
        string queryInsert = "INSERT INTO tbl_login ([username],[Password], [UserGuid],[SecureAnswer],[settingsBA],[settings],[DataBa],[DataBs])  VALUES (@username, @password,@userguid,@ans,@ba,@bs,@dba,@dbs )";
            try
            {
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", hashedPassword); // store the hashed value
                    cmd.Parameters.AddWithValue("@userguid", userGuid); // store the Guid
                    cmd.Parameters.AddWithValue("@ans", hashedseq);
                    cmd.Parameters.AddWithValue("@ba", baset);
                    cmd.Parameters.AddWithValue("@bs", bsset);
                    cmd.Parameters.AddWithValue("@dba", dba);
                    cmd.Parameters.AddWithValue("@dbs", dbs);

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
        public void updatesavedsettingBs(string settingssave,string username,string dbs,string mbs)
        {
            SqlConnection con = new SqlConnection(connectString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE  [tbl_login] SET settings = @settingssave,DataBs = @dbs, markedBs = @mbs WHERE Username = @username", con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@settingssave", settingssave);
                    cmd.Parameters.AddWithValue("@dbs", dbs);
                    cmd.Parameters.AddWithValue("@mbs", mbs);
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
        public void updatesavedsettingBA(string settingssaveBA, string username,string dba,string mba)
        {
            SqlConnection con = new SqlConnection(connectString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE  [tbl_login] SET settingsBA = @settingssaveBA,DataBa=@dba,markedBA=@mba WHERE username = @username", con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@settingssaveBA", settingssaveBA);
                    cmd.Parameters.AddWithValue("@dba", dba);
                    cmd.Parameters.AddWithValue("@mba", mba);
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
        public Tuple<string,string> getusersettingsbs(string username)
        {
           
            
            string bs = "";
            string dbs = "";
            string mbs = "";
            SqlConnection con = new SqlConnection(connectString);
            try
            {
                string query = "SELECT settings, settingsBA,DataBa,DataBs,markedBs FROM [tbl_login] WHERE username=@username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // dr.Read() = we found user(s) with matching username!
                       
                       bs= Convert.ToString(dr["settings"]);
                       dbs = Convert.ToString(dr["DataBs"]);
                        mbs = Convert.ToString(dr["markedBs"]);

                    }
                    con.Close();
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
            Properties.Settings.Default.bsSetting= dbs;
            Properties.Settings.Default.bslist = mbs;
           var ans = Tuple.Create(dbs, bs);
            return ans;
            
        }
        public Tuple<string, string> getusersettingsBa(string username)
        {

            string ba = "";
            string dba = "";
            string mba = "";
           
            SqlConnection con = new SqlConnection(connectString);
            try
            {
                string query = "SELECT settings, settingsBA,DataBa,DataBs,markedBA FROM [tbl_login] WHERE username=@username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // dr.Read() = we found user(s) with matching username!
                        ba = Convert.ToString(dr["settingsBA"]);
                        dba = Convert.ToString(dr["DataBa"]);
                        mba = Convert.ToString(dr["markedBA"]);


                    }
                    con.Close();
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            Properties.Settings.Default.BaSetting = dba;
            Properties.Settings.Default.balist = mba;
            var ans = Tuple.Create(ba, dba);
            return ans;

        }

    }
}
