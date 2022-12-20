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
    public partial class Store_Other : Form
    {
        Msgs msg;
        int qty = 1;
        bool showdeleted = false;
        public Store_Other()
        {
            InitializeComponent();

            // Messages Handler
            msg = new Msgs(lbl_Error);

            // Load Colors
            cbox_sColor.DataSource = GetDataArray("SELECT * FROM Colors");

            LoadDataFromDB();

            // Calc Total if Manager
            if (Session.isManager)
            {
                pnl_Total.Show();
                int t = 0;
                foreach (DataGridViewRow r in dgv_Withdraws.Rows)
                {
                    t += Convert.ToInt32(r.Cells["السعر"].Value);
                }
                txt_Total.Text = t.ToString();
            }

            #region Controls' Events
            // txt_sCode
            // ----
            // - EnterClick
            txt_sCode.KeyDown += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_sCode.Text) && E.KeyCode == Keys.Enter)
                {
                    btn_Trans.PerformClick();
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
                    btn_Trans.PerformClick();
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
            #endregion
        }

        private void LoadDataFromDB(string where = "AND w.Status = 1 ")
        {
            DGVs("SELECT w.P_ID كود, AVG(w.Price) السعر, p.Art الأرتكل, p.Color اللون, p.Size المقاس, " +
                "IIF(SUM(w.Qty) = -1, N'داخل', N'خارج' ) الحالة, w.InDay اليوم " +
                "FROM WithDraws w LEFT JOIN Products p ON p.ID = w.P_ID WHERE w.Branch = 'Store' " + where +
                "GROUP BY w.P_ID, p.Art, p.Color, p.Size, w.InDay  HAVING SUM(w.Qty) != 0 ", dgv_Withdraws);
        }

        private void To_From_Click(object sender, EventArgs e)
        {
            if(rb_To.Checked)
            {
                btn_Trans.Text = "إرسال المنتج إلى الفرع الأخر";
                qty = 1;
            }
            else
            {
                btn_Trans.Text = "إستلام المنتج من الفرع الأخر";
                qty = -1;
            }

            if (ch_ByArt.Checked) txt_sArt.Focus();
            else txt_sCode.Focus();
        }

        private void Ch_ByArt_CheckedChanged(object sender, EventArgs e)
        {
            if(ch_ByArt.Checked)
            { pnl_sArt.Show(); pnl_sCode.Hide(); txt_sArt.Focus(); }
            else
            { pnl_sArt.Hide(); pnl_sCode.Show(); txt_sCode.Focus(); }
        }

        private void Btn_Trans_Click(object sender, EventArgs e)
        {
            // Validation
            if(ch_ByArt.Checked)
                if(String.IsNullOrWhiteSpace(txt_sArt.Text) || String.IsNullOrWhiteSpace(txt_sSize.Text))
                { msg.ShowError("قم بملئ الحقول أولا"); txt_sArt.Focus(); return; }
            else if(String.IsNullOrWhiteSpace(txt_sCode.Text))
                { msg.ShowError("قم بملئ الحقول أولا"); txt_sCode.Focus(); return; }

            // Get Code
            int code;
            if (ch_ByArt.Checked)
                code = GetDataInt("SELECT ID FROM Products WHERE Art=@Art AND Color=@Color AND Size=@Size", new string[]
                    { "@Art", txt_sArt.Text, "@Color", cbox_sColor.Text, "@Size", txt_sSize.Text });
            else code = Convert.ToInt32(txt_sCode.Text);

            // Check Avalability
            if (GetDataInt("SELECT ISNULL(SUM((ISNULL(p.Qty, 0) - ISNULL(s.QTY, 0) - ISNULL(w.Qty, 0) - ISNULL(o.Qty, 0))), 0) " +
                "FROM Products p LEFT JOIN Online o ON o.P_ID = p.ID " +
                "LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM Sells GROUP BY P_ID) s ON s.P_ID = p.ID " +
                "LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM WithDraws GROUP BY P_ID) w ON w.P_ID = p.ID WHERE p.ID = @ID", 
                new string[] { "@ID", code.ToString() }) <= 0 && rb_To.Checked)
            { msg.ShowError("المنتج المطلوب غير متوفر"); if (ch_ByArt.Checked) txt_sArt.Focus(); else txt_sCode.Focus(); return; }

            // Get Price
            int price = GetDataInt("SELECT (pp.Price - pp.Descount) FROM Products_Prices pp INNER JOIN Products p ON p.Art = pp.Art WHERE p.ID = @ID"
                , new string[] { "@ID", code.ToString() });

            // Insert
            if(CmdExcute("INSERT INTO WithDraws(P_ID, QTY, InDay, Date_Time, Price) VALUES(" + code + ", " + qty + ",'" + Session.inDay
                + "', '" + DateTime.Now + "', " + price + ")"))
            {
                msg.ShowError("تم تحويل المنتج بنجاح", true);

                btn_Refresh.PerformClick();

                // Calc Total if Manager
                if (Session.isManager)
                {
                    pnl_Total.Show();
                    int t = 0;
                    foreach (DataGridViewRow r in dgv_Withdraws.Rows)
                    {
                        t += Convert.ToInt32(r.Cells["السعر"].Value);
                    }
                    txt_Total.Text = t.ToString();
                }
            }
        }

        private void Btn_Refresh_Click(object sender, EventArgs e)
        {
            LoadDataFromDB();
            showdeleted = false;
        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            if (showdeleted) return;
            // Ask User
            if(!Visual_Scripts.AskUser("هل انت متأكد من أنك تريد أرشفة التحولات؟\nهذه العملية لا يمكن التراجع عنها", 'q') || 
                !Session.isManager)
            {
                msg.ShowError("لا تمتلك صلاحيات القيام بذلك");
                return;
            }

            if(CmdExcuteRows("UPDATE WithDraws SET Status = 0 WHERE Status = 1") > 0)
            {
                // DONE
                msg.ShowError("تم الأرشفة بنجاح", true);
                btn_Refresh.PerformClick();
            }
            else
            {
                // FAILED
                msg.ShowError("حدث خطأ ما");
            }
        }

        private void Btn_ShowDeleted_Click(object sender, EventArgs e)
        {
            LoadDataFromDB("AND w.Status = 0");
            showdeleted = true;
        }
    }
}
