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

namespace Motaz_Store
{
    public partial class MainForm : Form
    {
        Visual_Scripts scripts;
        public MainForm()
        {
            InitializeComponent();

            scripts = new Visual_Scripts(tlp_Buttons, Color.FromArgb(242, 241, 240), Color.FromArgb(191, 107, 4));

            // Add Hover Events
            scripts.OnBtnHover();
        }

        private void TopBtnClick(object sender, EventArgs e)
        {
            var btn = sender as Control;

            // Change Color
            scripts.OnBtnClick(btn);

            switch (btn.Name)
            {
                case "lbl_Sells":
                    FrmToPnl(pnl_Holder, Forms.sells);
                    break;

                case "lbl_Store":
                    FrmToPnl(pnl_Holder, Forms.store);
                    break;

                case "lbl_Settings":
                    FrmToPnl(pnl_Holder, Forms.settings);
                    break;
            }
        }
    }
}
