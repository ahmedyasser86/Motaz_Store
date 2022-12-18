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
    public partial class Settings : Form
    {
        public float GainRate = 0.4f;
        public bool IsAdmin = true;
        public Settings()
        {
            InitializeComponent();

            if (Session.username == "bika") btn_Bika.Show();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            btn_General.PerformClick();
        }

        private void btn_Clicks(object sender, EventArgs e)
        {
            var btn = sender as Button;

            Btn_Click_Color(pnl_Top, new Color[] { Color.FromArgb(191, 113, 44), Color.FromArgb(242, 229, 213) },
                new Color[] { Color.FromArgb(242, 229, 213), Color.FromArgb(89, 25, 2) }, btn);
            Btn_Move_Line(btn, pnl_Line);

            switch (btn.Name)
            {
                case "btn_General":
                    FrmToPnl(Pnl_Holder, settings_General, settings_General.txt_Color);
                    break;
                case "btn_User":
                    FrmToPnl(Pnl_Holder, settings_User, settings_User.txt_p_OldPass);
                    break;
                case "btn_Adv":
                    FrmToPnl(Pnl_Holder, settings_Adv);
                    break;
                case "btn_Bika":
                    FrmToPnl(Pnl_Holder, settings_Bika);
                    break;
            }
        }
    }
}
