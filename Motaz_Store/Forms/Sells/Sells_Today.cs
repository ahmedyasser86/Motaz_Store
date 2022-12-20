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
    public partial class Sells_Today : Form
    {
        Msgs msg;
        public Sells_Today()
        {
            InitializeComponent();

            // Messages Handler
            msg = new Msgs(lbl_Error);

            // Premission
            if (!Session.isManager)
                pnl_Custom.Hide();

            #region Txts' Events
            // txt_Ex
            txt_Ex.KeyDown += (S, E) =>
            {
                if(!String.IsNullOrWhiteSpace(txt_Ex.Text) && E.KeyCode == Keys.Enter)
                {
                    txt_Des.Focus();
                    E.SuppressKeyPress = true;
                }
            };
            // txt_Des
            txt_Des.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_Des.Text) && E.KeyCode == Keys.Enter)
                {
                    Btn_Add.PerformClick();
                    E.SuppressKeyPress = true;
                }
            };
            // dtp Date
            dtp_Date.KeyDown += (S, E) =>
            {
                if(E.KeyCode == Keys.Enter)
                {
                    E.SuppressKeyPress = true;
                    btn_CustomDay.PerformClick();
                }
            };
            #endregion

            Btn_Refresh_Click(btn_Refresh, new EventArgs());
        }

        public void LoadDay(string inDay)
        {
            // Check If Day Avalable
            try
            {
                // Get Safe
                lbl_Safe.Text = GetDataInt("SELECT Amount FROM Safe WHERE InDay=@InDay", new string[] { "@InDay", inDay }).ToString();
            }
            catch
            {
                // Not Found
                inDay = Session.inDay;
                msg.ShowError("لم يتم العصور على هذا اليوم");

                // Get Safe
                lbl_Safe.Text = GetDataInt("SELECT Amount FROM Safe WHERE InDay=@InDay", new string[] { "@InDay", inDay }).ToString();
            }

            // Hide Add Ex and Make Times Read Only
            if (inDay != Session.inDay)
            {
                tpl_Ex.Hide();
            }
            else
            {
                tpl_Ex.Show();
                dgv_Times.Columns[1].ReadOnly = false;
            }

            // Clear Cross Threads
            Control.CheckForIllegalCrossThreadCalls = false;

            #region Dgv_Sells
            // Clear Dgv
            dgv_Sells.Rows.Clear();
            // Get Data From DB
            DataTable dt = GetDataTable("SELECT s.B_ID, s.Total, pp.Des, pp.Art FROM Sells s LEFT JOIN Products p ON s.P_ID = p.ID" +
                " LEFT JOIN Products_Prices pp ON p.Art = pp.Art WHERE s.InDay = @InDay", new string[] { "@InDay", inDay });
            // Group By Bill ID
            string b_id = null;
            int Total = 0;
            string Des = "";
            int counter = 1;
            foreach (DataRow r in dt.Rows)
            {
                if (b_id == null || b_id == r[0].ToString())
                {
                    b_id = r[0].ToString();
                    Total += Convert.ToInt32(r[1]);
                    Des += r[2] + " " + r[3].ToString().Substring(2) + "، ";
                }
                else
                {
                    DataGridViewRow rg = new DataGridViewRow();
                    rg.CreateCells(dgv_Sells, counter, Total, Des.Substring(0, Des.Length - 2), b_id);
                    dgv_Sells.Rows.Add(rg);

                    b_id = r[0].ToString();
                    Total = Convert.ToInt32(r[1]);
                    Des = r[2] + " " + r[3].ToString().Substring(2) + "، ";
                    counter++;
                }
            }
            if (dt.Rows.Count > 0)
            {
                // Add The Last Row
                DataGridViewRow rg2 = new DataGridViewRow();
                rg2.CreateCells(dgv_Sells, counter, Total, Des.Substring(0, Des.Length - 2), b_id);
                dgv_Sells.Rows.Add(rg2);
            }

            #endregion Dgv_Sells
            // ------------------
            #region Dgv_Exs
            // Clear rows
            dgv_Exs.Rows.Clear();
            // Get Data From DB
            DataTable dt_Ex = GetDataTable("SELECT * FROM Exs WHERE InDay=@InDay", new string[] { "@InDay", inDay });
            // Add Data To DGV
            foreach (DataRow r in dt_Ex.Rows)
            {
                DataGridViewRow rg = new DataGridViewRow();
                rg.CreateCells(dgv_Exs, r["Amount"], r["Details"]);
                dgv_Exs.Rows.Add(rg);
            }
            #endregion Dgv_Exs
            // ------------------
            #region Dgv_Times
            // Clear rows
            dgv_Times.Rows.Clear();
            // Get Data From DB
            DataTable dt_Ti = GetDataTable("SELECT * FROM Times WHERE InDay=@InDay", new string[] { "@InDay", inDay });
            // Add Data To DGV
            foreach (DataRow r in dt_Ti.Rows)
            {
                DataGridViewRow rg = new DataGridViewRow();
                rg.CreateCells(dgv_Times, r["Name"], r["Time"]);
                dgv_Times.Rows.Add(rg);
            }
            #endregion Dgv_Times
            // ------------------
            #region Calc_Totals
            Calc_Totals();
            #endregion Calc_Totals
        }

        private void Calc_Totals()
        {
            // Erad
            int Erad = 0;
            foreach (DataGridViewRow r in dgv_Sells.Rows)
            {
                Erad += Convert.ToInt32(r.Cells[1].Value);
            }
            lbl_Erad.Text = Erad.ToString();

            // Ex
            int Ex = 0;
            foreach (DataGridViewRow r in dgv_Exs.Rows)
            {
                Ex += Convert.ToInt32(r.Cells[0].Value);
            }
            lbl_Exs.Text = Ex.ToString();

            // Total
            int total = Convert.ToInt32(lbl_Safe.Text) + Erad - Ex;
            lbl_Total.Text = total.ToString();
        }

        public void Btn_Refresh_Click(object sender, EventArgs e)
        {
            LoadDay(Session.inDay);
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            // Validation
            if(String.IsNullOrWhiteSpace(txt_Ex.Text) || String.IsNullOrWhiteSpace(txt_Des.Text))
            {
                msg.ShowError("قم بملئ الحقول اولا");
                txt_Ex.Focus();
                return;
            }

            int ex;
            int i = -1;

            try { ex = Convert.ToInt32(txt_Ex.Text); }
            catch { msg.ShowError("يجب ان يكون رقم صحيح داخل القيمة"); txt_Ex.Text = ""; txt_Ex.Focus(); return; }

            // Check Deplicate
            foreach(DataGridViewRow r in dgv_Exs.Rows)
            {
                if(r.Cells[1].Value.ToString() == txt_Des.Text)
                {
                    ex += Convert.ToInt32(r.Cells[0].Value);
                    i = dgv_Exs.Rows.IndexOf(r);
                    break;
                }
            }

            if(i != -1)
            {
                if(ex == 0)
                {
                    // DELETE
                    if (CmdExcuteRows("DELETE FROM Exs WHERE Details=@Details AND InDay=@InDay", new string[] {
                    "@Details", txt_Des.Text, "@InDay", Session.inDay}) == 1)
                    {
                        // Done
                        msg.ShowError("تم حذف المصروف بنجاح", true);
                        txt_Ex.Text = "";
                        txt_Des.Text = "";
                        txt_Ex.Focus();
                        dgv_Exs.Rows.RemoveAt(i);
                    }
                }
                else
                {
                    // Update
                    if (CmdExcuteRows("UPDATE Exs SET Amount=" + ex + " WHERE Details=@Details AND InDay=@InDay", new string[] {
                    "@Details", txt_Des.Text, "@InDay", Session.inDay}) == 1)
                    {
                        // Done
                        msg.ShowError("تم تعديل المصروف", true);
                        txt_Ex.Text = "";
                        txt_Des.Text = "";
                        txt_Ex.Focus();
                        dgv_Exs.Rows[i].Cells[0].Value = ex;
                    }
                    else
                    {
                        msg.ShowError("خدث خطأ ما حاول مرة أخرى");
                        txt_Ex.Focus();
                    }
                }
            }
            else
            {
                if(ex == 0)
                {
                    msg.ShowError("لا يمكن ان يدخل رقم 0");
                    txt_Ex.Text = "";
                    txt_Ex.Focus();
                    return;
                }

                // Insert
                if(CmdExcute("INSERT INTO Exs(Amount, Details, InDay) VALUES(" + ex + ", N'" + txt_Des.Text + "', '" + Session.inDay + "')"))
                {
                    // Done
                    msg.ShowError("تم إضافة المصروف", true);
                    DataGridViewRow r = new DataGridViewRow();
                    r.CreateCells(dgv_Exs, txt_Ex.Text, txt_Des.Text);
                    dgv_Exs.Rows.Add(r);
                    txt_Ex.Text = "";
                    txt_Des.Text = "";
                    txt_Ex.Focus();
                }
                else
                {
                    // failed
                    msg.ShowError("خدث خطأ ما حاول مرة أخرى");
                    txt_Ex.Focus();
                }
            }

            // Calc Total
            Calc_Totals();
        }

        private void Btn_CustomDay_Click(object sender, EventArgs e)
        {
            string date = dtp_Date.Value.ToString("yy.MM.dd_ddd");
            LoadDay(date);
            tlp_Details.Show();
        }

        private void Dgv_Sells_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                Forms.sells_Del.txt_BillID.Text = dgv_Sells.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Forms.sells.btn_Del.PerformClick();
                Forms.sells_Del.btn_Edit.PerformClick();
            }
        }

        private void Dgv_Times_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // INSERT Into DB
            string Name = dgv_Times.Rows[e.RowIndex].Cells[0].Value.ToString();
            string Time = dgv_Times.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (CmdExcuteRows("UPDATE Times SET Time=N'" + Time + "' WHERE Name=@Name AND InDay=@InDay", new string[] { "@Name", Name,
            "@InDay", Session.inDay}) != 1)
            {
                // Error
                msg.ShowError("حدث خطأ في التسجيل داخل قاعدة البيانات");
            }
        }

        private void Btn_EndDay_Click(object sender, EventArgs e)
        {
            if(Visual_Scripts.AskUser("هل انت متأكد من انك تريد تقفيل اليوم؟", 'q'))
            {
                btn_Refresh.PerformClick();
                Forms.endDay.lbl_Erad.Text = lbl_Erad.Text;
                Forms.endDay.lbl_Safe.Text = lbl_Safe.Text;
                Forms.endDay.lbl_Ex.Text = lbl_Exs.Text;
                Forms.endDay.lbl_Total.Text = lbl_Total.Text;
                Forms.endDay.lbl_ToHome.Text = lbl_Total.Text;
                Forms.sells.OpenEndDay();
            }
        }

        private void Btn_Erad_Click(object sender, EventArgs e)
        {
            if(tlp_Details.Visible)
            {
                // Hide
                tlp_Details.Hide();
                btn_Erad.Text = "عرض الإيراد";
            }
            else
            {
                tlp_Details.Show();
                btn_Erad.Text = "إخفاء الإيراد";
            }
        }
    }
}
