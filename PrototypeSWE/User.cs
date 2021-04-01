using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeSWE
{
    public class User
    {
        private string Username;
        public string username   // property
        {
            get { return Username; }   // get method
            set { Username = value; }  // set method
        }

        private Dictionary<string, bool> savesettingsba;
        public Dictionary<string, bool> MapBA { 
            get { return savesettingsba; }
            set { savesettingsba = value; }
        }
        private Dictionary<string, bool> savesettingsbs;
        public Dictionary<string, bool> MapBs
        {
            get { return savesettingsbs; }
            set { savesettingsbs = value; }
        }

    }
        
    }
