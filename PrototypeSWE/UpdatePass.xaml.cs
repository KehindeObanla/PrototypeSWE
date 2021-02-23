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
using System.Text.RegularExpressions;

namespace PrototypeSWE
{
    /// <summary>
    /// Interaction logic for UpdatePass.xaml
    /// </summary>
    public partial class UpdatePass : Window
    {
        private string username;
        private string seqanswer;
        private string newpass;
        private string Renter;
        private Security UpdatesPass = new Security();
        private loginScreen lg;

        public UpdatePass()
        {
            InitializeComponent();
        }

        private void Updatepassword(object sender, RoutedEventArgs e)
        {
            username = UserName.Text;
            seqanswer = SeqAnswer.Text;
            newpass = NewPass.Text;
             Renter = RnewPass.Text;
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
            if (newpass == Renter)
            {
                if (Regex.IsMatch(newpass, pattern))
                {
                    int same = UpdatesPass.CheckAnswer(username, seqanswer);
                    if (same != 0)
                    {
                        bool update = UpdatesPass.Updatetable(newpass, username, same);
                        if (update == true)
                        {
                            wrngpass.Content = "password Updated";
                            UserName.Clear();
                            SeqAnswer.Clear();
                            NewPass.Clear();
                            RnewPass.Clear();
                             lg = new loginScreen();
                            lg.Show();
                            this.Close();
                        }
                        else
                        {
                            wrngpass.Content = "invalid username";

                        }
                    }
                    else
                    {
                        wrngpass.Content = "False Answer";
                    }
                }
                else
                {
                    wrngpass.Content = "Wrong Password Format";
                }
                   
            }
            else
            {
                wrngpass.Content = "Password is not same";
            }
           
        }

        
    }
}
