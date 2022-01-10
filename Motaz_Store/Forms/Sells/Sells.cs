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
    public partial class Sells : Form
    {
        public Sells()
        {
            InitializeComponent();
        }

        private void Sells_Load(object sender, EventArgs e)
        {
            // Open Default Page
            btn_Add.PerformClick();
        }

        private void Top_Btns_CLick(object sender, EventArgs e)
        {
            var btn = sender as Button;

            Btn_Click_Color(pnl_Top, new Color[] { Color.FromArgb(191, 113, 44), Color.FromArgb(242, 229, 213) },
                new Color[] { Color.FromArgb(242, 229, 213), Color.FromArgb(89, 25, 2) }, btn);
            Btn_Move_Line(btn, pnl_Line);

            switch (btn.Name)
            {
                case "btn_Add":
                    FrmToPnl(Pnl_Holder, sells_Add);
                    break;
                case "btn_Del":
                    FrmToPnl(Pnl_Holder, sells_Del);
                    break;
                case "btn_Today":
                    FrmToPnl(Pnl_Holder, sells_Today);
                    break;
                case "btn_View":
                    FrmToPnl(Pnl_Holder, sells_View);
                    break;
            }
        }
    }
}
