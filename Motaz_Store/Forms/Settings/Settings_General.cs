using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Motaz_Store.myDB;

namespace Motaz_Store
{
    public partial class Settings_General : Form
    {
        Msgs msg;
        public Settings_General()
        {
            InitializeComponent();

            // Messages Handler
            msg = new Msgs(lbl_Error);

            // Get GainRate From DB
            if (!Session.isManager) txt_Gain.PasswordChar = '*';
            float gainrate = float.Parse(GetDataString("SELECT Value FROM Settings Where Setting='GainRate'"),
                CultureInfo.InvariantCulture.NumberFormat);
            txt_Gain.Text = (gainrate * 100).ToString();
            Forms.settings.GainRate = gainrate;

            #region Controls' Events
            txt_Gain.KeyDown += (S, E) =>
             {
                 if(E.KeyCode == Keys.Enter && !txt_Gain.ReadOnly)
                 {
                     Btn_Gain.PerformClick();
                 }
             };

            // --
            // txt_Color
            txt_Color.KeyDown += (S, E) =>
            {
                if(E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Color.Text))
                {
                    btn_ColorAdd.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            // --
            // txt_Seller
            txt_Seller.KeyDown += (S, E) =>
            {
                if(E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Seller.Text))
                {
                    btn_SellerAdd.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            // --
            // txt_Sup
            txt_Sup.KeyDown += (S, E) =>
            {
                if(E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Sup.Text))
                {
                    btn_SupAdd.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            #endregion
        }

        private void Btn_ColorAdd_Click(object sender, EventArgs e)
        {
            // Validation
            if(String.IsNullOrWhiteSpace(txt_Color.Text))
            {
                msg.ShowError("قم بكتابة اللون أولا");
                txt_Color.Focus();
                return;
            }

            if(CmdExcute("INSERT INTO Colors(Color_Name) VALUES(N'" + txt_Color.Text + "')"))
            {
                // Done
                msg.ShowError("نجح إضافة اللون، قم بإعادة تشغيل البرنامج لكي يظهر لك", true);
                txt_Color.Text = "";
                txt_Color.Focus();
            }
            else
            {
                // Failed
                msg.ShowError("فشل إضافة اللون، قد يكون موجود بالفعل");
                txt_Color.Focus();
            }
        }

        private void Btn_ColorDelete_Click(object sender, EventArgs e)
        {
            // Validation
            if (String.IsNullOrWhiteSpace(txt_Color.Text))
            {
                msg.ShowError("قم بكتابة اللون أولا");
                txt_Color.Focus();
                return;
            }

            if (CmdExcuteRows("DELETE FROM Colors WHERE Color_Name=@Color_Name", new string[] { "@Color_Name", txt_Color.Text }) > 0)
            {
                // Done
                msg.ShowError("نجح حذف اللون، قم بإعادة تشغيل البرنامج الأن", true);
                txt_Color.Text = "";
                txt_Color.Focus();
            }
            else
            {
                // Failed
                msg.ShowError("فشل حذف اللون، قد يكون غير موجود بالفعل");
                txt_Color.Focus();
            }
        }

        private void Btn_SellerAdd_Click(object sender, EventArgs e)
        {
            // Validation
            if(String.IsNullOrWhiteSpace(txt_Seller.Text))
            {
                msg.ShowError("");
                txt_Seller.Focus();
                return;
            }

            string online = (rb_Online.Checked) ? "1" : "0";

            if(CmdExcute("INSERT INTO Sellers(Name, Online) VALUES(N'" + txt_Seller.Text + "', " + online + ")"))
            {
                msg.ShowError("تم إضافة البائع بنجاح، أعد تشغيل البرنامج ليظهر لك", true);
                txt_Seller.Text = "";
                txt_Seller.Focus();
            }
            else
            {
                msg.ShowError("فشل إضافة البائع، قد يكون الأسم مكرر");
                txt_Seller.Focus();
            }
        }

        private void Btn_SellerDelete_Click(object sender, EventArgs e)
        {
            // Validation
            if (String.IsNullOrWhiteSpace(txt_Seller.Text))
            {
                msg.ShowError("");
                txt_Seller.Focus();
                return;
            }

            string online = (rb_Online.Checked) ? "1" : "0";

            if (CmdExcuteRows("DELETE FROM Sellers WHERE Name=@Name AND Online=@Online", new string[] { "@Name", txt_Seller.Text
            , "@Online", online }) > 0)
            {
                msg.ShowError("تم حذف البائع بنجاح، أعد تشغيل البرنامج الأن", true);
                txt_Seller.Text = "";
                txt_Seller.Focus();
            }
            else
            {
                msg.ShowError("فشل حذف البائع، قد يكون لا يوجد بائع بهذه المواصفات");
                txt_Seller.Focus();
            }
        }

        private void Btn_Gain_Click(object sender, EventArgs e)
        {
            if(Btn_Gain.Text == "تعديل")
            {
                if (!Session.isManager)
                {
                    msg.ShowError("لا تمتلك صلاحيات التعديل");
                    return;
                }

                txt_Gain.ReadOnly = false;
                txt_Gain.BackColor = Color.FromArgb(255, 244, 230);
                txt_Gain.Focus();
                Btn_Gain.Text = "حفظ";
            }
            else
            {
                try
                {
                    Convert.ToInt32(txt_Gain.Text);
                }
                catch
                {
                    msg.ShowError("يجب أن يكون رقم صحيح");
                    txt_Gain.Text = (Forms.settings.GainRate * 100).ToString();
                    txt_Gain.ReadOnly = true;
                    txt_Gain.BackColor = Color.FromArgb(209, 178, 140);
                    Btn_Gain.Text = "تعديل";
                    return;
                }

                if (Visual_Scripts.AskUser("هل انت متأكد من تعديل نسبة الربح؟", 'q'))
                {
                    // UPDATE
                    float gain = float.Parse(txt_Gain.Text) / 100;
                    if (CmdExcute("UPDATE Settings SET Value='" + gain.ToString() + "' WHERE Setting='GainRate'"))
                    {
                        msg.ShowError("تم التعديل بنجاح", true);
                        Forms.settings.GainRate = gain;
                        txt_Gain.ReadOnly = true;
                        txt_Gain.BackColor = Color.FromArgb(209, 178, 140);
                        Btn_Gain.Text = "تعديل";
                        txt_Gain.Focus();
                    }
                    else
                    {
                        msg.ShowError("حدث خطأ ما");
                        txt_Gain.Text = (Forms.settings.GainRate * 100).ToString();
                        txt_Gain.ReadOnly = true;
                        txt_Gain.BackColor = Color.FromArgb(209, 178, 140);
                        Btn_Gain.Text = "تعديل";
                    }
                }
                else
                {
                    txt_Gain.Text = (Forms.settings.GainRate * 100).ToString();
                    txt_Gain.ReadOnly = true;
                    txt_Gain.BackColor = Color.FromArgb(209, 178, 140);
                    Btn_Gain.Text = "تعديل";
                }
            }
        }

        private void Btn_SupAdd_Click(object sender, EventArgs e)
        {
            // Validation
            if (String.IsNullOrWhiteSpace(txt_Sup.Text))
            {
                msg.ShowError("قم بكتابة المورد أولا");
                txt_Sup.Focus();
                return;
            }

            if (CmdExcute("INSERT INTO Sups(Name) VALUES(N'" + txt_Sup.Text + "')"))
            {
                // Done
                msg.ShowError("نجح إضافة المورد، قم بإعادة تشغيل البرنامج لكي يظهر لك", true);
                txt_Sup.Text = "";
                txt_Sup.Focus();
            }
            else
            {
                // Failed
                msg.ShowError("فشل إضافة المورد، قد يكون موجود بالفعل");
                txt_Sup.Focus();
            }
        }

        private void Btn_SupDelete_Click(object sender, EventArgs e)
        {
            // Validation
            if (String.IsNullOrWhiteSpace(txt_Sup.Text))
            {
                msg.ShowError("قم بكتابة اللون أولا");
                txt_Sup.Focus();
                return;
            }

            if (CmdExcuteRows("DELETE FROM Sups WHERE Name=@Name", new string[] { "@Name", txt_Sup.Text }) > 0)
            {
                // Done
                msg.ShowError("نجح حذف المورد، قم بإعادة تشغيل البرنامج الأن", true);
                txt_Sup.Text = "";
                txt_Sup.Focus();
            }
            else
            {
                // Failed
                msg.ShowError("فشل حذف المورد، قد يكون غير موجود بالفعل");
                txt_Sup.Focus();
            }
        }
    }
}
