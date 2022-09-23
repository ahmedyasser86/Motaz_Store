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
    public partial class Sells_Del : Form
    {
        Msgs msg;
        List<Bill> bills;
        int bill_i = 0;
        List<Product> Products = new List<Product>();
        int tmp;

        public Sells_Del()
        {
            InitializeComponent();

            // Messages Handler
            msg = new Msgs(lbl_Error);

            // Load Colors
            List<string> Colors = GetDataArray("SELECT * FROM Colors");
            cbox_Color.DataSource = Colors;
            cbox_sColor.DataSource = Colors;

            #region Enter Clicks & Leave

            // txt_BillID
            // ----
            // - EnterClick
            txt_BillID.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_BillID.Text) && E.KeyCode == Keys.Enter)
                {
                    btn_Edit.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            // ------------------

            // txt_sCode
            // ----
            // - EnterClick
            txt_sCode.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_sCode.Text) && E.KeyCode == Keys.Enter)
                {
                    btn_Search.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            // -------------------

            // txt_sArt
            // ------
            // - EnterClick
            txt_sArt.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_sArt.Text) && E.KeyCode == Keys.Enter)
                {
                    cbox_sColor.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            // -------------------
            // cbox_sColor
            // ------
            // - EnterClick
            cbox_sColor.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter)
                {
                    txt_sSize.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            // -------------------
            // txt_sSize
            // ------
            // - EnterClick
            txt_sSize.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_sSize.Text) && E.KeyCode == Keys.Enter)
                {
                    btn_Search.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            // - TextChanged -> Handle Entering non-numbers
            txt_sSize.TextChanged += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_sSize.Text))
                {
                    try
                    {
                        Convert.ToInt32(txt_sSize.Text);
                    }
                    catch
                    {
                        txt_sSize.Text = "";
                        msg.ShowError("يجب إدخال ارقام صحيحة فقط للمقاس");
                        txt_sSize.Focus();
                    }
                }
            };
            // -------------------
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

            #endregion
        }

        #region Normal Functions
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
                foreach (DataGridViewRow r in dgv_New.Rows)
                {
                    if (Convert.ToInt32(r.Cells[1].Value) == code)
                    {
                        r.Cells[5].Value = qty * di;
                        r.Cells[6].Value = qty * pr - qty * di;
                        r.Cells[4].Value = qty;
                    }
                }
                calc_Totals();
                return true;
            }
        }
        bool Change_Qty(int code)
        {
            foreach (DataGridViewRow r in dgv_New.Rows)
            {
                if (Convert.ToInt32(r.Cells["Code"].Value) == code)
                {
                    return Change_Qty(Convert.ToInt32(r.Cells["Code"].Value), Convert.ToInt32(r.Cells["Qty"].Value) + 1);
                }
            }
            return false;
        }

        private void calc_Totals()
        {
            int total = Convert.ToInt32(lbl_Old.Text);
            foreach(DataGridViewRow r in dgv_New.Rows)
            {
                total += Convert.ToInt32(r.Cells[6].Value);
            }
            lbl_Total.Text = total.ToString();
            lbl_toPay.Text = (total - Convert.ToInt32(lbl_Old.Text)).ToString();
        }

        private void Add_Bill_To_DGV(Bill bill)
        {
            lbl_Bill_ID.Text = bill.ID.ToString();
            lbl_Casher.Text = bill.Casher;
            lbl_Seller.Text = bill.Seller;
            lbl_Date.Text = bill.DateTime;

            int old = 0;
            foreach (Product p in bill.Products)
            {
                DataGridViewRow r = new DataGridViewRow();
                string des = p.Des + " " + p.Art + " " + p.Color + " " + p.Size;
                // Calc Total..
                int t = 0;
                int stat = 0;
                if (p.Qty > 0)
                {
                    t = p.Qty * p.Price - p.Dis;
                    old += t;
                    stat = 1;
                }
                r.CreateCells(dgv_Old, dgv_Old.Rows.Count + 1, p.Code, des, p.Price, p.Qty, p.Dis, t, p.inDay, "إرجاع", stat);
                dgv_Old.Rows.Add(r);
            }
            lbl_Old.Text = old.ToString();
            calc_Totals();
        }
        #endregion

        private void Ch_ByProduct_CheckedChanged(object sender, EventArgs e)
        {
            if(ch_ByProduct.Checked)
            {
                pnl_BillID.Hide();
                pnl_Search.Show();
                txt_sCode.Focus();
            }
            else
            {
                if(ch_ByArt.Checked)
                {
                    ch_ByArt.Checked = false;
                }
                pnl_BillID.Show();
                pnl_Search.Hide();
                txt_BillID.Focus();
            }
        }

        private void Ch_ByArt_CheckedChanged(object sender, EventArgs e)
        {
            if(ch_ByArt.Checked)
            {
                if (!ch_ByProduct.Checked) ch_ByProduct.Checked = true;

                pnl_sArt.Show();
                pnl_sCode.Hide();
                txt_sArt.Focus();
            }
            else
            {
                pnl_sArt.Hide();
                pnl_sCode.Show();
                txt_sCode.Focus();
            }
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            btn_Cancel.PerformClick();
            // Get Bills
            if (ch_ByArt.Checked && ch_ByProduct.Checked)
            {
                // VALIDATION
                if(String.IsNullOrWhiteSpace(txt_sArt.Text) || String.IsNullOrWhiteSpace(txt_sSize.Text))
                { msg.ShowError("قم بملئ البيانات أولا"); txt_sArt.Focus(); return; }

                bills = GetBill(txt_sArt.Text, cbox_sColor.Text, Convert.ToInt32(txt_sSize.Text));
            }
            else if(ch_ByProduct.Checked)
            {
                // VALIDATION
                if (String.IsNullOrWhiteSpace(txt_sCode.Text))
                { msg.ShowError("قم بملئ البيانات أولا"); txt_sCode.Focus(); return; }

                bills = GetBill(Convert.ToInt32(txt_sCode.Text), true);
            }
            else
            {
                return;
            }

            // Check Bills
            if(bills == null)
            {
                msg.ShowError("لا يوجد فواتير بهذه المواصفات");
                return;
            }

            // Show First Bill
            Add_Bill_To_DGV(bills[bill_i]);

            // Show Elements
            tpl_Elements.Show();
            tpl_BillDetails.Show();
            if(bills.Count > 1) btn_Next.Show();
            else { btn_Next.Hide(); btn_Next.Hide(); }

            // Focus
            if (ch_Code.Checked) txt_Art.Focus();
            else txt_Code.Focus();
        }

        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            btn_Cancel.PerformClick();
            // VALIDATION
            if (String.IsNullOrWhiteSpace(txt_BillID.Text)) { msg.ShowError("قم بإضافة رقم الفاتورة اولا"); txt_BillID.Focus(); return; }

            // ADD Bill Data
            Bill mybill = GetBill(Convert.ToInt32(txt_BillID.Text));
            // Check the bill
            if (mybill == null)
            {
                msg.ShowError("لا يوجد فواتير بهذا الرقم");
                return;
            }
            Add_Bill_To_DGV(mybill);

            // Show Elements
            tpl_Elements.Show();
            tpl_BillDetails.Show();

            // Hide Buttons
            btn_Next.Hide();
            btn_Back.Hide();

            // Focus
            if (ch_Code.Checked) txt_Art.Focus();
            else txt_Code.Focus();
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
                r.CreateCells(dgv_New, dgv_New.Rows.Count + 1, p.Code, desc, p.Price, 1, p.Dis, (p.Price - p.Dis), "مسح", 1);
                dgv_New.Rows.Add(r);
                Products.Add(p);
            }

            calc_Totals();

            if (ch_Code.Checked)
            { ch_Code.Checked = false; }
            else
            { txt_Code.Text = ""; txt_Code.Focus(); }
        }

        private void Btn_Back_Click(object sender, EventArgs e)
        {
            // Clear dgvs
            dgv_New.Rows.Clear();
            dgv_Old.Rows.Clear();

            // Add Back Bill to DGV
            bill_i--;
            Add_Bill_To_DGV(bills[bill_i]);

            // Show Hide Buttons
            if (bill_i == 0)
                btn_Back.Hide();
            if (bills.Count > bill_i + 1)
                btn_Next.Show();
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {
            // Clear dgvs
            dgv_New.Rows.Clear();
            dgv_Old.Rows.Clear();

            // Add Next Bill to DGV
            bill_i++;
            Add_Bill_To_DGV(bills[bill_i]);

            // Show Hide Buttons
            if (bill_i > 0)
                btn_Back.Show();
            if (bills.Count == bill_i + 1)
                btn_Next.Hide();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            dgv_New.Rows.Clear();
            dgv_Old.Rows.Clear();

            tpl_BillDetails.Hide();
            tpl_Elements.Hide();
            ch_ByProduct.Checked = false;

            txt_BillID.Focus();
        }

        private void Btn_All_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow r in dgv_Old.Rows)
            {
                if(Convert.ToInt32(r.Cells["o_status"].Value) == 1)
                {
                    Dgv_Old_CellContentClick(dgv_Old, new DataGridViewCellEventArgs(8, r.Index));
                }
            }
        }

        private async void Btn_Save_ClickAsync(object sender, EventArgs e)
        {
            // If Empty Dgvnew return
            if(dgv_New.Rows.Count < 1)
            {
                btn_Cancel.PerformClick();
                msg.ShowError("لا يوجد تعديلات", true);
                return;
            }

            Transaction trans = new Transaction();

            if(lbl_Date.Text == Session.inDay)
            {
                // Delete the mortaga3
                foreach(DataGridViewRow r in dgv_New.Rows)
                {
                    string code = r.Cells["code"].Value.ToString();
                    // Mortaga3
                    if (Convert.ToInt32(r.Cells["n_status"].Value) == -1)
                    {
                        // Get Old Qty
                        int qty = 0;
                        foreach(DataGridViewRow r2 in dgv_Old.Rows)
                        {
                            if(r2.Cells["o_code"].Value.ToString() == code)
                            {
                                qty = Convert.ToInt32(r2.Cells["o_qty"].Value);
                                break;
                            }
                        }

                        // Compare
                        qty += Convert.ToInt32(r.Cells["qty"].Value);
                        if (qty == 0) // All Qty Mortaga3
                        {
                            // Delete
                            trans.AddCmd("DELETE FROM Sells WHERE B_ID=@bid AND P_ID=@pid AND InDay=@inday",
                                new string[] { "@bid", lbl_Bill_ID.Text, "@pid", code, "@inday", lbl_Date.Text });
                        }
                        else
                        {
                            // Update
                            trans.AddCmd("UPDATE Sells SET QTY=" + qty.ToString() + " WHERE B_ID=@bid AND P_ID=@pid AND InDay=@inday",
                                new string[] { "@bid", lbl_Bill_ID.Text, "@pid", code, "@inday", lbl_Date.Text });
                        }
                    }
                    else
                    {
                        trans.AddCmd("INSERT INTO Sells(P_ID, B_ID, Price, QTY, Discount, Date_Time, InDay) VALUES(" +
                        r.Cells["code"].Value + ", " + lbl_Bill_ID.Text + ", " + r.Cells["price"].Value + ", " + r.Cells["qty"].Value
                        + ", " + r.Cells["discount"].Value + ", '" + DateTime.Now + "', '" + Session.inDay + "')");
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow r in dgv_New.Rows)
                {
                    trans.AddCmd("INSERT INTO Sells(P_ID, B_ID, Price, QTY, Discount, Date_Time, InDay) VALUES(" +
                    r.Cells["code"].Value + ", " + lbl_Bill_ID.Text + ", " + r.Cells["price"].Value + ", " + r.Cells["qty"].Value
                    + ", " + r.Cells["discount"].Value + ", '" + DateTime.Now + "', '" + Session.inDay + "')");
                }
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
                await Task.Run(() =>
                {
                    Forms.sells_Today.Btn_Refresh_Click(Forms.sells_Today.btn_Refresh, e);
                });

                msg.ShowError("تم تعديل البيعة بنجاح", true);
                if (Visual_Scripts.AskUser("هل تريد طباعة فاتورة جديدة؟", 'q'))
                {
                    GetBill(Convert.ToInt32(lbl_Bill_ID.Text)).Print_Bill(Convert.ToInt32(lbl_Old.Text));
                }
                btn_Cancel.PerformClick();
            }
        }

        private void Dgv_Old_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 8)
            {
                using (var r = dgv_Old.Rows[e.RowIndex])
                {
                    if (Convert.ToInt32(r.Cells["o_status"].Value) == -1)
                    {
                        // Cancel Edit

                        // Delete From New
                        foreach(DataGridViewRow row in dgv_New.Rows)
                        {
                            if(r.Cells[1].Value == row.Cells[1].Value)
                            {
                                dgv_New.Rows.Remove(row);
                                break;
                            }
                        }

                        // Change Color
                        r.DefaultCellStyle.BackColor = Color.FromArgb(242, 229, 213);

                        // Change btn text and status
                        r.Cells[8].Value = "إرجاع";
                        r.Cells[9].Value = 1;
                    }
                    else if (Convert.ToInt32(r.Cells["o_status"].Value) == 1)
                    {
                        // Edit

                        // Add -Qty to new dgv
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgv_New, dgv_New.Rows.Count + 1, r.Cells[1].Value, r.Cells[2].Value, r.Cells[3].Value,
                            -Convert.ToInt32(r.Cells[4].Value), r.Cells[5].Value, -Convert.ToInt32(r.Cells[6].Value), "", -1);
                        row.DefaultCellStyle.BackColor = Color.FromArgb(242, 196, 196);
                        dgv_New.Rows.Add(row);

                        // Change color
                        r.DefaultCellStyle.BackColor = Color.FromArgb(242, 196, 196);

                        // change btn text and status
                        r.Cells[8].Value = "إلغاء الإرجاع";
                        r.Cells[8].Style.BackColor = Color.FromArgb(89, 25, 2);
                        r.Cells[9].Value = -1;
                    }

                    if (ch_Code.Checked) txt_Art.Focus();
                    else txt_Code.Focus();

                    calc_Totals();
                }
            }
        }

        private void Dgv_New_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                using (var r = dgv_New.Rows[e.RowIndex])
                {
                    if(Convert.ToInt32(r.Cells["n_status"].Value) == 1)
                    {
                        int code = Convert.ToInt32(r.Cells[1].Value);
                        dgv_New.Rows.RemoveAt(e.RowIndex);
                        foreach (Product p in Products)
                        {
                            if (p.Code == code)
                            {
                                Products.Remove(p);
                                break;
                            }
                        }
                        calc_Totals();
                        txt_Code.Focus();
                    }
                    if (ch_Code.Checked) txt_Art.Focus();
                    else txt_Code.Focus();
                }
            }
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

        private void Dgv_New_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            tmp = Convert.ToInt32(dgv_New.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
        }

        private void Dgv_New_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(Convert.ToInt32(dgv_New.Rows[e.RowIndex].Cells["n_status"].Value) == 1)
            {
                try
                {
                    int tmp2 = Convert.ToInt32(dgv_New.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                    if (e.ColumnIndex == 4)
                    {
                        // Change Qty
                        if (!Change_Qty(Convert.ToInt32(dgv_New.Rows[e.RowIndex].Cells[1].Value), tmp2) ||
                            Convert.ToInt32(dgv_New.Rows[e.RowIndex].Cells[1].Value) <= 0)
                        {
                            dgv_New.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = tmp;
                            msg.ShowError("الكمية المطلوبة غير متوفرة في المخزن");
                        }
                    }
                    else
                    {
                        dgv_New.Rows[e.RowIndex].Cells[6].Value = Convert.ToInt32(dgv_New.Rows[e.RowIndex].Cells[3].Value)
                            * Convert.ToInt32(dgv_New.Rows[e.RowIndex].Cells[4].Value) - tmp2;

                        calc_Totals();
                    }
                }
                catch
                {
                    dgv_New.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = tmp;
                    msg.ShowError("يجب إضافة رقم صحيح");
                }
            }
            else
            {
                foreach(DataGridViewRow r in dgv_Old.Rows)
                {
                    if(r.Cells["o_code"].Value == dgv_New.Rows[e.RowIndex].Cells[1].Value)
                    {
                        if (!(Convert.ToInt32(r.Cells["o_qty"].Value) >= (Convert.ToInt32(dgv_New.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) * -1)
                        && Convert.ToInt32(dgv_New.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) < 0))
                        {
                            dgv_New.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = tmp;
                            msg.ShowError("عدد الإرجاع أكبر من العدد الفعلي أو أكبر من 0");
                            return;
                        }

                        int T = Convert.ToInt32(dgv_New.Rows[e.RowIndex].Cells[3].Value)
                            * Convert.ToInt32(dgv_New.Rows[e.RowIndex].Cells[4].Value)
                            + Convert.ToInt32(dgv_New.Rows[e.RowIndex].Cells[5].Value);
                        dgv_New.Rows[e.RowIndex].Cells[6].Value = T;
                    }
                }
                calc_Totals();
            }
        }
    }
}
