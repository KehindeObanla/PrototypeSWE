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
        
        private string Username { get; set; }
        private string Password1 { get; set; }
        private string Password2 { get; set; }
        private string Ans1 { get; set; }
        private Security checkSec = new Security();
        private loginScreen lg;

        public CreateAccount()
        {
            InitializeComponent();
        }

       
        /*check if the two passwords are the same*/
        private void CreateUserAccount(object sender, RoutedEventArgs e)
        {

            Password1 = txtpass.Password;
            Password2 = confirmPass.Password;
            Username = txtUser.Text;
            Ans1 = seqAns.Text;
            if(!String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password1) && !String.IsNullOrEmpty(Password2) && !String.IsNullOrEmpty(Ans1))
            {
                if (Password1 == Password2)
                {
                    checkFormat(Password1, Username);
                }
                else
                {

                    incorrect.Content = "password not the same";
                    txtUser.Clear();
                    confirmPass.Clear();
                    txtpass.Clear();

                }
            }
            else
            {
                incorrect.Content = "all fields are required";
            }
           
        }
       
       /* check password format
        password must have 1 lower case,1 uppercase,
        1 number and between 8-15 characters long*/
        private void checkFormat(string Password1,string username)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
            if (Regex.IsMatch(Password1, pattern))
            {
                checkUser(username, Password1);
               
            }
            else
            {
                PassFormat.Content = " Wrong  Password Format";
                confirmPass.Clear();
                txtpass.Clear();
            }
        }
        /* check if user exist in database*/
        private void checkUser(string Username,string Password1)
        {
            bool id = checkSec.prsentuser(Username);
            if (id == false)
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
       

        private void Login(object sender, RoutedEventArgs e)
        {
            lg = new loginScreen();
            lg.Show();
            this.Close();
        }
    }
}
