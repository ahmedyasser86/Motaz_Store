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
    public partial class Store_Edit : Form
    {
        Msgs msg;
        List<string> Values = new List<string>();
        public Store_Edit()
        {
            InitializeComponent();

            // Error Message Handler
            msg = new Msgs(lbl_Error);

            // Focus txtArt
            txt_Art.Focus();

            #region Txts EnterClicks & Leave
            // --> txt Art
            // Enter
            txt_Art.KeyDown += (S, E) =>
            {
                if(!String.IsNullOrWhiteSpace(txt_Art.Text) && E.KeyCode == Keys.Enter)
                {
                    txt_Price.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            // Leave
            txt_Art.Leave += (S, E) =>
            {
                if(!String.IsNullOrWhiteSpace(txt_Art.Text))
                {
                    Values = GetDataArray("SELECT Price, Descount, Des FROM Products_Prices WHERE Art=@Art",
                        3, new string[] { "@Art", txt_Art.Text });

                    if(Values.Count > 0)
                    {
                        txt_Price.Text = Values[0];
                        txt_Discount.Text = Values[1];
                        txt_Des.Text = Values[2];
                        lbl_Price.Text = (Convert.ToInt32(txt_Price.Text) - Convert.ToInt32(txt_Discount.Text)).ToString();
                    }
                    else
                    {
                        msg.ShowError("حدث خطأ ما، تأكد من البيانات المدخلة");
                        txt_Art.Text = "";
                        txt_Price.Text = "0";
                        txt_Discount.Text = "0";
                        txt_Des.Text = "";
                        lbl_Price.Text = "000";
                        txt_Art.Focus();
                    }
                }
            };

            // --> ch Price
            ch_Price.CheckedChanged += (S, E) =>
            {
                if(!String.IsNullOrWhiteSpace(txt_Art.Text))
                {
                    if (ch_Price.Checked)
                    {
                        RemoveReadOnly(txt_Price);
                    }
                    else
                    {
                        MakeReadOnly(txt_Price, 0);
                    }
                }
                else
                {
                    ch_Price.Checked = false;
                }
            };

            // --> txt Price
            // Enter
            txt_Price.KeyDown += (S, E) =>
            {
                if(E.KeyCode == Keys.Enter)
                {
                    txt_Discount.Focus();
                }
                else if(E.KeyCode == Keys.F8 && !String.IsNullOrWhiteSpace(txt_Art.Text))
                {
                    if (ch_Price.Checked)
                        ch_Price.Checked = false;
                    else ch_Price.Checked = true;
                }
            };
            // Leave
            // Change Leave to textChanged
            txt_Price.Leave += (S, E) =>
            {
                try
                {
                    Convert.ToInt32(txt_Price.Text);
                    lbl_Price.Text = (Convert.ToInt32(txt_Price.Text) - Convert.ToInt32(txt_Discount.Text)).ToString();
                }
                catch
                {
                    txt_Price.Text = Values[0];
                    msg.ShowError("السعر يجب أن يكون رقم صحيح");
                }
            };
            #endregion
        }

        private void MakeReadOnly(TextBox txt, int index)
        {
            txt.Text = Values[index];
            txt.ReadOnly = true;
            txt.BackColor = Color.FromArgb(230, 202, 168);
        }
        private void RemoveReadOnly(TextBox txt)
        {
            txt.ReadOnly = false;
            txt.BackColor = Color.FromArgb(255, 244, 230);
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {

        }
    }
}
