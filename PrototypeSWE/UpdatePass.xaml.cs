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
        // update user password
        private void Updatepassword(object sender, RoutedEventArgs e)
        {
            username = UserName.Text;
            seqanswer = SeqAnswer.Text;
            newpass = NewPass.Password;
             Renter = RnewPass.Password;
            // regex to match user password
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(seqanswer) && !String.IsNullOrEmpty(newpass) && !String.IsNullOrEmpty(Renter))
            {

                if (newpass == Renter)
                {
                    if (Regex.IsMatch(newpass, pattern))
                    {
                        var same = UpdatesPass.CheckAnswer(username, seqanswer);
                        int ans1 = same.Item1;
                        string ans2 = same.Item2;
                        if (ans1 != 0)
                        {
                            bool update = UpdatesPass.Updatetable(newpass, username, ans2);
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
                                notuser.Content = "invalid username";

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
            else
            {
                wrngpass.Content = "all fields are required";
            }
           
        }

        
    }
}
