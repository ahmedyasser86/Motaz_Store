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

            if (!Session.isManager)
                btn_View.Hide();
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
                    FrmToPnl(Pnl_Holder, sells_Add, sells_Add.txt_Code, sells_Add.txt_Art);
                    break;
                case "btn_Del":
                    FrmToPnl(Pnl_Holder, sells_Del, sells_Del.txt_BillID);
                    break;
                case "btn_Today":
                    password.frm = "Today";
                    FrmToPnl(Pnl_Holder, password, password.txt_Password);
                    break;
                case "btn_View":
                    FrmToPnl(Pnl_Holder, sells_View);
                    break;
                case "btn_Online":
                    FrmToPnl(Pnl_Holder, sells_Online);
                    break;
                case "btn_Agel":
                    FrmToPnl(Pnl_Holder, sells_Agel);
                    break;
            }
        }

        public void OpenToday()
        {
            FrmToPnl(Pnl_Holder, sells_Today, sells_Today.txt_Ex);
        }

        public void OpenEndDay()
        {
            FrmToPnl(Pnl_Holder, endDay);
        }
    }
}
