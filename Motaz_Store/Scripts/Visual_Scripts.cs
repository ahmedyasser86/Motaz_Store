using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Motaz_Store
{
    public static class Visual_Scripts
    {
        public static void Btn_Click_Color(Control pnl, Color[] BaseColor, Color[] ClickedColor, Control ClickedBtn)
        {
            foreach(Control btn in pnl.Controls)
            {
                if(BaseColor.Length == 1)
                {
                    btn.BackColor = BaseColor[0];
                }
                else if(ClickedColor.Length == 2)
                {
                    btn.BackColor = BaseColor[0];
                    btn.ForeColor = BaseColor[1];
                }
            }

            if (ClickedColor.Length == 1)
            {
                ClickedBtn.BackColor = ClickedColor[0];
            }
            else if (ClickedColor.Length == 2)
            {
                ClickedBtn.BackColor = ClickedColor[0];
                ClickedBtn.ForeColor = ClickedColor[1];
            }
        }

        public static void Btn_Move_Line(Control Btn, Panel Line)
        {
            Line.Width = Btn.Width + 2;
            Line.Location = new Point(Btn.Location.X - 1, Btn.Location.Y + Btn.Height);
            Line.Show();
        }

        public static void FrmToPnl(Panel pnl, Form frm, Control toFocus = null, Control toFocus2 = null)
        {
            pnl.Controls.Clear();
            pnl.Controls.Add(frm);
            frm.Show();


            // Open form focused on element
            if(toFocus != null)
            {
                // 2 Choises
                if(toFocus2 != null)
                {
                    if (toFocus.Visible) toFocus.Focus();
                    else toFocus2.Focus();
                }
                else
                {
                    toFocus.Focus();
                }
            }
        }

        public static bool AskUser(string text, char type)
        {
            if (type != 'q' && type != 'e') return false;
            if(type == 'q')
            {
                DialogResult dr = MessageBox.Show(text, "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                    return true;
                else
                    return false;
            }

            else
            {
                DialogResult dr = MessageBox.Show(text, "حطأ", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (dr == DialogResult.OK)
                    return true;
                else
                    return false;
            }
        }
    }
}
