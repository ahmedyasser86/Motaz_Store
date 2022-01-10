using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Motaz_Store
{
    public class Visual_Scripts
    {
        Color BFore;
        Color BBack;
        Color HFore;
        Color HBack;

        string Clicked = "";
        Control Panel = new Control();

        public Visual_Scripts(Control pnl, Color HoverFore, Color HoverBack)
        {
            BFore = pnl.Controls[0].ForeColor;
            BBack = pnl.Controls[0].BackColor;
            HFore = HoverFore;
            HBack = HoverBack;
            Panel = pnl;
        }

        public void OnBtnHover(Control c)
        {

            c.MouseEnter += (S, E) =>
            {
                c.ForeColor = HFore;
                c.BackColor = HBack;
            };

            c.MouseLeave += (S, E) =>
            {
                if(c.Name != Clicked)
                {
                    c.ForeColor = BFore;
                    c.BackColor = BBack;
                }
            };
        }

        public void OnBtnHover()
        {
            foreach(Control C in Panel.Controls)
            {
                OnBtnHover(C);
            }
        }

        public void OnBtnClick(Control CButton)
        {
            foreach(Control ctrl in Panel.Controls)
            {
                ctrl.BackColor = BBack;
                ctrl.ForeColor = BFore;
            }

            CButton.BackColor = HBack;
            CButton.ForeColor = HFore;

            Clicked = CButton.Name;
        }

        public static void FrmToPnl(Panel pnl, Form frm)
        {
            pnl.Controls.Clear();
            pnl.Controls.Add(frm);
            frm.Show();
        }
    }
}
