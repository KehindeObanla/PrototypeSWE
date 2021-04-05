using Newtonsoft.Json;
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
    /// Interaction logic for BSWindow.xaml
    /// </summary>
    public partial class BSWindow : Window
    {
        private MainWindow mw;
        List<Button> coloredButtons = new List<Button>();
        string username = Properties.Settings.Default.userset;
        public static List<string> otherbuttons = new List<string>() { "DownloadBtnBS", "EditBS", "Backbtn", "SaveEdit" };
        public static Dictionary<string, bool> savedsettings = new Dictionary<string, bool>();
        public static List<string> menuitemnames = new List<string>() { "Communication", "LPSy", "CreativeArtsy", "AHistoryy", "GPSy",
        "SBSy","CGUy","UICy","MRlist","ADDrlist"};
        public static List<string> box = new List<string>();
        Security updatesettings;
        public BSWindow()
        {
            InitializeComponent();
        }
        /* this function redirexts the user to the main window*/

        private void BsbackTomainWindow(object sender, RoutedEventArgs e)
        {
             mw = new MainWindow();
            mw.Show();
            this.Close();
        }
        /* THIS FUNCTION TAKES A SCREENSHOT OF 
         * THE USERS SCRREN AND STORES IT IN THE DESKTOP*/
        private void DownloadBS(object sender, RoutedEventArgs e)
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
        /*this function changes user button to default color
         */
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
        /*this function changes the color of preq and 
         * future classes of CMPS1044
         */
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


        /*this function changes the color of preq and 
         * future classes of CMPS1063
         */
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
            /*this fuction makes drop down menus clickabel
             */
        private void makecheckable(List<MenuItem> list, bool check = true)
        {
            if (check == true)
            {
                foreach (MenuItem item in list)
                {
                    item.IsCheckable = true;
                }
            }
            else
            {
                foreach (MenuItem item in list)
                {
                    item.IsCheckable = false;
                }
            }

        }
        /*
         *  if a menus has 2 clicked menuitem it disables the menu
         */

        private void checkitem(List<IEnumerable<MenuItem>> j, MenuItem lPS)
        {
            int countedcheckbox = 0;
            foreach (MenuItem item in j)
            {

                if (item.IsChecked)
                {
                    countedcheckbox++;
                }
            }
            if (countedcheckbox >= 2)
            {
                lPS.IsEnabled = false;
            }
        }
        /*
        *  if a menus has 2 clicked menuitem it disables the menu
        */
        private void checkitem(List<MenuItem> list, MenuItem tolock)
        {
            int countedcheckbox = 0;
            foreach (MenuItem item in list)
            {
                if (item.IsChecked)
                {
                    countedcheckbox++;
                }
            }
            if (countedcheckbox >= 2)
            {
                tolock.IsEnabled = false;
            }


        }
            /* this fuction saves the user  button settings to a dictionary
             */
        public static void savesetting(List<Button> buttonlist)
        {
            foreach (var item in buttonlist)
            {
                if (!otherbuttons.Contains(item.Name))
                {

                    if (savedsettings.ContainsKey(item.Name))
                    {
                        if (box.Contains(item.Name))
                        {
                            savedsettings[item.Name] = false;
                        }
                        else
                        {
                            savedsettings[item.Name] = true;
                        }
                    }
                    else
                    {
                        if (box.Contains(item.Name))
                        {
                            savedsettings.Add(item.Name, false);
                        }
                        else
                        {
                            savedsettings.Add(item.Name, true);
                        }
                    }


                }
            }
        }
        /* this fuction saves the user  menu settings to a dictionary
          */
        public static void savemenuitem(List<Menu> menus)
        {
            foreach (var item in menus)
            {
                var itemlist = item.Items.OfType<MenuItem>().ToList();
                if (savedsettings.ContainsKey(itemlist[0].Name))
                {
                    if (!itemlist[0].IsEnabled)
                    {
                        savedsettings[itemlist[0].Name]= false;
                    }
                    else
                    {
                        savedsettings[itemlist[0].Name] = true;
                    }
                }
                else
                {
                    if (!itemlist[0].IsEnabled)
                    {
                        savedsettings.Add(itemlist[0].Name, false);
                    }
                    else
                    {
                        savedsettings.Add(itemlist[0].Name, true);
                    }
                }
                
               
            }
        }
        /* this fuction gets user setting and Serialize
         * the dictionary
         */
        public string get_BsButtons_menu()
        {
           
            var buttonlist = getbtn.Children.OfType<Button>().ToList();
            var menus = getbtn.Children.OfType<Menu>().ToList();
            savesetting(buttonlist);
            savemenuitem(menus);
            string jsonsavedbs = JsonConvert.SerializeObject(savedsettings, Formatting.Indented);
            return jsonsavedbs;


        }
            /* this fucion checks the buttons that
             * need to be diabled
             */
        public void checkedboxes()
        {
            var checkboxes = getbtn.Children.OfType<CheckBox>().ToList();
            foreach (var item in checkboxes)
            {
                if (item.IsChecked == true)
                {
                    int length = item.Name.Length;
                    string name = item.Name.Remove(length - 3, 3);
                    box.Add(name);
                }
            }
        }
        /*this function changes the color of preq and 
         * future classes of  MATH1534
         */
        private void MATH1534_Click(object sender, RoutedEventArgs e)
        {
            checkbuttons();
            MATH1534Popup.IsOpen = true;
        }
        /*this function changes the color of preq and 
         * future classes of  MATH1233
         */
        private void MATH1233_Click(object sender, RoutedEventArgs e)
        {
            checkbuttons();
            MATH1233Popup.IsOpen = true;
        }
        /*this function changes the color of preq and 
         * future classes of   MATH1443
         */
        private void MATH1443_Click(object sender, RoutedEventArgs e)
        {
            checkbuttons();
            MATH1443Popup.IsOpen = true;
            MATH1443.Background = new SolidColorBrush(Colors.Brown);
            MATH1233.Background = new SolidColorBrush(Colors.Yellow);
            coloredButtons.Add(MATH1443);
            coloredButtons.Add(MATH1233);
        }
        /*this function changes the color of preq and 
       * future classes of  CMPS4143
       */
        private void CMPS4143_Click(object sender, RoutedEventArgs e)
        {
            checkbuttons();
            CMPS4143Popup.IsOpen = true;
            CMPS4143.Background = new SolidColorBrush(Colors.Brown);
            CMPS2143.Background = new SolidColorBrush(Colors.Yellow);
            coloredButtons.Add(CMPS4143);
            coloredButtons.Add(CMPS2143);
        }
        /*this function changes the color of preq and 
        * future classes of   CMPS2143
        */
        private void CMPS2143_Click(object sender, RoutedEventArgs e)
        {
            checkbuttons();
            CMPS2143Popup.IsOpen = true;
            CMPS2143.Background = new SolidColorBrush(Colors.Brown);
            CMPS1063.Background = new SolidColorBrush(Colors.Yellow);
            CMPS4103.Background = new SolidColorBrush(Colors.Aqua);
            CMPS4143.Background = new SolidColorBrush(Colors.Aqua);
            coloredButtons.Add(CMPS2143);
            coloredButtons.Add(CMPS1063);
            coloredButtons.Add(CMPS4103);
            coloredButtons.Add(CMPS4143);
        }
        /*this function changes the color of preq and 
       * future classes of    CMPS4113
       */
        private void CMPS4113_Click(object sender, RoutedEventArgs e)
        {
            checkbuttons();
            CMPS4113Popup.IsOpen = true;
            CMPS4113.Background = new SolidColorBrush(Colors.Brown);
            CMPS3013.Background = new SolidColorBrush(Colors.Yellow);
            CMPS2143.Background = new SolidColorBrush(Colors.Yellow);
            coloredButtons.Add(CMPS4113);
            coloredButtons.Add(CMPS3013);
            coloredButtons.Add(CMPS2143);
        }
        /*this function changes the color of preq and 
     * future classes of    CMPS2084
     */
        private void CMPS2084_Click(object sender, RoutedEventArgs e)
        {
            checkbuttons();
            CMPS2084Popup.IsOpen = true;
            CMPS2084.Background = new SolidColorBrush(Colors.Brown);
            CMPS1044.Background = new SolidColorBrush(Colors.Yellow);
            CMPS3023.Background = new SolidColorBrush(Colors.Aqua);
            CMPS4103.Background = new SolidColorBrush(Colors.Aqua);
            coloredButtons.Add(CMPS2084);
            coloredButtons.Add(CMPS1044);
            coloredButtons.Add(CMPS3023);
            coloredButtons.Add(CMPS4103);

        }
        /*this function changes the color of preq and 
         * future classes of    CMPS2433
        */
        private void CMPS2433_Click(object sender, RoutedEventArgs e)
        {
            checkbuttons();
            CMPS2433Popup.IsOpen = true;

            CMPS2433.Background = new SolidColorBrush(Colors.Brown);
            CMPS1063.Background = new SolidColorBrush(Colors.Yellow);
            CMPS3013.Background = new SolidColorBrush(Colors.Aqua);
            coloredButtons.Add(CMPS2433);
            coloredButtons.Add(CMPS1063);
            coloredButtons.Add(CMPS3013);

        }
        /*this function changes the color of preq and 
         * future classes of    CMPS3233
        */
        private void CMPS3233_Click(object sender, RoutedEventArgs e)
        {
            checkbuttons();
            CMPS3233Popup.IsOpen = true;
            CMPS3233.Background = new SolidColorBrush(Colors.Brown);
            CMPS2433.Background = new SolidColorBrush(Colors.Yellow);
            coloredButtons.Add(CMPS3233);
            coloredButtons.Add(CMPS2433);
        }
        /*this function changes the color of preq and 
         * future classes of   CMPS3013
        */
        private void CMPS3013_Click(object sender, RoutedEventArgs e)
        {
            checkbuttons();
            CMPS3013Popup.IsOpen = true;
            CMPS3013.Background = new SolidColorBrush(Colors.Brown);
            CMPS2433.Background = new SolidColorBrush(Colors.Yellow);
            CMPS4103.Background = new SolidColorBrush(Colors.Aqua);
            CMPS4113.Background = new SolidColorBrush(Colors.Aqua);
            coloredButtons.Add(CMPS3013);
            coloredButtons.Add(CMPS2433);
            coloredButtons.Add(CMPS4103);
            coloredButtons.Add(CMPS4113);
        }
        /*this function changes the color of preq and 
        * future classes of  CMPS3023
        */
        private void CMPS3023_Click(object sender, RoutedEventArgs e)
        {
            checkbuttons();
            CMPS3023Popup.IsOpen = true;
            CMPS3023.Background = new SolidColorBrush(Colors.Brown);
            CMPS2084.Background = new SolidColorBrush(Colors.Yellow);
            coloredButtons.Add(CMPS3023);
            coloredButtons.Add(CMPS2084);

        }
        /*this function changes the color of preq and 
       * future classes of CMPS4103
       */
        private void CMPS4103_Click(object sender, RoutedEventArgs e)
        {
            checkbuttons();
            CMPS4103Popup.IsOpen = true;
            CMPS4103.Background = new SolidColorBrush(Colors.Brown);
            CMPS2084.Background = new SolidColorBrush(Colors.Yellow);
            coloredButtons.Add(CMPS3233);
            coloredButtons.Add(CMPS2433);
        }
        /*this function changes the color of preq and 
         * future classes of CMPS4991
        */
        private void CMPS4991_Click(object sender, RoutedEventArgs e)
        {
            checkbuttons();
            CMPS4991Popup.IsOpen = true;
        }
        /*this function changes the color of preq and 
         * future classes of STAT3573
        */
        private void STAT3573_Click(object sender, RoutedEventArgs e)
        {
            checkbuttons();
            STAT3573Popup.IsOpen = true;
            STAT3573.Background = new SolidColorBrush(Colors.Brown);
            MATH1233.Background = new SolidColorBrush(Colors.Yellow);
            MATH1534.Background = new SolidColorBrush(Colors.Yellow);
            coloredButtons.Add(STAT3573);
            coloredButtons.Add(MATH1233);
            coloredButtons.Add(MATH1534);

        }
        /*this fuction makes all the checkbox visible
         * and meuitem clickable
         */
        private void EditBS_Click(object sender, RoutedEventArgs e)
        {
            SaveEdit.Visibility = Visibility.Visible;
            MATH1534box.Visibility = Visibility.Visible;
            math1233box.Visibility = Visibility.Visible;
            MATH1443box.Visibility = Visibility.Visible;
            stat3573box.Visibility = Visibility.Visible;
            CMPS4991box.Visibility = Visibility.Visible;
            CMPS1044box.Visibility = Visibility.Visible;
            CMPS4143box.Visibility = Visibility.Visible;
            CMPS1063box.Visibility = Visibility.Visible;
            CMPS4113box.Visibility = Visibility.Visible;
            CMPS2084box.Visibility = Visibility.Visible;
            CMPS3023box.Visibility = Visibility.Visible;
            CMPS4103box.Visibility = Visibility.Visible;
            CMPS3013box.Visibility = Visibility.Visible;
            CMPS2143box.Visibility = Visibility.Visible;
            CMPS2433box.Visibility = Visibility.Visible;
            CSAE1box.Visibility = Visibility.Visible;
            CSAE2box.Visibility = Visibility.Visible;
            CSAE3box.Visibility = Visibility.Visible;
            CSAE4box.Visibility = Visibility.Visible;
            CSAE5box.Visibility = Visibility.Visible;
            CSAE6box.Visibility = Visibility.Visible;
            CSAE7box.Visibility = Visibility.Visible;
            var communicationlist = Communication.Items.OfType<MenuItem>().ToList();
            makecheckable(communicationlist);
            var LPSlist = LPSy.Children.OfType<MenuItem>().ToList();
            makecheckable(LPSlist);
            var CreativeArtslist = CreativeArtsy.Children.OfType<MenuItem>().ToList();
            makecheckable(CreativeArtslist);
            var AHistorylist = AHistoryy.Children.OfType<MenuItem>().ToList();
            makecheckable(AHistorylist);
            var GPSlist = GPSy.Children.OfType<MenuItem>().ToList();
            makecheckable(GPSlist);
            var SBSlist = SBSy.Children.OfType<MenuItem>().ToList();
            makecheckable(SBSlist);
            var CGUlist = CGUy.Children.OfType<MenuItem>().ToList();
            makecheckable(CGUlist);
            var UIClist = UICy.Children.OfType<MenuItem>().ToList();
            makecheckable(UIClist);
            var MRlist = MMY.Children.OfType<MenuItem>().ToList();
            makecheckable(MRlist);
            var ADDrlist = MAthreq.Children.OfType<MenuItem>().ToList();
            makecheckable(ADDrlist);

        }
        /* this fuction update user settings*/
        public void calldb(string settings, string user)
        {
            updatesettings = new Security();
            updatesettings.updatesavedsettingBs(settings, user);
            
        }
         /* this fuction disable buttons*/
        public void disablechecked()
        {
            var buttonlist = getbtn.Children.OfType<Button>().ToList();
            foreach (var item in buttonlist)
            {
                if (box.Contains(item.Name))
                {
                    item.IsEnabled = false;
                }
            }
        }
        /* this fuction saves user settings
         * and disable edit button
         */
        private void SaveEdit_Click(object sender, RoutedEventArgs e)
        {

            SaveEdit.Visibility = Visibility.Hidden;
            MATH1534box.Visibility = Visibility.Hidden;
            math1233box.Visibility = Visibility.Hidden;
            MATH1443box.Visibility = Visibility.Hidden;
            stat3573box.Visibility = Visibility.Hidden;
            CMPS4991box.Visibility = Visibility.Hidden;
            CMPS1044box.Visibility = Visibility.Hidden;
            CMPS4143box.Visibility = Visibility.Hidden;
            CMPS1063box.Visibility = Visibility.Hidden;
            CMPS4113box.Visibility = Visibility.Hidden;
            CMPS2084box.Visibility = Visibility.Hidden;
            CMPS3023box.Visibility = Visibility.Hidden;
            CMPS4103box.Visibility = Visibility.Hidden;
            CMPS3013box.Visibility = Visibility.Hidden;
            CMPS2143box.Visibility = Visibility.Hidden;
            CMPS2433box.Visibility = Visibility.Hidden;
            CSAE1box.Visibility = Visibility.Hidden;
            CSAE2box.Visibility = Visibility.Hidden;
            CSAE3box.Visibility = Visibility.Hidden;
            CSAE4box.Visibility = Visibility.Hidden;
            CSAE5box.Visibility = Visibility.Hidden;
            CSAE6box.Visibility = Visibility.Hidden;
            CSAE7box.Visibility = Visibility.Hidden;
            var communicationlist = Communication.Items.OfType<MenuItem>().ToList();
            checkitem(communicationlist, Communication);
            makecheckable(communicationlist, false);
            var LPSlist = LPSy.Children.OfType<MenuItem>().ToList();
            checkitem(LPSlist, LPS);
            makecheckable(LPSlist, false);
            var CreativeArtslist = CreativeArtsy.Children.OfType<MenuItem>().ToList();
            checkitem(CreativeArtslist, CreativeArts);
            makecheckable(CreativeArtslist, false);
            var AHistorylist = AHistoryy.Children.OfType<MenuItem>().ToList();
            checkitem(AHistorylist, AHistory);
            makecheckable(AHistorylist, false);
            var GPSlist = GPSy.Children.OfType<MenuItem>().ToList();
            checkitem(GPSlist, GPS);
            makecheckable(GPSlist, false);
            var SBSlist = SBSy.Children.OfType<MenuItem>().ToList();
            checkitem(SBSlist, SBS);
            makecheckable(SBSlist, false);
            var CGUlist = CGUy.Children.OfType<MenuItem>().ToList();
            checkitem(CGUlist, CGU);
            makecheckable(CGUlist, false);
            var UIClist = UICy.Children.OfType<MenuItem>().ToList();
            checkitem(UIClist, UIC);
            makecheckable(UIClist, false);
            var mathreqlist = ADDR.Items.OfType<MenuItem>().ToList();
            checkitem(mathreqlist, ADDR);
            var MMlist = MM.Items.OfType<MenuItem>().ToList();
            checkitem(MMlist, MM);
            checkedboxes();
            disablechecked();
            string stoerebs = get_BsButtons_menu();
            string name = Properties.Settings.Default.userset;
            calldb(stoerebs, name);

        }
        /* this function stores user settings*/
        public void settings(Dictionary<string, bool> set)
        {
            
            var buttonlist = getbtn.Children.OfType<Button>().ToList();
            var menus =  getbtn.Children.OfType<Menu>().ToList();
            foreach (var item in buttonlist)
            {
                var name = item.Name;
                if (!otherbuttons.Contains(name))
                {
                    item.IsEnabled = set[name];
                }
            }
            foreach (var item in menus)
            {
                var itemlist = item.Items.OfType<MenuItem>().ToList();
                var name = itemlist[0].Name;
                item.IsEnabled = set[name];
            }
        }
        // this fuction get user settings from dictionary
        public Dictionary<string, bool> getsettings(string username)
        {
             Dictionary<string, bool> bsSettings = new Dictionary<string, bool>();
             Security Securelogin = new Security();
             var usersettings = Securelogin.getusersettingsBaandbs(username);
             var bs= usersettings.Item2;
             bsSettings = JsonConvert.DeserializeObject<Dictionary<string, bool>>(bs);
            
               
            return bsSettings;

        }
         /*this fuction adds current user
          * settings to old settings
          */
        public void mergedic(Dictionary<string, bool> dic)
        {
            foreach (var item in dic)
            {
                if (!savedsettings.ContainsKey(item.Key))
                {
                    savedsettings.Add(item.Key, item.Value);
                }

               
            }
        }
        /*this function updates ba user settings onload
         */
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          var setting =  getsettings(username);
            if (setting != null)
            {
                mergedic(setting);
                settings(setting);

            }

        }
    }
}
