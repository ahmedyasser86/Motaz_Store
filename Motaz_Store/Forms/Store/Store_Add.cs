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
        List<int> ManSizes = new List<int>();
        Msgs msg;
        public Store_Add()
        {
            InitializeComponent();

            // Focus txt_Art
            txt_Art.Focus();

            // Get Colors From DB
            cbox_Color.DataSource = GetDataArray("SELECT * FROM Colors");

            // Get Sups From DB
            cbox_Sub.DataSource = GetDataArray("SELECT Name FROM Sups");

            // Error Msgs
            msg = new Msgs(lbl_Error);

            #region Txts EnterClicks & Leave
            // ---> Txts EnterClicks & Leave

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
                        try
                        {
                            // Get Price
                            txt_Price.Text = GetDataInt("SELECT Price FROM Products_Prices WHERE Art=@Art",
                                new string[] { "@Art", txt_Art.Text }).ToString();

                            // Get Des
                            txt_Des.Text = GetDataString("SELECT Des FROM Products_Prices WHERE Art=@Art",
                                new string[] { "@Art", txt_Art.Text });

                            // Get Sup
                            cbox_Sub.Text = GetDataString("SELECT Sup FROM Products_Prices WHERE Art=@Art",
                                new string[] { "@Art", txt_Art.Text });
                        }
                        catch
                        {
                            msg.ShowError("لا يوجد منتجات بهذا الأرتكل!");
                            rb_New.Checked = true;
                            return;
                        }
                    }
                    else
                    {
                        txt_Art.Text = String.Join("", txt_Art.Text.Split(' '));
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
                        msg.ShowError("السعر يجب أن يكون أكبر من 0");
                        txt_Price.Text = "";
                        txt_Price.Focus();
                    }
                }
                catch
                {
                    msg.ShowError("السعر يجب أن يكون رقم صحيح");
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
                        msg.ShowError("الكمية يجب أن تكون أكبر من 0");
                        txt_Qty.Text = "";
                        txt_Qty.Focus();
                    }
                }
                catch
                {
                    msg.ShowError("الكمية يجب أن تكون رقم صحيح");
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
                        msg.ShowError("المقاس يجب أن يكون أكبر من 0");
                        txt_SizeFrom.Text = "";
                        txt_SizeFrom.Focus();
                    }
                }
                catch
                {
                    msg.ShowError("المقاس يجب أن يكون رقم صحيح");
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
                        msg.ShowError("المقاس يجب أن يكون أكبر من 0");
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
                            msg.ShowError("عدد المقاسات لا يقبل القسمة على الكمية");
                            txt_SizeFrom.Text = "";
                            txt_SizeTo.Text = "";
                            txt_SizeFrom.Focus();
                        }
                    }
                    catch
                    {
                        msg.ShowError("قم بكتابة العدد وبدأ المقاس أولا");
                        txt_Qty.Text = "";
                        txt_SizeFrom.Text = "";
                        txt_SizeTo.Text = "";
                        txt_Qty.Focus();
                    }
                }
                catch
                {
                    msg.ShowError("المقاس يجب أن يكون رقم صحيح");
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

                    txt_Qty.Text = ManSizes.Count.ToString();
                }
                catch
                {
                    msg.ShowError("هناك خطأ في البيانات المكتوبة، إذا كنت تواجه مشكلة في\nإستخدام النظام اليدوي، إستخدم النظام التلقائي");
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
                    cbox_Sub.Focus();
                    E.SuppressKeyPress = true;
                }
                else if (E.KeyCode == Keys.F8)
                {
                    btn_DesAuto.PerformClick();
                }
            };

            // Sup
            // EnterClick
            cbox_Sub.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter)
                {
                    Btn_Add.PerformClick();
                }
            };

            #endregion
        }

        private void Rb_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_New.Checked)
            {
                // Add New Product
                RemoveReadOnly(txt_Price);
                RemoveReadOnly(txt_Des);

            }
            else
            {
                // Add Tekrar
                MakeReadOnly(txt_Price, "0");
                MakeReadOnly(txt_Des, "");
            }

            txt_Art.Focus();
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

                txt_Size.Focus();
            }

            else
            {
                // Qty Manual
                RemoveReadOnly(txt_Qty);

                // Hide Manual TextBox
                pnl_SizeMan.Hide();

                // Show Auto TextBox
                pnl_SizeAuto.Show();

                txt_Qty.Focus();
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
                msg.ShowError("قم بكتابة الأرتكل أولا");
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
            txt_Art.Focus();
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            #region Validation Of Empty Txts
            if (String.IsNullOrWhiteSpace(txt_Art.Text))
            { txt_Art.Focus(); msg.ShowError("قم بملئ البيانات أولا"); return; }
            if (String.IsNullOrWhiteSpace(txt_Price.Text))
            { txt_Price.Focus(); msg.ShowError("قم بملئ البيانات أولا"); return; }
            if (String.IsNullOrWhiteSpace(txt_Qty.Text))
            { txt_Qty.Focus(); msg.ShowError("قم بملئ البيانات أولا"); return; }
            if (String.IsNullOrWhiteSpace(txt_Size.Text) && rb_SManual.Checked)
            { txt_Size.Focus(); msg.ShowError("قم بملئ البيانات أولا"); return; }
            if (String.IsNullOrWhiteSpace(txt_SizeFrom.Text) && !rb_SManual.Checked)
            { txt_SizeFrom.Focus(); msg.ShowError("قم بملئ البيانات أولا"); return; }
            if (String.IsNullOrWhiteSpace(txt_SizeTo.Text) && !rb_SManual.Checked)
            { txt_SizeTo.Focus(); msg.ShowError("قم بملئ البيانات أولا"); return; }
            if (String.IsNullOrWhiteSpace(txt_Des.Text))
            { txt_Des.Focus(); msg.ShowError("قم بملئ البيانات أولا"); return; }
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

            if (rb_New.Checked)
            {
                Transaction trans = new Transaction();
                #region Add The Article
                // Validation of If Available
                if (isAvailable("SELECT * FROM Products_Prices WHERE Art=@Art", new string[] { "@Art", txt_Art.Text }))
                {
                    if (!AskUser("هذا الأرتكل موجود لمنتج أخر\nسيتم تغيير أرتكل المنتج الأخر وأضافة هذا الأرتكل للمنتج الجحديد\n" +
                        "هل توافق على ذلك؟ هذه العملية لا يمكن التراجع عنها", 'e'))
                    {
                        trans.CloseTrans();
                        return;
                    }

                    else
                    {
                        // Insert New Art For The Old One
                        string Art = txt_Art.Text + "O";
                        while (isAvailable("SELECT * FROM Products_Prices WHERE Art=@Art", new string[] { "@Art", Art }))
                            Art += "O";

                        // Old Product Details
                        List<string> old_details = GetDataArray("SELECT Price, Descount, Des, Sup, F_Price FROM Products_Prices WHERE Art=@Art", 5,
                            new string[] { "@Art", txt_Art.Text });

                        trans.AddCmd("INSERT INTO Products_Prices(Art, Price, Descount, Des, sup, F_Price) VALUES('" + Art + "', " + old_details[0] +
                            ", " + old_details[1] + ", N'" + old_details[2] + "', N'" + old_details[3] + "', " + old_details[4] + ")");

                        // Update The Old Products To Their new Art
                        trans.AddCmd("UPDATE Products SET Art='" + Art + "' WHERE Art=@Art"
                            , new string[] { "@Art", txt_Art.Text });

                        // Update The Old Art With New Values
                        trans.AddCmd("UPDATE Products_Prices SET Price=" + txt_Price.Text + ", Descount=0, Des=N'" +
                            txt_Des.Text + "' WHERE Art=@Art", new string[] { "@Art", txt_Art.Text });
                    }
                }
                else
                {
                    // Get First Prist
                    // F_Price Rate Based On UserValue
                    // Get the gain if the first price = 100
                    float GainOf100 = 100 * Forms.settings.GainRate;
                    // Get the rate
                    float GainRate = GainOf100 / (100 + GainOf100);
                    // calc f_price based on the gain rate
                    int F_Price = Convert.ToInt32(txt_Price.Text) - 
                        Convert.ToInt32(Convert.ToDecimal(txt_Price.Text) * Convert.ToDecimal(GainRate));
                    // Round it to 5
                    while (F_Price % 5 != 0)
                    {
                        F_Price++;
                    }

                    // Add Product Details
                    trans.AddCmd("INSERT INTO Products_Prices(Art, Price, Descount, Des, F_Price, Sup) VALUES('" + txt_Art.Text +
                        "', " + txt_Price.Text + ", 0, N'" + txt_Des.Text + "', " + F_Price + ", N'" + cbox_Sub.Text + "')");
                }
                #endregion

                #region Add Products
                foreach(Sizes s in sizes)
                {
                    trans.AddCmd("INSERT INTO Products(Art, Color, Size, Qty) VALUES('" + txt_Art.Text + "', N'" + cbox_Color.Text
                        + "', " + s.Size + ", " + s.Qty + ");");
                }

                string tran = trans.StartTrans();
                if (tran != null)
                {
                    // Error

                    if (Forms.settings.IsAdmin)
                    {
                        MessageBox.Show(tran);
                    }
                    else
                    {
                        msg.ShowError("حدث خطأ أثناء إضافة المنتجات إلى قاعدة البيانات");
                    }
                }
                else
                {
                    // Print Barcode
                    if(cb_PrintBarcode.Checked)
                        printBarcode(sizes, txt_Art.Text, cbox_Color.Text, Convert.ToInt32(txt_Price.Text));

                    msg.ShowError("تم إضافة المنتجات بنجاح", true);
                    rb_Again.Checked = true;
                    txt_Art.Focus();
                    cbox_Color.Focus();
                }
                #endregion
            }
            else
            {
                Transaction trans = new Transaction();

                foreach(Sizes s in sizes)
                {
                    if (isAvailable("SELECT ID From Products WHERE Art=@Art AND Color=@Color AND Size=@Size",
                        new string[] { "@Art", txt_Art.Text, "@Color", cbox_Color.Text, "@Size", s.Size.ToString() }))
                    {
                        trans.AddCmd("UPDATE Products SET Qty=Qty + " + s.Qty + " WHERE Art=@Art AND Color=@Color AND Size=@Size",
                        new string[] { "@Art", txt_Art.Text, "@Color", cbox_Color.Text, "@Size", s.Size.ToString() });
                    }
                    else
                    {
                        trans.AddCmd("INSERT INTO Products(Art, Color, Size, Qty) VALUES('" + txt_Art.Text + "', N'" + cbox_Color.Text
                            + "', " + s.Size + ", " + s.Qty + ")");
                    }
                }

                string tran = trans.StartTrans();
                if (tran != null)
                {
                    // Error

                    if (Forms.settings.IsAdmin)
                    {
                        MessageBox.Show(tran);
                    }
                    else
                    {
                        msg.ShowError("حدث خطأ أثناء إضافة المنتجات إلى قاعدة البيانات");
                    }
                }
                else
                {
                    // Print Barcode
                    if (cb_PrintBarcode.Checked)
                        printBarcode(sizes, txt_Art.Text, cbox_Color.Text, Convert.ToInt32(txt_Price.Text));

                    msg.ShowError("تم إضافة المنتجات بنجاح", true);
                    cbox_Color.Focus();
                }
            }
        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            rb_New.Checked = true;
            txt_Art.Text = "";
            txt_Price.Text = "";
            rb_SManual.Checked = true;
            txt_Qty.Text = "";
            txt_Size.Text = "";
            txt_SizeFrom.Text = "";
            txt_SizeTo.Text = "";
            txt_Des.Text = "";
        }
    }
}