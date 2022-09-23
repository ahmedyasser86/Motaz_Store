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
        public static Sells_Agel sells_Agel = new Sells_Agel() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public static Sells_Online sells_Online = new Sells_Online() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public static EndDay endDay = new EndDay() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };

        // Settings
        public static Settings settings = new Settings() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };

        public static Settings_General settings_General = new Settings_General() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public static Settings_User settings_User = new Settings_User() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public static Settings_Adv settings_Adv = new Settings_Adv() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public static Settings_Bika settings_Bika = new Settings_Bika() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };

        // Store
        public static Store store = new Store() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };

        public static Store_Add store_Add = new Store_Add() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public static Store_Del store_Del = new Store_Del() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public static Store_Edit store_Edit = new Store_Edit() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public static Store_View store_View = new Store_View() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
        public static Store_Other store_Other = new Store_Other() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };

        // Password
        public static Password password = new Password() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
    }
}
