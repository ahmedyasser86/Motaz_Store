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
    public partial class Store : Form
    {
        public Store()
        {
            InitializeComponent();
        }

        private void Store_Load(object sender, EventArgs e)
        {
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
                    FrmToPnl(pnl_Holder, store_Add);
                    break;
                case "btn_Del":
                    FrmToPnl(pnl_Holder, store_Del);
                    break;
                case "btn_Edit":
                    FrmToPnl(pnl_Holder, store_Edit);
                    break;
                case "btn_View":
                    FrmToPnl(pnl_Holder, store_View);
                    break;
                case "btn_Other":
                    FrmToPnl(pnl_Holder, store_Other);
                    break;
            }
        }
    }
}
