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
        string art = null;
        public Store_Edit()
        {
            InitializeComponent();

            // Error Message Handler
            msg = new Msgs(lbl_Error);

            // Focus txtArt
            txt_Art.Focus();

            if (Session.isManager)
                txt_FPrice.PasswordChar = '\0';

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
                    if (txt_Art.Text == art) return;

                    Values = GetDataArray("SELECT Price, Descount, Des, F_Price FROM Products_Prices WHERE Art=@Art",
                        4, new string[] { "@Art", txt_Art.Text });

                    if(Values.Count > 0)
                    {
                        txt_Price.Text = Values[0];
                        txt_Discount.Text = Values[1];
                        txt_Des.Text = Values[2];
                        txt_FPrice.Text = Values[3];
                        lbl_Price.Text = (Convert.ToInt32(txt_Price.Text) - Convert.ToInt32(txt_Discount.Text)).ToString();
                        art = txt_Art.Text;
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
                        txt_Price.Focus();
                    }
                    else
                    {
                        MakeReadOnly(txt_Price, 0);
                        // txt_Art.Focus();
                    }
                }
                else
                {
                    ch_Price.Checked = false;
                    txt_Art.Focus();
                }
            };

            // --> txt Price
            // Enter
            txt_Price.KeyDown += (S, E) =>
            {
                if(E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Art.Text))
                {
                    txt_Discount.Focus();
                }
                else if(E.KeyCode == Keys.F8)
                {
                    if (ch_Price.Checked)
                        ch_Price.Checked = false;
                    else ch_Price.Checked = true;
                }
            };
            // Leave
            txt_Price.Leave += (S, E) =>
            {
                if (String.IsNullOrWhiteSpace(txt_Art.Text)) return;

                if(String.IsNullOrWhiteSpace(txt_Price.Text) || txt_Price.Text == Values[0])
                {
                    ch_Price.Checked = false;
                }
            };
            // textChanged
            txt_Price.TextChanged += (S, E) =>
            {
                try
                {
                    Convert.ToInt32(txt_Price.Text);
                    lbl_Price.Text = (Convert.ToInt32(txt_Price.Text) - Convert.ToInt32(txt_Discount.Text)).ToString();
                }
                catch
                {
                    if(!String.IsNullOrWhiteSpace(txt_Price.Text))
                    {
                        txt_Price.Text = Values[0];
                        msg.ShowError("السعر يجب أن يكون رقم صحيح");
                    }
                }
            };


            // --> ch Dis
            ch_Dis.CheckedChanged += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Art.Text))
                {
                    if (ch_Dis.Checked)
                    {
                        RemoveReadOnly(txt_Discount);
                        txt_Discount.Focus();
                    }
                    else
                    {
                        MakeReadOnly(txt_Discount, 1);
                        // txt_Art.Focus();
                    }

                }
                else
                {
                    ch_Dis.Checked = false;
                    txt_Art.Focus();
                }
            };

            // --> txt Discount
            // Enter
            txt_Discount.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Art.Text))
                {
                    txt_Des.Focus();
                }
                else if (E.KeyCode == Keys.F8)
                {
                    if (ch_Dis.Checked)
                        ch_Dis.Checked = false;
                    else ch_Dis.Checked = true;
                }
            };

            // Leave
            txt_Discount.Leave += (S, E) =>
            {
                if (String.IsNullOrWhiteSpace(txt_Art.Text)) return;

                if (String.IsNullOrWhiteSpace(txt_Discount.Text) || txt_Discount.Text == Values[1])
                {
                    ch_Dis.Checked = false;
                }
            };
            // textChanged
            txt_Discount.TextChanged += (S, E) =>
            {
                try
                {
                    Convert.ToInt32(txt_Discount.Text);
                    lbl_Price.Text = (Convert.ToInt32(txt_Price.Text) - Convert.ToInt32(txt_Discount.Text)).ToString();
                }
                catch
                {
                    if (!String.IsNullOrWhiteSpace(txt_Discount.Text))
                    {
                        txt_Discount.Text = Values[1];
                        msg.ShowError("السعر يجب أن يكون رقم صحيح");
                    }
                }
            };

            // --> ch Des
            ch_Des.CheckedChanged += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Art.Text))
                {
                    if (ch_Des.Checked)
                    {
                        RemoveReadOnly(txt_Des);
                        txt_Des.Focus();
                    }
                    else
                    {
                        MakeReadOnly(txt_Des, 2);
                        // txt_Art.Focus();
                    }

                }
                else
                {
                    ch_Des.Checked = false;
                    txt_Art.Focus();
                }
            };

            // --> txt Des
            // Enter
            txt_Des.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Art.Text))
                {
                    txt_FPrice.Focus();
                }
                else if (E.KeyCode == Keys.F8)
                {
                    if (ch_Des.Checked)
                        ch_Des.Checked = false;
                    else ch_Des.Checked = true;
                }
            };

            // Leave
            txt_Des.Leave += (S, E) =>
            {
                if (String.IsNullOrWhiteSpace(txt_Art.Text)) return;

                if (String.IsNullOrWhiteSpace(txt_Des.Text) || txt_Des.Text == Values[2])
                {
                    ch_Des.Checked = false;
                }
            };

            // --> ch F_Price
            ch_FPrice.CheckedChanged += (S, E) =>
              {
                  if (!String.IsNullOrWhiteSpace(txt_Art.Text))
                  {
                      if (ch_FPrice.Checked)
                      {
                          RemoveReadOnly(txt_FPrice);
                          txt_FPrice.Focus();
                      }
                      else
                      {
                          MakeReadOnly(txt_FPrice, 3);
                          // txt_FPrice.Focus();
                      }

                  }
                  else
                  {
                      ch_FPrice.Checked = false;
                      txt_Art.Focus();
                  }
              };

            // --> txt F_Price
            // Enter
            txt_FPrice.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Art.Text))
                {
                    Btn_Save.Focus();
                    Btn_Save.PerformClick();
                }
                else if (E.KeyCode == Keys.F8)
                {
                    if (ch_FPrice.Checked)
                        ch_FPrice.Checked = false;
                    else ch_FPrice.Checked = true;
                }
            };
            // TextChanged
            txt_FPrice.TextChanged += (S, E) =>
            {
                try
                {
                    Convert.ToInt32(txt_FPrice.Text);
                }
                catch
                {
                    if (!String.IsNullOrWhiteSpace(txt_FPrice.Text))
                    {
                        txt_FPrice.Text = Values[3];
                        msg.ShowError("السعر يجب أن يكون رقم صحيح");
                    }
                }
            };
            // Leave
            txt_FPrice.Leave += (S, E) =>
            {
                if (String.IsNullOrWhiteSpace(txt_Art.Text)) return;

                if (String.IsNullOrWhiteSpace(txt_FPrice.Text) || txt_FPrice.Text == Values[3])
                {
                    ch_FPrice.Checked = false;
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
            if(ch_FPrice.Checked || ch_Price.Checked || ch_Des.Checked || ch_Dis.Checked)
            {
                if(CmdExcute("UPDATE Products_Prices SET Price=" + txt_Price.Text
                    + ", F_Price=" + txt_FPrice.Text + ", Descount=" + txt_Discount.Text
                    + ", Des=N'" + txt_Des.Text + "' WHERE Art=@Art", new string[] { "@Art", txt_Art.Text }))
                {
                    msg.ShowError("تم تعديل بيانات المنتج بنجاح", true);
                }
            }
            ch_FPrice.Checked = false;
            ch_Price.Checked = false;
            ch_Des.Checked = false;
            ch_Dis.Checked = false;

            txt_Art.Text = "";
            art = "";
            txt_Des.Text = "";
            txt_Discount.Text = "0";
            txt_FPrice.Text = "0";
            txt_Price.Text = "0";
            Values.Clear();

            txt_Art.Focus();
        }
    }
}
