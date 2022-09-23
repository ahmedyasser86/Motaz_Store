using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Motaz_Store
{
    public static class Program
    {
        public static MainForm main;
        public static LogIn logIn;
        public static string Backup;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            myDB.OpenConn();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            logIn = new LogIn();
            Application.Run(logIn);
            if(Session.username != null)
            {
                main = new MainForm();
                Application.Run(main);
                myDB.BackUp(Backup);
            }
            myDB.CloseConn();
        }
    }
}
