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

namespace PrototypeSWE
{
    /// <summary>
    /// Interaction logic for loginScreen.xaml
    /// </summary>
    public partial class loginScreen : Window
    {
        private string Username;
        private string Password;
        private int ID;
        private Security Securelogin;
        private MainWindow dashboard;
        private CreateAccount CA;
        private UpdatePass fgpass;
        
        public loginScreen()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        /* checks if the user is a valid user*/
        private void Login(object sender, RoutedEventArgs e)
        {
           
            Username = txtUser.Text;
            Password = txtpass.Password;
            string message = "";
            Securelogin = new Security();
            ID = Securelogin.GetUserIdByUsernameAndPassword(Username, Password);
            if (!String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password))
            {
                if (ID == 0)
                {
                    message = "invalid username or password";
                    loginError.Content = message;
                    txtpass.Clear();
                    txtUser.Clear();
                }
                else
                {

                    Properties.Settings.Default.userset = txtUser.Text;
                    dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
            }
            else
            {
                loginError.Content = "username or password is empty";
            }
            
        }
        /*sends the user to the Create user page*/
        public void CallCreate(object sender, RoutedEventArgs e)
        {
            CA = new CreateAccount();
            CA.Show();
            this.Close();
        }
        /*sends the user to the forgot password page*/
        public void ForgotPAss(object sender, RoutedEventArgs e)
        {
            fgpass = new UpdatePass();
            fgpass.Show();
            this.Close();
        }
      
       

    }
}
