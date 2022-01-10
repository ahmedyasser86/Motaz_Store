using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Motaz_Store.Visual_Scripts;
using static Motaz_Store.Forms;

namespace Motaz_Store
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Open Default Page
            Btn_Sells.PerformClick();
        }

        private void Top_Buttons_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            Btn_Click_Color(tlp_Buttons, new Color[] { Color.FromArgb(89, 25, 2) }, new Color[] { Color.FromArgb(191, 113, 44) }
            , btn);

            switch (btn.Name)
            {
                case "Btn_Sells":
                    FrmToPnl(pnl_Holder, sells);
                    break;
                case "Btn_Store":
                    FrmToPnl(pnl_Holder, store);
                    break;
                case "Btn_Settings":
                    FrmToPnl(pnl_Holder, settings);
                    break;
            }
        }
    }
}
