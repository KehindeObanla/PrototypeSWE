using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace PrototypeSWE
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
        private static string connectString = Properties.Settings.Default.Connection_String;
        private string Username;
        private string Password1;
        private string Password2;
        private string Ans1;
        private Security checkSec = new Security();
        private loginScreen lg;

        public CreateAccount()
        {
            InitializeComponent();
        }
       
        /*THIS  functions checks the usesername Exist
         * checks if the password matches the pattern
         * checks if the two passwords are the same
         * then it adds the user to the database
         * and goes to th elogin page
         */
        private void CreateUserAccount(object sender, RoutedEventArgs e)
        {
             Password1 = txtpass.Password;
             Password2 = confirmPass.Password;
             Username = txtUser.Text;
             Ans1 = seqAns.Text;
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
            
            if (Password1 == Password2)
            {
                if(Regex.IsMatch(Password1, pattern))
                {
                    int id = checkSec.GetUserIdByUsernameAndPassword(Username, Password1);
                    if (id == 0)
                    {
                        checkSec.AddUser(Username, Password1, Ans1);
                         lg = new loginScreen();
                        lg.Show();
                        this.Close();
                    }
                    else
                    {
                        ExistUser.Content = " UserName  Exists";
                        txtUser.Clear();
                        confirmPass.Clear();
                        txtpass.Clear();
                    }
                }
                else
                {
                    PassFormat.Content = " Wrong  Password Format";
                    confirmPass.Clear();
                    txtpass.Clear();
                }
                

            }
            else
            {

                incorrect.Content = "password not the same";
                txtUser.Clear();
                confirmPass.Clear();
                txtpass.Clear();
            }




        }

       
    }
}
