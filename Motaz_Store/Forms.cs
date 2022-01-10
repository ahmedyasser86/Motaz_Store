using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Motaz_Store
{
    public class Forms
    {
        public static Sells sells = new Sells() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };


        public static Settings settings = new Settings() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };


        public static Store store = new Store() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
    }
}
