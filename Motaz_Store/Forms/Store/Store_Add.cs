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
using static Motaz_Store.Visual_Scripts;

namespace Motaz_Store
{

    public partial class Store_Add : Form
    {
        Timer errorShow = new Timer();
        List<int> ManSizes = new List<int>();
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
                else if(E.KeyCode == Keys.F8)
                {
                    rb_Again.Checked = true;
                }
                else if(E.KeyCode == Keys.F9)
                {
                    rb_New.Checked = true;
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
                if (String.IsNullOrWhiteSpace(txt_Price.Text)) return;
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
                if(E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Qty.Text))
                {
                    txt_SizeFrom.Focus();
                    E.SuppressKeyPress = true;
                }
                else if (E.KeyCode == Keys.F8)
                {
                    rb_SManual.Checked = true;
                    txt_Qty.Focus();
                }
                else if (E.KeyCode == Keys.F9)
                {
                    rb_SAuto.Checked = true;
                }
            };
            // Leave
            txt_Qty.Leave += (S, E) =>
            {
                if (String.IsNullOrWhiteSpace(txt_Qty.Text)) return;
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
            // EnterClick
            txt_SizeFrom.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_SizeFrom.Text))
                {
                    txt_SizeTo.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            // Leave
            txt_SizeFrom.Leave += (S, E) =>
            {
                if (String.IsNullOrWhiteSpace(txt_SizeFrom.Text)) return;
                try
                {
                    if (Convert.ToInt32(txt_SizeFrom.Text) <= 0)
                    {
                        ShowError("المقاس يجب أن يكون أكبر من 0");
                        txt_SizeFrom.Text = "";
                        txt_SizeFrom.Focus();
                    }
                }
                catch
                {
                    ShowError("المقاس يجب أن يكون رقم صحيح");
                    txt_SizeFrom.Text = "";
                    txt_SizeFrom.Focus();
                }
            };

            // --> SizeTo
            // EnterClick
            txt_SizeTo.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_SizeTo.Text))
                {
                    txt_Des.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            // Leave
            txt_SizeTo.Leave += (S, E) =>
            {
                if (String.IsNullOrWhiteSpace(txt_SizeTo.Text)) return;
                try
                {
                    if (Convert.ToInt32(txt_SizeTo.Text) <= 0)
                    {
                        ShowError("المقاس يجب أن يكون أكبر من 0");
                        txt_SizeTo.Text = "";
                        txt_SizeTo.Focus();
                    }
                    try
                    {
                        int qty = Convert.ToInt32(txt_Qty.Text);
                        int s1 = Convert.ToInt32(txt_SizeFrom.Text);
                        int s2 = Convert.ToInt32(txt_SizeTo.Text);

                        if((qty / (s2 - s1 + 1)) * (s2 - s1 + 1) != qty)
                        {
                            ShowError("عدد المقاسات لا يقبل القسمة على الكمية");
                            txt_SizeFrom.Text = "";
                            txt_SizeTo.Text = "";
                            txt_SizeFrom.Focus();
                        }
                    }
                    catch
                    {
                        ShowError("قم بكتابة العدد وبدأ المقاس أولا");
                        txt_Qty.Text = "";
                        txt_SizeFrom.Text = "";
                        txt_SizeTo.Text = "";
                        txt_Qty.Focus();
                    }
                }
                catch
                {
                    ShowError("المقاس يجب أن يكون رقم صحيح");
                    txt_SizeTo.Text = "";
                    txt_SizeTo.Focus();
                }
            };

            // --> Size
            // EnterClick
            txt_Size.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Size.Text))
                {
                    txt_Des.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            // Leave
            txt_Size.Leave += (S, E) =>
            {
                if (String.IsNullOrWhiteSpace(txt_Size.Text)) return;
                // Clear Spaces
                txt_Size.Text = String.Concat(txt_Size.Text.Where(c => !Char.IsWhiteSpace(c)));

                try
                {
                    ManSizes.Clear();
                    // Split 1
                    List<string> S1 = txt_Size.Text.Split('-').ToList();
                    foreach(string s in S1)
                    {
                        if (s.Split('*').Length > 1)
                        {
                            // Multiply
                            int size = Convert.ToInt32(s.Split('*')[0]);
                            for(int i = 0, n = Convert.ToInt32(s.Split('*')[1]); i < n; i++)
                            {
                                ManSizes.Add(size);
                            }
                        }
                        else if (s.Split('>').Length > 1)
                        {
                            // From .. To ..
                            int sStart = Convert.ToInt32(s.Split('>')[0]);
                            int sEnd = Convert.ToInt32(s.Split('>')[1]);
                            for(int i = sStart; i <= sEnd; i++)
                            {
                                ManSizes.Add(i);
                            }
                        }
                        else if (Convert.ToInt32(s) >= 0)
                        {
                            // Size
                            ManSizes.Add(Convert.ToInt32(s));
                        }
                        else throw new Exception();
                    }
                }
                catch
                {
                    ShowError("هناك خطأ في البيانات المكتوبة، إذا كنت تواجه مشكلة في\nإستخدام النظام اليدوي، إستخدم النظام التلقائي");
                    txt_Size.Text = "";
                    txt_Size.Focus();
                    ManSizes.Clear();
                }
            };

            // Des
            // EnterClick
            txt_Des.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Des.Text))
                {
                    Btn_Add.PerformClick();
                    E.SuppressKeyPress = true;
                }
                else if (E.KeyCode == Keys.F8)
                {
                    btn_DesAuto.PerformClick();
                }
            };

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

        private void Btn_Des_Auto_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(txt_Art.Text))
            {
                txt_Des.Text = Pro_Des.GetDes(txt_Art.Text.Substring(0, 2));
                txt_Des.Focus();
            }
            else
            {
                ShowError("قم بكتابة الأرتكل أولا");
                txt_Art.Focus();
            }
        }

        private void Ch_Shorcuts_CheckedChanged(object sender, EventArgs e)
        {
            if (ch_Shorcuts.Checked)
            {
                lbl_ShortArt.Show();
                lbl_ShortDes.Show();
                lbl_ShortQty.Show();
            }
            else
            {
                lbl_ShortArt.Hide();
                lbl_ShortDes.Hide();
                lbl_ShortQty.Hide();
            }
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            #region Validation Of Empty Txts
            if (String.IsNullOrWhiteSpace(txt_Art.Text))
            { txt_Art.Focus(); ShowError("قم بملئ البيانات أولا"); return; }
            if (String.IsNullOrWhiteSpace(txt_Price.Text))
            { txt_Price.Focus(); ShowError("قم بملئ البيانات أولا"); return; }
            if (String.IsNullOrWhiteSpace(txt_Qty.Text))
            { txt_Qty.Focus(); ShowError("قم بملئ البيانات أولا"); return; }
            if (String.IsNullOrWhiteSpace(txt_Size.Text) && rb_SManual.Checked)
            { txt_Size.Focus(); ShowError("قم بملئ البيانات أولا"); return; }
            if (String.IsNullOrWhiteSpace(txt_SizeFrom.Text) && !rb_SManual.Checked)
            { txt_SizeFrom.Focus(); ShowError("قم بملئ البيانات أولا"); return; }
            if (String.IsNullOrWhiteSpace(txt_SizeTo.Text) && !rb_SManual.Checked)
            { txt_SizeTo.Focus(); ShowError("قم بملئ البيانات أولا"); return; }
            if (String.IsNullOrWhiteSpace(txt_Des.Text))
            { txt_Des.Focus(); ShowError("قم بملئ البيانات أولا"); return; }
            #endregion

            // User Confirm
            if (!AskUser("هذه العملية لا يمكن التراجع عنها، تأكد من صحة البيانات\nهل انت متأكد من إضافة المنتجات؟", 'q'))
                return;

            List<Sizes> sizes = new List<Sizes>();
            #region Collect Sizes
            if (rb_SManual.Checked)
            {
                foreach (int s in ManSizes)
                {
                    bool ava = false;
                    int i;
                    int L = sizes.Count;
                    for (i = 0; i < L; i++)
                    {
                        if(s == sizes[i].Size)
                        {
                            ava = true;
                            break;
                        }
                    }
                    if (ava)
                    {
                        sizes[i].Qty++;
                    }
                    else
                    {
                        sizes.Add(new Sizes(s));
                    }
                }
            }
            else
            {
                int sQty = Convert.ToInt32(txt_Qty.Text)
                    / (Convert.ToInt32(txt_SizeTo.Text) - Convert.ToInt32(txt_SizeFrom.Text) + 1);

                for (int i = Convert.ToInt32(txt_SizeFrom.Text), L = Convert.ToInt32(txt_SizeTo.Text); i <= L; i++)
                    sizes.Add(new Sizes(i, sQty));
            }
            #endregion

            #region Add The Article
            // Validation of If Available
            if(isAvailable("SELECT * FROM Products_Prices WHERE Art=@Art", new string[] { "@Art", txt_Art.Text }))
            {
                if (!AskUser("هذا الأرتكل موجود لمنتج أخر\nسيتم تغيير أرتكل المنتج الأخر وأضافة هذا الأرتكل للمنتج الجحديد\n" +
                    "هل توافق على ذلك؟ هذه العملية لا يمكن التراجع عنها", 'e')) return;
                else
                {
                    // Insert New Art For The Old One
                    string Art = txt_Art.Text + "O";
                    while (isAvailable("SELECT * FROM Products_Prices WHERE Art=@Art", new string[] { "@Art", Art }))
                        Art += "O";

                    CmdExcute("INSERT INTO Products_Prices(Art, Price, Descount, Des) VALUES('" + Art + "', 0, 0, 'Old')");

                    // Update The Old Products To Their new Art
                    CmdExcute("UPDATE Products SET Art='" + Art + "' WHERE Art=@Art"
                        , new string[] { "@Art", txt_Art.Text });

                    // Update The Old Art With New Values
                    CmdExcute("UPDATE Products_Prices SET Price=" + txt_Price.Text + ", Discount=0, Des='" +
                        txt_Des.Text + "' WHERE Art=@Art", new string[] { "@Art", txt_Art.Text });
                }
            }
            else
            {
                CmdExcute("INSERT INTO Products_Prices(Art, Price, Descount, Des) VALUES('" + txt_Art.Text +
                    "', " + txt_Price.Text + ", 0, '" + txt_Des.Text + "')");
            }
            #endregion
        }
    }
}