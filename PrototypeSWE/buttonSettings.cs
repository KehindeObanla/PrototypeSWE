using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace PrototypeSWE
{
    public static class buttonSettings
    {
        public static BSWindow bswin;
        public static BAWindow bawin;
        public static Dictionary<string, bool> savedsettings = new Dictionary<string, bool>();
        public static Dictionary<string, bool> savedsettingsBA = new Dictionary<string, bool>();
        public static List<string> otherbuttons = new List<string>(){ "DownloadBtnBS", "EditBS", "Backbtn" , "SaveEdit" , "EditBA1", "BackBtnBA1", "DownloadBtnBA1", "SaveEdit" };
        public static List<string> menuitemnames = new List<string>() { "Communication", "LPSy", "CreativeArtsy", "AHistoryy", "GPSy",
        "SBSy","CGUy","UICy","MRlist","ADDrlist"};
        public static void savesetting(List<Button> buttonlist,char c )
        {
            foreach (var item in buttonlist)
            {
                if (!otherbuttons.Contains(item.Name))
                {
                    if (c == 's')
                    {
                        if (!item.IsEnabled)
                        {
                            savedsettings.Add(item.Name, false);
                        }
                        else
                        {
                            savedsettings.Add(item.Name, true);
                        }

                    }
                    else
                    {
                        if (!item.IsEnabled)
                        {
                            savedsettingsBA.Add(item.Name, false);
                        }
                        else
                        {
                            savedsettingsBA.Add(item.Name, true);
                        }
                    }

                }
            }
        }
        public static void savemenuitem(List<Menu> menus,char c)
        {
            foreach (var item in menus)
            {
                var itemlist = item.Items.OfType<MenuItem>().ToList();
                if(c == 's')
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
                else
                {
                    if (!itemlist[0].IsEnabled)
                    {
                        savedsettingsBA.Add(itemlist[0].Name, false);
                    }
                    else
                    {
                        savedsettingsBA.Add(itemlist[0].Name, true);
                    }
                }
            }
        }
        public static string get_BsButtons_menu()
       {
            bswin = new BSWindow();
            var buttonlist = bswin.getbtn.Children.OfType<Button>().ToList();
            var menus = bswin.getbtn.Children.OfType<Menu>().ToList();
            savesetting(buttonlist,'s');
            savemenuitem(menus,'s');
            string jsonsavedbs = JsonConvert.SerializeObject(savedsettings, Formatting.Indented);
            return jsonsavedbs;


        }



        public static string get_BaButtons()
        {
            bawin = new BAWindow();
            var buttonlist = bawin.MainGrid_Copy.Children.OfType<Button>().ToList();
            var menu = bawin.MainGrid_Copy.Children.OfType<Menu>().ToList();
            savesetting(buttonlist,'a');
            savemenuitem(menu,'a');
            string jsonsavedba = JsonConvert.SerializeObject(savedsettingsBA, Formatting.Indented);
            return jsonsavedba;
        }
        
       
    }
}
