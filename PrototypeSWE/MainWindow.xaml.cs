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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrototypeSWE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenWindowBS(object sender, RoutedEventArgs e)
        {
            BSWindow bs = new BSWindow();
            bs.Show();
            this.Close();
           
        }

        private void OpenWindowBA(object sender, RoutedEventArgs e)
        {
            BAWindow ba = new BAWindow();
            ba.Show();
            this.Close();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
