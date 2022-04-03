using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Motaz_Store
{
    public static class Forms
    {
        // Sells
        public static Sells sells = new Sells() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };

        public static Sells_Add sells_Add = new Sells_Add() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public static Sells_Del sells_Del = new Sells_Del() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public static Sells_Today sells_Today = new Sells_Today() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public static Sells_View sells_View = new Sells_View() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };


        public static Settings settings = new Settings() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };


        public static Store store = new Store() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };

        public static Store_Add store_Add = new Store_Add() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public static Store_Del store_Del = new Store_Del() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public static Store_Edit store_Edit = new Store_Edit() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public static Store_View store_View = new Store_View() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public static Store_Other store_Other = new Store_Other() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
    }
}
