using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Motaz_Store
{
    public partial class LogIn : Form
    {
        Msgs msg;
        public LogIn()
        {
            InitializeComponent();

            // Messages Handler
            msg = new Msgs(lbl_Error);

            // EnterClicks
            txt_user.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_user.Text) && E.KeyCode == Keys.Enter)
                {
                    txt_pass.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            txt_pass.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_pass.Text) && E.KeyCode == Keys.Enter)
                {
                    btn_Login.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
        }

        private void Txt_user_Enter(object sender, EventArgs e)
        {
            txt_user.BackColor = Color.FromArgb(255, 244, 230);
            txt_user.ForeColor = Color.FromArgb(45, 64, 59);
            pnl_username.BackColor = Color.FromArgb(45, 64, 59);
            lbl_user.Location = new Point(131, 184);
            lbl_user.ForeColor = Color.FromArgb(45, 64, 59);
            lbl_user.BackColor = Color.FromArgb(242, 229, 213);
            lbl_user.Font = new Font("Cairo SemiBold", 10, FontStyle.Bold);
        }

        private void Txt_user_Leave(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(txt_user.Text))
            {
                lbl_user.Location = new Point(117, 220);
                lbl_user.BackColor = Color.FromArgb(234, 220, 202);
                lbl_user.Font = new Font("Cairo SemiBold", 13, FontStyle.Bold);
            }
            lbl_user.ForeColor = Color.FromArgb(209, 178, 140);
            txt_user.BackColor = Color.FromArgb(234, 220, 202);
            txt_user.ForeColor = Color.FromArgb(209, 178, 140);
            pnl_username.BackColor = Color.FromArgb(209, 178, 140);
        }

        private void Txt_pass_Enter(object sender, EventArgs e)
        {
            txt_pass.BackColor = Color.FromArgb(255, 244, 230);
            txt_pass.ForeColor = Color.FromArgb(45, 64, 59);
            pnl_password.BackColor = Color.FromArgb(45, 64, 59);
            lbl_pass.Location = new Point(140, 285);
            lbl_pass.ForeColor = Color.FromArgb(45, 64, 59);
            lbl_pass.BackColor = Color.FromArgb(242, 229, 213);
            lbl_pass.Font = new Font("Cairo SemiBold", 10, FontStyle.Bold);
        }

        private void Txt_pass_Leave(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(txt_pass.Text))
            {
                lbl_pass.Location = new Point(128, 321);
                lbl_pass.BackColor = Color.FromArgb(234, 220, 202);
                lbl_pass.Font = new Font("Cairo SemiBold", 13, FontStyle.Bold);
            }
            lbl_pass.ForeColor = Color.FromArgb(209, 178, 140);
            txt_pass.BackColor = Color.FromArgb(234, 220, 202);
            txt_pass.ForeColor = Color.FromArgb(209, 178, 140);
            pnl_password.BackColor = Color.FromArgb(209, 178, 140);
        }

        private void Lbl_user_Click(object sender, EventArgs e)
        {
            txt_user.Focus();
        }

        private void Lbl_pass_Click(object sender, EventArgs e)
        {
            txt_pass.Focus();
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            // Validation
            if(String.IsNullOrWhiteSpace(txt_user.Text))
            { txt_user.Focus(); msg.ShowError("قم بملئ الحقول اولا"); return; }
            if (String.IsNullOrWhiteSpace(txt_pass.Text))
            { txt_pass.Focus(); msg.ShowError("قم بملئ الحقول اولا"); return; }

            List<string> vals = myDB.GetDataArray("SELECT * FROM Users WHERE Username=@user AND Password=@pass", 3, new string[] {
            "@user", txt_user.Text, "@pass", txt_pass.Text});

            if(vals.Count < 3)
            {
                msg.ShowError("أسم المستخدم أو كلمة المرور خطأ");
                txt_user.Focus();
                return;
            }
            else
            {
                Session.username = vals[0];
                Session.password = vals[1];
                Session.inDay = myDB.GetDataString("SELECT Value FROM Settings WHERE Setting='InDay'");
                Session.isManager = (Convert.ToInt32(vals[2]) == 1) ? true : false;
                this.Close();
            }
        }

        private void Ch_Pass_CheckedChanged(object sender, EventArgs e)
        {
            if(ch_Pass.Checked)
            {
                txt_pass.PasswordChar = '\0';
            }
            else
            {
                txt_pass.PasswordChar = '*';
            }
        }
    }
}
