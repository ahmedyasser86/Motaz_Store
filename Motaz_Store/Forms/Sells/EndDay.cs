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
    public partial class EndDay : Form
    {
        Msgs msg;
        public EndDay()
        {
            InitializeComponent();

            // Messages Handler
            msg = new Msgs(lbl_Error);

            // txt Safe Event Handler
            txt_Safe.TextChanged += (S, E) =>
            {
                if(String.IsNullOrWhiteSpace(txt_Safe.Text))
                {
                    lbl_ToHome.Text = lbl_Total.Text;
                }
                else
                {
                    try
                    {
                        int safe = Convert.ToInt32(txt_Safe.Text);
                        lbl_ToHome.Text = (Convert.ToInt32(lbl_Total.Text) - safe).ToString();
                    }
                    catch
                    {
                        msg.ShowError("يجب كتابة ارقام صحيحة فقط");
                        txt_Safe.Text = "";
                        txt_Safe.Focus();
                    }
                }
            };
        }

        private void Btn_Back_Click(object sender, EventArgs e)
        {
            Forms.sells.OpenToday();
        }

        private async void Btn_EndDay_ClickAsync(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(txt_Safe.Text))
            {
                msg.ShowError("قم بكتابة قيمة الخزنة الجديدة أولا");
                txt_Safe.Focus();
                return;
            }

            if (Visual_Scripts.AskUser("هل انت متأكد من انك تريد تقفيل اليوم؟", 'q'))
            {
                prog.Show();

                // Get New InDay
                DateTime inday = DateTime.ParseExact(Session.inDay, "yy.MM.dd_ddd", 
                    System.Globalization.CultureInfo.InvariantCulture).AddDays(1);
                string new_inday = inday.ToString("yy.MM.dd_ddd");

                await Task.Run(() => { prog.PerformStep(); });

                // Transaction
                Transaction trans = new Transaction();

                // Update Settings
                trans.AddCmd("UPDATE Settings SET Value='" + new_inday + "' WHERE Setting='InDay'");

                await Task.Run(() => { prog.PerformStep(); });

                // Insert Sellers
                List<string> Sellers = GetDataArray("SELECT Name FROM Sellers WHERE Online=0");
                foreach (string s in Sellers)
                    trans.AddCmd("INSERT INTO Times(Name, InDay) VALUES(N'" + s + "', '" + new_inday + "')");

                await Task.Run(() => { prog.PerformStep(); });

                // Insert New Safe
                trans.AddCmd("INSERT INTO Safe(Amount, InDay) VALUES(" + txt_Safe.Text + ", '" + new_inday + "')");

                await Task.Run(() => { prog.PerformStep(); });

                string tran = trans.StartTrans();

                await Task.Run(() => { prog.PerformStep(); });

                if (tran == null)
                {
                    // Done
                    MessageBox.Show("تم تقفيل اليوم بنجاح سيتم إعادة تشغيل البرنامج");
                    Application.Restart();
                }
                else
                {
                    msg.ShowError("حدث خطأ ما، برجاء المحاولة مرة اخرى");
                    if (Forms.settings.IsAdmin) MessageBox.Show(tran);
                }
            }
        }
    }
}
