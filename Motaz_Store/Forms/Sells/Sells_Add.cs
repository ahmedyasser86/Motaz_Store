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
    public partial class Sells_Add : Form
    {
        Msgs msg;
        List<string> Sellers;
        List<string> Sellers_Online;
        List<Product> Products = new List<Product>();
        int I = 1;
        int tmp;
        public Sells_Add()
        {
            InitializeComponent();

            // Get Sellers from DB
            Sellers = GetDataArray("SELECT Name FROM Sellers WHERE Online=0");

            // Get Online Sellers from DB
            Sellers_Online = GetDataArray("SELECT Name FROM Sellers WHERE Online=1");

            // Get Colors to combobox from DB
            cbox_Color.DataSource = GetDataArray("SELECT * FROM Colors");

            // Messages Handler
            msg = new Msgs(lbl_Error);

            #region Controls' enter leave Events
            // cbox_Seller
            // -----
            cbox_Seller.TextChanged += (S, E) =>
            {
                // Focus on TextBox
                if (ch_Code.Checked) txt_Art.Focus();
                else txt_Code.Focus();
            };
            // --------------------
            // txt_Code
            // -----
            // - EnterClick
            txt_Code.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Code.Text) && E.KeyCode == Keys.Enter)
                {
                    Btn_Add.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            // -------------------
            // txt_Art
            // ------
            // - EnterClick
            txt_Art.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Art.Text) && E.KeyCode == Keys.Enter)
                {
                    cbox_Color.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            // -------------------
            // cbox_Color
            // ------
            // - EnterClick
            cbox_Color.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter)
                {
                    txt_Size.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            // -------------------
            // txt_Size
            // ------
            // - EnterClick
            txt_Size.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Size.Text) && E.KeyCode == Keys.Enter)
                {
                    Btn_Add.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            // - TextChanged -> Handle Entering non-numbers
            txt_Size.TextChanged += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Size.Text))
                {
                    try
                    {
                        Convert.ToInt32(txt_Size.Text);
                    }
                    catch
                    {
                        txt_Size.Text = "";
                        msg.ShowError("يجب إدخال ارقام صحيحة فقط للمقاس");
                        txt_Size.Focus();
                    }
                }
            };
            // -------------------
            // txt_Paid
            // ------
            // - EnterClick
            txt_Paid.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Size.Text) && E.KeyCode == Keys.Enter)
                {
                    btn_Save.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            // - TextChanged -> Handle Entering non-numbers
            txt_Paid.TextChanged += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Paid.Text))
                {
                    try
                    {
                        txt_Baqy.Text = (Convert.ToInt32(txt_Paid.Text) - Convert.ToInt32(lbl_toPay.Text)).ToString();
                    }
                    catch
                    {
                        txt_Paid.Text = "";
                        msg.ShowError("يجب إدخال ارقام صحيحة فقط للمقاس");
                        txt_Paid.Focus();
                    }
                }
                else
                {
                    txt_Baqy.Text = "0";
                }
            };
            #endregion

            Start_Up();
        }

        private void Start_Up()
        {
            rb_Store.Checked = true;
            rb_Print.Checked = true;
            rb_Cash.Checked = true;
        }

        private void Ch_Code_CheckedChanged(object sender, EventArgs e)
        {
            if (ch_Code.Checked)
            {
                pnl_Code.Hide();
                pnl_Art.Show();
                txt_Art.Focus();
            }
            else
            {
                pnl_Code.Show();
                pnl_Art.Hide();
                txt_Code.Focus();
            }
        }

        // TODO: Shortcuts
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            Product p;
            // Validate Txts
            // Get Product from db
            if (ch_Code.Checked)
            {
                if (String.IsNullOrWhiteSpace(txt_Art.Text) || String.IsNullOrWhiteSpace(txt_Size.Text))
                { msg.ShowError("يجب ملئ جميع الحقول"); return; }

                p = GetProduct(txt_Art.Text, cbox_Color.Text, Convert.ToInt32(txt_Size.Text));
            }
            else
            {
                if (String.IsNullOrWhiteSpace(txt_Code.Text))
                { msg.ShowError("يجب ملئ جميع الحقول"); return; }
                p = GetProduct(Convert.ToInt32(txt_Code.Text));
            }

            // Check avalablity
            if (p == null)
            { msg.ShowError("لا يوجد منتج بهذه المواصفات"); return; }

            // Check if the product already exists
            bool ex = false;
            foreach (Product pr in Products)
            {
                if (pr.Code == p.Code)
                { ex = true; break; }
            }

            // if exists: Change qty
            if (ex)
            {
                if (!Change_Qty(p.Code))
                { msg.ShowError("الكمية المطلوبة ليست متوفرة في المخزن"); return; }
            }
            else
            {
                // else: Add New
                // if avalable in store
                if (p.Qty < 1)
                { msg.ShowError("الكمية المطلوبة ليست متوفرة في المخزن"); return; }

                DataGridViewRow r = new DataGridViewRow();
                string desc = p.Des + " " + p.Art + " " + p.Color + " " + p.Size;
                r.CreateCells(dgv_Sells, I++, p.Code, desc, p.Price, 1, p.Dis, (p.Price - p.Dis), "مسح");
                dgv_Sells.Rows.Add(r);
                Products.Add(p);
            }

            Calc_Totals();

            if (ch_Code.Checked)
            { ch_Code.Checked = false; }
            else
            { txt_Code.Text = ""; txt_Code.Focus(); }
        }

        private void rb_Sell_Type(object sender, EventArgs e)
        {
            if (rb_Online.Checked)
            {
                cbox_Seller.DataSource = Sellers_Online;
                rb_Agel.Checked = true;
            }
            else
            {
                cbox_Seller.DataSource = Sellers;
            }

            // Focus on TextBox
            if (ch_Code.Checked) txt_Art.Focus();
            else txt_Code.Focus();
        }

        private void rb_Payment(object sender, EventArgs e)
        {
            // Focus on TextBox
            if (ch_Code.Checked) txt_Art.Focus();
            else txt_Code.Focus();
        }

        private void rb_print(object sender, EventArgs e)
        {
            // Focus on TextBox
            if (ch_Code.Checked) txt_Art.Focus();
            else txt_Code.Focus();
        }

        void Calc_Totals()
        {
            I = 1;

            int total = 0;
            int dis = 0;
            int topay = 0;

            foreach (DataGridViewRow r in dgv_Sells.Rows)
            {
                r.Cells[0].Value = I++;
                total += Convert.ToInt32(r.Cells[3].Value) * Convert.ToInt32(r.Cells[4].Value);
                dis += Convert.ToInt32(r.Cells[5].Value);
                topay += Convert.ToInt32(r.Cells[6].Value);
            }

            lbl_Dis.Text = dis.ToString();
            lbl_Total.Text = total.ToString();
            lbl_toPay.Text = topay.ToString();

            if (!String.IsNullOrWhiteSpace(txt_Paid.Text))
            {
                txt_Baqy.Text = (Convert.ToInt32(txt_Paid.Text) - Convert.ToInt32(lbl_toPay.Text)).ToString();
            }
            else
            {
                txt_Baqy.Text = "0";
            }
        }

        bool Change_Qty(int code, int qty)
        {
            int max_qty = 0;
            int pr = 0;
            int di = 0;
            foreach (Product p in Products)
            {
                if (p.Code == code)
                {
                    max_qty = p.Qty;
                    pr = p.Price;
                    di = p.Dis;
                    break;
                }
            }
            if (qty > max_qty)
            {
                return false;
            }
            else
            {
                foreach (DataGridViewRow r in dgv_Sells.Rows)
                {
                    if (Convert.ToInt32(r.Cells[1].Value) == code)
                    {
                        r.Cells[5].Value = qty * di;
                        r.Cells[6].Value = qty * pr - qty * di;
                        r.Cells[4].Value = qty;
                    }
                }
                Calc_Totals();
                return true;
            }
        }
        bool Change_Qty(int code)
        {
            foreach (DataGridViewRow r in dgv_Sells.Rows)
            {
                if (Convert.ToInt32(r.Cells["Code"].Value) == code)
                {
                    return Change_Qty(Convert.ToInt32(r.Cells["Code"].Value), Convert.ToInt32(r.Cells["Qty"].Value) + 1);
                }
            }
            return false;
        }

        private void Dgv_Sells_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                int code = Convert.ToInt32(dgv_Sells.Rows[e.RowIndex].Cells[1].Value);
                dgv_Sells.Rows.RemoveAt(e.RowIndex);
                foreach (Product p in Products)
                {
                    if (p.Code == code)
                    {
                        Products.Remove(p);
                        break;
                    }
                }
                Calc_Totals();
                txt_Code.Focus();
            }
        }

        private void Dgv_Sells_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            tmp = Convert.ToInt32(dgv_Sells.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
        }

        private void Dgv_Sells_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int tmp2 = Convert.ToInt32(dgv_Sells.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                if (e.ColumnIndex == 4)
                {
                    // Change Qty
                    if (!Change_Qty(Convert.ToInt32(dgv_Sells.Rows[e.RowIndex].Cells[1].Value), tmp2) ||
                        Convert.ToInt32(dgv_Sells.Rows[e.RowIndex].Cells[1].Value) <= 0)
                    {
                        dgv_Sells.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = tmp;
                        msg.ShowError("الكمية المطلوبة غير متوفرة في المخزن");
                    }
                }
                else
                {
                    dgv_Sells.Rows[e.RowIndex].Cells[6].Value = Convert.ToInt32(dgv_Sells.Rows[e.RowIndex].Cells[3].Value)
                        * Convert.ToInt32(dgv_Sells.Rows[e.RowIndex].Cells[4].Value) - tmp2;

                    Calc_Totals();
                }
            }
            catch
            {
                dgv_Sells.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = tmp;
                msg.ShowError("يجب إضافة رقم صحيح");
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            // Clear Products
            dgv_Sells.Rows.Clear();
            Products.Clear();
            // Focus TextBox
            if (ch_Code.Checked) txt_Art.Focus();
            else txt_Code.Focus();
            // Empty txts
            txt_Paid.Text = "";
            // Zero totals
            Calc_Totals();
        }

        private async void Btn_Save_ClickAsync(object sender, EventArgs e)
        {
            // Validation
            if (dgv_Sells.Rows.Count <= 0)
            {
                msg.ShowError("قم بإضافة منتجات اولا");
                if (ch_Code.Checked) txt_Art.Focus();
                else txt_Code.Focus();
                return;
            }

            Transaction trans = new Transaction(true);

            List<Product> products = new List<Product>();

            int Bill_ID = 0;
            // Store cach
            if (rb_Cash.Checked)
            {
                // ---> INSERT INTO Bills
                // Get The next bill id
                Bill_ID = GetDataInt("SELECT ISNULL(MAX(ID), 0) FROM Bills") + 1;
                // insert
                trans.AddCmd("INSERT INTO Bills(ID, Seller, Casher, inDay) Values(" + Bill_ID + ", N'" + cbox_Seller.Text + "', '" +
                    Session.username + "', '" + Session.inDay + "')");

                // insert into sells
                foreach (DataGridViewRow r in dgv_Sells.Rows)
                {
                    trans.AddCmd("INSERT INTO Sells(P_ID, B_ID, Price, QTY, Discount, Date_Time, InDay) VALUES(" +
                        r.Cells["Code"].Value + ", " + Bill_ID + ", " + r.Cells["Price"].Value + ", " + r.Cells["Qty"].Value
                        + ", " + r.Cells["Discount"].Value + ", '" + DateTime.Now + "', '" + Session.inDay + "')");

                    // Update Products List
                    foreach(Product p in Products)
                    {
                        if(p.Code == Convert.ToInt32(r.Cells["Code"].Value))
                        {
                            p.Dis = Convert.ToInt32(r.Cells["Discount"].Value);
                            p.Qty = Convert.ToInt32(r.Cells["Qty"].Value);
                        }
                    }
                }
            }

            // Store agel
            else if (rb_Store.Checked && rb_Agel.Checked)
            {
                foreach (DataGridViewRow r in dgv_Sells.Rows)
                {
                    trans.AddCmd("INSERT INTO WithDraws(P_ID, QTY, Branch, Date_Time, InDay) VALUES(" + r.Cells["Code"].Value
                        + ", " + r.Cells["Qty"].Value + ", 'Agel_Store', '" + DateTime.Now + "', '" + Session.inDay + "')");
                }
            }

            // Online agel
            else if (rb_Online.Checked && rb_Agel.Checked)
            {
                foreach (DataGridViewRow r in dgv_Sells.Rows)
                {
                    trans.AddCmd("INSERT INTO Online(P_ID, Qty, Seller, Date_Time, Price, Dis, Des) VALUES(" + r.Cells["Code"].Value
                        + ", " + r.Cells["Qty"].Value + ", N'" + cbox_Seller.Text + "', '" + DateTime.Now + "', " + r.Cells["Price"].Value
                        + ", " + r.Cells["Discount"].Value + ", N'" + r.Cells["Product"].Value + "')");
                }
            }

            // Print Reciept
            if(rb_Print.Checked && rb_Cash.Checked)
            {
                Bill bill = new Bill(Bill_ID, Session.inDay, Session.username, cbox_Seller.Text, Products);
                int paid;
                if (String.IsNullOrWhiteSpace(txt_Paid.Text))
                    paid = 0;
                else
                    paid = Convert.ToInt32(txt_Paid.Text);
                await Task.Run(() => { bill.Print_Bill(paid); });   
            }

            // Save
            string tran = trans.StartTrans();

            if (tran != null)
            {
                msg.ShowError("حدث خطأ ما، برجاء المحاولة مرة اخرى");
                if (Forms.settings.IsAdmin) MessageBox.Show(tran);
            }
            else
            {
                msg.ShowError("تم إضافة البيعة بنجاح", true);
                btn_Cancel.PerformClick();
                if(rb_Cash.Checked)
                {
                    await Task.Run(() =>
                    {
                        Forms.sells_Today.Btn_Refresh_Click(Forms.sells_Today.btn_Refresh, e);
                    });
                }
            }
        }
    }   
}
