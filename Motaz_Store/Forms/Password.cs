using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Motaz_Store.Forms;

namespace Motaz_Store
{
    public partial class Password : Form
    {
        Msgs msg;
        public string frm;
        public Password()
        {
            InitializeComponent();

            // Messages Handler
            msg = new Msgs(lbl_Error);

            // Keydown
            txt_Password.KeyDown += (S, E) =>
            {
                if(E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Password.Text))
                {
                    E.SuppressKeyPress = true;
                    Btn_Show.PerformClick();
                }
            };
        }

        private void Btn_Show_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(txt_Password.Text))
            {
                msg.ShowError("قم بكتابة كلمة مرورك أولا");
                txt_Password.Focus();
                return;
            }

            if(txt_Password.Text == Session.password)
            {
                txt_Password.Text = "";
                switch (frm)
                {
                    case "Today":
                        sells.OpenToday();
                        sells_Today.tlp_Details.Hide();
                        break;
                }
            }
            else
            {
                msg.ShowError("كلمة المرور خاطئة، قم بكتابة كلمة مرور حسابك");
                txt_Password.Focus();
            }
        }
    }
}
