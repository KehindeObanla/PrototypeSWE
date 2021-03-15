using System;
using System.Collections.Generic;
using System.Drawing;
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


namespace PrototypeSWE
{
    /// <summary>
    /// Interaction logic for BAWindow.xaml
    /// </summary>
    public partial class BAWindow : Window
    {
        private MainWindow mw;
        List<Button> coloredButtons = new List<Button>();
        public BAWindow()
        {
            InitializeComponent();
           
        }
        /* this function redirexts the user to the main window*/
        private void BacktoMainWindow(object sender, RoutedEventArgs e)
        {
            mw = new MainWindow();
            mw.Show();
            this.Close();
        }
        /* THIS FUNCTION TAKES A SCREENSHOT OF 
         * THE USERS SCRREN AND STORES IT IN THE DESKTOP*/
        private void DownloadBA(object sender, RoutedEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            double screenLeft = SystemParameters.VirtualScreenLeft;
            double screenTop = SystemParameters.VirtualScreenTop;
            double screenWidth = SystemParameters.VirtualScreenWidth;
            double screenHeight = SystemParameters.VirtualScreenHeight;

            using (Bitmap bmp = new Bitmap((int)screenWidth,
                (int)screenHeight))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    String filename = "MajorMap-" + DateTime.Now.ToString("ddMMyyyy-hhmmss") + ".png";
                    Opacity = .0;
                    g.CopyFromScreen((int)screenLeft, (int)screenTop, 0, 0, bmp.Size);
                    bmp.Save(path + "\\" + filename);
                    Opacity = 1;
                }

            }
            MessageBox.Show("ScreenShot on Desktop");
        }
        
        private void checkbuttons()
        {
            if (coloredButtons.Count != 0)
            {
                foreach (var button in coloredButtons)
                {
                    
                    button.Background = new SolidColorBrush(Colors.Gainsboro);
                    
                }

            }
        }
        private void CMPS1044_Click(object sender, RoutedEventArgs e)
        {
            checkbuttons();
            CMPS1044Popup.IsOpen = true;
            CMPS1044.Background = new SolidColorBrush(Colors.SaddleBrown);
            MATH1534.Background = new SolidColorBrush(Colors.Yellow);
            MATH1233.Background = new SolidColorBrush(Colors.Yellow);
            CMPS1063.Background = new SolidColorBrush(Colors.Aqua);
            CMPS2084.Background = new SolidColorBrush(Colors.Aqua);
            coloredButtons.Add(MATH1233);
            coloredButtons.Add(MATH1534);
            coloredButtons.Add(CMPS1063);
            coloredButtons.Add(CMPS2084);
            coloredButtons.Add(CMPS1044);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
        }

        private void CMPS1063__Click(object sender, RoutedEventArgs e)
        {
            checkbuttons();
            CMPS1063Popup.IsOpen = true;
            CMPS1063.Background = new SolidColorBrush(Colors.SaddleBrown);
            CMPS1044.Background = new SolidColorBrush(Colors.Green);
            CMPS2143.Background = new SolidColorBrush(Colors.Aqua);
            coloredButtons.Add(CMPS1063);
            coloredButtons.Add(CMPS1044);
            coloredButtons.Add(CMPS2143);


        }

        private void EditBA1_Click(object sender, RoutedEventArgs e)
        {
            SaveEdit.Visibility= Visibility.Visible;
        }
    }
}
