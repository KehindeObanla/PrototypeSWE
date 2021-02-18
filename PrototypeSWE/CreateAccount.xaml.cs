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
        public CreateAccount()
        {
            InitializeComponent();
        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string pass1 = txtpass.Password;
            string pass2 = confirmPass.Password;
            string username = txtUser.Text;
            string seq = seqAns.Text;
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
            
            if (pass1 == pass2)
            {
                if(Regex.IsMatch(pass1, pattern))
                {

                    int id = Security.GetUserIdByUsernameAndPassword(username, pass1);
                    if (id == 0)
                    {
                        Security.AddUser(username, pass1, seq);
                        loginScreen lg = new loginScreen();
                        lg.Show();
                        this.Close();
                    }
                    else
                    {
                        ExistUser.Content = " UserName  Exists";
                    }
                }
                else
                {
                    ExistUser.Content = " Wrong  Password Format";
                }
                

            }
            else
            {
                incorrect.Content = "password not the same";
                txtUser.Text = "";
                txtUser.Clear();
                confirmPass.Clear();
                txtpass.Clear();
            }




        }

       
    }
}
