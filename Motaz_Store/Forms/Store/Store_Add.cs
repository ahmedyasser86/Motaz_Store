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
    public partial class Store_Add : Form
    {
        Timer errorShow = new Timer();
        public Store_Add()
        {
            InitializeComponent();

            // Make Selected Index of cBox Type
            cbox_Type.SelectedIndex = 0;

            // Focus txt_Art
            txt_Art.Focus();

            // Get Colors From DB
            cbox_Color.DataSource = GetDataArray("SELECT * FROM Colors");

            // Error Msg Control
            errorShow.Interval = 3000;
            errorShow.Tick += (s, e) =>
            {
                lbl_Error.Hide();
                errorShow.Stop();
            };

            #region Txts EnterClicks & Leave
            // ---> Txts EnterClicks & Leave

            // --> Type
            // EnterClick
            cbox_Type.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter)
                {
                    txt_Art.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            // Leave
            // NoThing TODO.

            // --> Art
            // EnterClick
            txt_Art.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Art.Text))
                {
                    cbox_Color.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            // Leave
            txt_Art.Leave += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Art.Text))
                {
                    if (rb_Again.Checked)
                    {
                        // Get Colors From DB
                        cbox_Color.DataSource = GetDataArray("SELECT Distinct(Color) FROM Products WHERE Art=@Art",
                            new string[] { "@Art", txt_Art.Text });

                        // Check If Art Exist
                        if (cbox_Color.Items.Count < 1)
                        {
                            ShowError("لا يوجد منتجات بهذا الأرتكل!");
                            txt_Art.Text = "";
                            txt_Art.Focus();
                            return;
                        }

                        // Get Price
                        txt_Price.Text = GetDataInt("SELECT Price FROM Products_Prices WHERE Art=@Art",
                            new string[] { "@Art", txt_Art.Text }).ToString();

                        // Get Des
                        txt_Des.Text = GetDataString("SELECT Des FROM Products_Prices WHERE Art=@Art",
                            new string[] { "@Art", txt_Art.Text });

                        // Get Type
                        if (txt_Art.Text.Substring(0, 1) == "B" || txt_Art.Text.Substring(0, 1) == "A")
                            cbox_Type.Text = "إكسسوار";
                        else cbox_Type.Text = "حذاء";
                    }
                    else
                    {
                        cbox_Color.DataSource = GetDataArray("SELECT * FROM Colors");
                    }
                }
            };

            // --> Color
            // EnterClick
            cbox_Color.KeyDown += (S, E) =>
            {
                if(E.KeyCode == Keys.Enter)
                {
                    if (rb_Again.Checked)
                    {
                        if (rb_SAuto.Checked)
                        {
                            txt_Qty.Focus();
                        }
                        else
                        {
                            txt_Size.Focus();
                        }
                    }
                    else
                    {
                        txt_Price.Focus();
                    }
                }

                E.SuppressKeyPress = true;
            };

            // --> Price
            // EnterClick
            txt_Price.KeyDown += (S, E) =>
            {
                if(E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Price.Text))
                {
                    if (rb_SManual.Checked)
                    {
                        txt_Size.Focus();
                    }
                    else
                    {
                        txt_Qty.Focus();
                    }

                    E.SuppressKeyPress = true;
                }
            };
            // Leave
            txt_Price.Leave += (S, E) =>
            {
                try
                {
                    if(Convert.ToInt32(txt_Price.Text) <= 0)
                    {
                        ShowError("السعر يجب أن يكون أكبر من 0");
                        txt_Price.Text = "";
                        txt_Price.Focus();
                    }
                }
                catch
                {
                    ShowError("السعر يجب أن يكون رقم صحيح");
                    txt_Price.Text = "";
                    txt_Price.Focus();
                }
            };

            // --> Qty
            // EnterClick
            txt_Qty.KeyDown += (S, E) =>
            {
                if(E.KeyCode == Keys.Enter && String.IsNullOrWhiteSpace(txt_Qty.Text))
                {
                    txt_SizeFrom.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            // Leave
            txt_Qty.Leave += (S, E) =>
            {
                try
                {
                    if(Convert.ToInt32(txt_Qty.Text) <= 0 && rb_SAuto.Checked)
                    {
                        ShowError("الكمية يجب أن تكون أكبر من 0");
                        txt_Qty.Text = "";
                        txt_Qty.Focus();
                    }
                }
                catch
                {
                    ShowError("الكمية يجب أن تكون رقم صحيح");
                    txt_Qty.Text = "";
                    txt_Qty.Focus();
                }
            };

            // --> SizeFrom


            // --> SizeTo


            // --> Size


            // Des

            #endregion
        }

        public void ShowError(string err)
        {
            lbl_Error.Text = err;
            lbl_Error.Show();
            errorShow.Start();
            System.Media.SystemSounds.Beep.Play();
        }

        private void Rb_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_New.Checked)
            {
                // Add New Product
                RemoveReadOnly(txt_Price);
                RemoveReadOnly(txt_Des);
                cbox_Type.Items.Clear();
                cbox_Type.Items.Add("حذاء"); cbox_Type.Items.Add("إكسسوار");
                cbox_Type.SelectedIndex = 0;
                cbox_Type.BackColor = Color.FromArgb(255, 244, 230);
                // Get Colors From DB
                cbox_Color.DataSource = GetDataArray("SELECT * FROM Colors");

            }
            else
            {
                // Add Tekrar
                MakeReadOnly(txt_Price, "0");
                MakeReadOnly(txt_Des, "");
                cbox_Type.Items.Clear();
                cbox_Type.BackColor = Color.FromArgb(209, 178, 140);
                cbox_Color.DataSource = null;
            }
        }

        void MakeReadOnly(TextBox txt, string text)
        {
            txt.ReadOnly = true;
            txt.BackColor = Color.FromArgb(209, 178, 140);
            txt.Text = text;
        }
        void RemoveReadOnly(TextBox txt)
        {
            txt.ReadOnly = false;
            txt.BackColor = Color.FromArgb(255, 244, 230);
            txt.Text = "";
        }

        private void rb_Size_CheckChanged(object sender, EventArgs e)
        {
            if (rb_SManual.Checked)
            {
                // Qty Not Manual
                MakeReadOnly(txt_Qty, "0");

                // Show Manual TextBox
                pnl_SizeMan.Show();

                // Hide Auto TextBox
                pnl_SizeAuto.Hide();
            }

            else
            {
                // Qty Manual
                RemoveReadOnly(txt_Qty);

                // Hide Manual TextBox
                pnl_SizeMan.Hide();

                // Show Auto TextBox
                pnl_SizeAuto.Show();
            }
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {

        }
    }
}
