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
        public loginScreen()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUser.Text;
            string password = txtpass.Password;
            int id = Security.GetUserIdByUsernameAndPassword(username, password);
            
            if(id == 0)
            {
                loginError.Content = " invalid username or password";
                txtpass.Clear();
                txtUser.Clear();
            }
            else
            {
                MainWindow dashboard = new MainWindow();
                dashboard.Show();
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateAccount CA = new CreateAccount();
            CA.Show();
            this.Close();
        }

        private void Click_Handler(object sender, RoutedEventArgs e)
        {

        }
    }
}
