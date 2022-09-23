using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Motaz_Store.myDB;

namespace Motaz_Store
{
    public partial class Settings_User : Form
    {
        Msgs msg;
        public Settings_User()
        {
            InitializeComponent();

            // Manager Panel
            if (Session.isManager) pnl_Manager.Show();

            // Messages Handler
            msg = new Msgs(lbl_Error);

            #region EnterClicks
            txt_p_OldPass.KeyDown += (S, E) =>
            {
                if(!String.IsNullOrWhiteSpace(txt_p_OldPass.Text) && E.KeyCode == Keys.Enter)
                {
                    txt_p_NewPass.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            txt_p_NewPass.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_p_NewPass.Text) && E.KeyCode == Keys.Enter)
                {
                    btn_ChangePass.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            // -----------------------------------
            txt_u_Pass.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_u_Pass.Text) && E.KeyCode == Keys.Enter)
                {
                    txt_u_Username.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            txt_u_Username.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_u_Username.Text) && E.KeyCode == Keys.Enter)
                {
                    btn_ChangeUser.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            // -----------------------------------
            txt_a_User.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_a_User.Text) && E.KeyCode == Keys.Enter)
                {
                    txt_a_Pass.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            txt_a_Pass.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_a_Pass.Text) && E.KeyCode == Keys.Enter)
                {
                    btn_AddUser.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            // -----------------------------------
            txt_d_User.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_d_User.Text) && E.KeyCode == Keys.Enter)
                {
                    btn_DeleteUser.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            // -----------------------------------
            txt_m_User.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_m_User.Text) && E.KeyCode == Keys.Enter)
                {
                    btn_MakeManager.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            #endregion
        }

        private void Btn_ChangePass_Click(object sender, EventArgs e)
        {
            // Validation Empty
            if(String.IsNullOrWhiteSpace(txt_p_OldPass.Text) || String.IsNullOrWhiteSpace(txt_p_NewPass.Text))
            {
                msg.ShowError("قم بملئ الحقول أولا");
                txt_p_OldPass.Focus();
                return;
            }

            // Validation OldPass
            if(txt_p_OldPass.Text != Session.password)
            {
                msg.ShowError("الباسورد القديم غير صحيح");
                txt_p_OldPass.Focus();
                return;
            }

            // Update
            if(CmdExcuteRows("UPDATE Users SET Password=N'" + txt_p_NewPass.Text + "' WHERE Username=@Username", new string[]
                { "@Username", Session.username }) == 1)
            {
                // Done
                msg.ShowError("تم تغيير كلمة المرور بنجاح", true);
                Session.password = txt_p_NewPass.Text;
                txt_p_OldPass.Text = "";
                txt_p_NewPass.Text = "";
                txt_p_OldPass.Focus();
            }
            else
            {
                // Failed
                msg.ShowError("حدث خطأ ما، حاول مرة أخرى");
                txt_p_OldPass.Focus();
            }
        }

        private void Btn_ChangeUser_Click(object sender, EventArgs e)
        {
            // Validation Empty
            if (String.IsNullOrWhiteSpace(txt_u_Pass.Text) || String.IsNullOrWhiteSpace(txt_u_Username.Text))
            {
                msg.ShowError("قم بملئ الحقول أولا");
                txt_u_Pass.Focus();
                return;
            }

            // Validation OldPass
            if (txt_u_Pass.Text != Session.password)
            {
                msg.ShowError("الباسورد القديم غير صحيح");
                txt_u_Pass.Focus();
                return;
            }

            // Update
            if (CmdExcuteRows("UPDATE Users SET Username=N'" + txt_u_Username.Text + "' WHERE Username=@Username", new string[]
                { "@Username", Session.username }) == 1)
            {
                // Done
                msg.ShowError("تم تغيير اسم المستخدم بنجاح", true);
                Session.username = txt_u_Username.Text;
                txt_u_Username.Text = "";
                txt_u_Pass.Text = "";
                txt_u_Pass.Focus();
            }
            else
            {
                // Failed
                msg.ShowError("حدث خطأ ما، حاول مرة أخرى");
                txt_u_Pass.Focus();
            }
        }

        private void Btn_AddUser_Click(object sender, EventArgs e)
        {
            // Validation
            if(String.IsNullOrWhiteSpace(txt_a_User.Text) || String.IsNullOrWhiteSpace(txt_a_User.Text))
            {
                msg.ShowError("قم بملئ الحقول أولا");
                txt_a_User.Focus();
                return;
            }

            // Add
            if(CmdExcute("INSERT INTO Users(Username, Password) VALUES(N'" + txt_a_User.Text + "', N'" + txt_a_Pass.Text + "')"))
            {
                // Done
                msg.ShowError("تم إضافة المستخدم بنجاح", true);
                txt_a_User.Text = "";
                txt_a_Pass.Text = "";
                txt_a_User.Focus();
            }
            else
            {
                // Failed
                msg.ShowError("فشل إضافة المستخدم، تأكد ان أسم المستخدم غير مكرر");
                txt_a_User.Focus();
            }
        }

        private void Btn_DeleteUser_Click(object sender, EventArgs e)
        {
            // Validation
            if (String.IsNullOrWhiteSpace(txt_d_User.Text))
            {
                msg.ShowError("قم بملئ الحقول أولا");
                txt_d_User.Focus();
                return;
            }

            // Ask User
            if (!Visual_Scripts.AskUser("هل انت متأكد من انك تريد حذف المستخدم؟\nلا يمكن التراجع عن هذه العملية", 'q'))
            {
                txt_d_User.Focus();
                return;
            }

            // Delete
            if(CmdExcuteRows("DELETE FROM Users WHERE Username=@Username", new string[] { "@Username", txt_d_User.Text }) > 0)
            {
                // Done
                msg.ShowError("تم حذف المستخدم ", true);
                txt_d_User.Text = "";
                txt_d_User.Focus();
            }
            else
            {
                // Failed
                msg.ShowError("فشلت العملية");
                txt_d_User.Focus();
            }

        }

        private void Btn_MakeManager_Click(object sender, EventArgs e)
        {
            // Validation
            if(String.IsNullOrWhiteSpace(txt_m_User.Text))
            {
                msg.ShowError("قم بملئ الحقول أولا");
                txt_m_User.Focus();
                return;
            }

            // Ask User
            if(!Visual_Scripts.AskUser("هل انت متأكد من انك تريد جعله مديرا؟\nلا يمكن التراجع عن هذه العملية", 'q'))
            {
                txt_m_User.Focus();
                return;
            }

            // Update
            if(CmdExcuteRows("UPDATE Users SET isManager=1 WHERE Username=@Username", new string[] { "@Username", txt_m_User.Text }) == 1)
            {
                // Done
                msg.ShowError("تم تعيين المستخدم مديرا", true);
                Session.isManager = true;
                txt_m_User.Text = "";
                txt_m_User.Focus();
            }
            else
            {
                // Failed
                msg.ShowError("فشلت العملية");
                txt_m_User.Focus();
            }
        }
    }
}
