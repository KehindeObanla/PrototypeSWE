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

        private void Login(object sender, RoutedEventArgs e)
        {
            Username = txtUser.Text;
            Password = txtpass.Password;
            Securelogin = new Security();
            ID = Securelogin.GetUserIdByUsernameAndPassword(Username, Password);
            
            if(ID == 0)
            {
                loginError.Content = " invalid username or password";
                txtpass.Clear();
                txtUser.Clear();
            }
            else
            {
                dashboard = new MainWindow();
                dashboard.Show();
                this.Close();
            }
        }

        private void CallCreate(object sender, RoutedEventArgs e)
        {
            CA = new CreateAccount();
            CA.Show();
            this.Close();
        }

        private void ForgotPAss(object sender, RoutedEventArgs e)
        {
            fgpass = new UpdatePass();
            fgpass.Show();
            this.Close();
        }
    }
}
