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
    public partial class Store_View : Form
    {
        Msgs msg;
        List<string> All_Colors = new List<string>();
        List<string> All_Sorts;
        bool filter = false;
        string search = null;
        public Store_View()
        {
            InitializeComponent();

            // Messages Handler
            msg = new Msgs(lbl_Error);

            // Hide Filter
            pnl_Filter.Height = 5;
            tlp_Filter.Hide();

            // Load Data To DGV
            DGVs("SELECT pp.Des الوصف, pp.Art الأرتكل, p.Color اللون, SUM((p.Qty - ISNULL(s.QTY, 0) - ISNULL(w.Qty, 0) - ISNULL(o.Qty, 0)))" +
                " الكمية, AVG((pp.Price - pp.Descount)) 'سعر البيع', pp.Sup المورد FROM Products_Prices pp " +
                "LEFT JOIN Products p ON p.Art = pp.Art LEFT JOIN Online o ON o.P_ID = p.ID " +
                "LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM Sells GROUP BY P_ID) s ON s.P_ID = p.ID " +
                "LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM WithDraws GROUP BY P_ID) w ON w.P_ID = p.ID WHERE (p.Qty - ISNULL(s.QTY, 0) - ISNULL(w.Qty, 0) - ISNULL(o.Qty, 0)) != 0 " +
                "GROUP BY p.Color, pp.Art, pp.Des, pp.Sup ORDER BY pp.Art", dgv_Sells);

            // Load Data From DB
            All_Sorts = Pro_Des.GetAllDes();
            cbox_Type.DataSource = All_Sorts;
            All_Colors.Add("الجميع");
            All_Colors.AddRange(GetDataArray("SELECT * FROM Colors"));
            cbox_Color.DataSource = All_Colors;
            txt_PriceFrom.Text = GetDataInt("SELECT MIN((Price - Descount)) FROM Products_Prices").ToString();
            txt_PriceTo.Text = GetDataInt("SELECT MAX((Price - Descount)) FROM Products_Prices").ToString();

            #region Txts Events

            #region -- Text Changed
            // Size
            txt_Size.TextChanged += (S, E) =>
            {
                if(!String.IsNullOrWhiteSpace(txt_Size.Text))
                {
                    try { Convert.ToInt32(txt_Size.Text); }
                    catch { msg.ShowError("يجب كتابة ارقام صحيحة فقط"); txt_Size.Text = ""; txt_Size.Focus(); }
                }
            };

            // Price From
            txt_PriceFrom.TextChanged += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_PriceFrom.Text))
                {
                    try { Convert.ToInt32(txt_PriceFrom.Text); }
                    catch { msg.ShowError("يجب كتابة ارقام صحيحة فقط"); txt_PriceFrom.Text = ""; txt_PriceFrom.Focus(); }
                }
            };

            // Price To
            txt_PriceTo.TextChanged += (S, E) =>
            {
                if (!String.IsNullOrWhiteSpace(txt_PriceTo.Text))
                {
                    try { Convert.ToInt32(txt_PriceTo.Text); }
                    catch { msg.ShowError("يجب كتابة ارقام صحيحة فقط"); txt_PriceTo.Text = ""; txt_PriceTo.Focus(); }
                }
            };
            #endregion -- Text Changed

            #region -- Enter Click
            // Type
            cbox_Type.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter)
                    cbox_Color.Focus();
            };
            // Color
            cbox_Color.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter)
                    txt_Size.Focus();
            };
            // Size
            txt_Size.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter)
                    txt_PriceFrom.Focus();
            };
            // Price From
            txt_PriceFrom.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter)
                    txt_PriceTo.Focus();
            };
            // Price From
            txt_PriceTo.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter)
                    btn_Filter.PerformClick();
            };

            // Art - Search
            txt_Art.KeyDown += (S, E) =>
            {
                if (E.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(txt_Art.Text))
                    btn_Search.PerformClick();
            };
            #endregion -- Enter Click

            #endregion Txts Events
        }

        private void Btn_Filter_Click(object sender, EventArgs e)
        {
            if(filter)
            {
                pnl_Filter.Height = 5;
                tlp_Filter.Hide();
                filter = false;

                // Filter
                string q;
                string w = "";
                List<string> Params = new List<string>();

                // Art
                if(search != null)
                {
                    w += " p.Art=@Art AND ";
                    Params.Add("@Art");
                    Params.Add(txt_Art.Text);
                }
                
                // Type
                if(cbox_Type.Text != "الجميع")
                {
                    w += " pp.Art LIKE '" + Pro_Des.GetChars(cbox_Type.Text) + "%' AND ";
                }

                // Color
                if(cbox_Color.Text != "الجميع")
                {
                    w += " p.Color=@Color AND ";
                    Params.Add("@Color");
                    Params.Add(cbox_Color.Text);
                }

                // Size
                if(!String.IsNullOrWhiteSpace(txt_Size.Text))
                {
                    w += " p.Size=@Size AND ";
                    Params.Add("@Size");
                    Params.Add(txt_Size.Text);
                }

                // Price From
                if(!String.IsNullOrWhiteSpace(txt_PriceFrom.Text))
                {
                    w += " (pp.Price - pp.Descount) >= @pricef AND ";
                    Params.Add("@pricef");
                    Params.Add(txt_PriceFrom.Text);
                }
                // Price To
                if (!String.IsNullOrWhiteSpace(txt_PriceTo.Text))
                {
                    w += " (pp.Price - pp.Descount) <= @pricet AND ";
                    Params.Add("@pricet");
                    Params.Add(txt_PriceTo.Text);
                }

                if(ch_Empty.Checked)
                {
                    w += " (p.Qty - ISNULL(s.QTY, 0) - ISNULL(w.Qty, 0) - ISNULL(o.Qty, 0)) != 0 AND ";
                }
                if (ch_Sizes.Checked) btn_PrintBarcode.Show();
                else btn_PrintBarcode.Hide();

                if (w != "")
                {
                    w = "WHERE " + w.Substring(0, w.Length - 4);
                }

                if (ch_Sizes.Checked)
                {

                    q = "SELECT p.ID كود, pp.Des الوصف, pp.Art الأرتكل, p.Color اللون, p.Size المقاس, " +
                        "(p.Qty - ISNULL(s.QTY, 0) - ISNULL(w.Qty, 0) - ISNULL(o.Qty, 0)) الكمية, (pp.Price - pp.Descount) 'سعر البيع'," +
                        " pp.Sup المورد FROM Products_Prices pp LEFT JOIN Products p ON p.Art = pp.Art LEFT JOIN Online o ON o.P_ID = p.ID" +
                        " LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM Sells GROUP BY P_ID) s ON s.P_ID = p.ID " +
                        "LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM WithDraws GROUP BY P_ID) w ON w.P_ID = p.ID " + w +
                        " ORDER BY pp.Art, p.Color, p.Size";
                }
                else
                {
                    q = "SELECT pp.Des الوصف, pp.Art الأرتكل, p.Color اللون, SUM((p.Qty - ISNULL(s.QTY, 0) - ISNULL(w.Qty, 0) - " +
                        "ISNULL(o.Qty, 0))) الكمية, AVG((pp.Price - pp.Descount)) 'سعر البيع', pp.Sup المورد " +
                        "FROM Products_Prices pp LEFT JOIN Products p ON p.Art = pp.Art LEFT JOIN Online o ON o.P_ID = p.ID " +
                        "LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM Sells GROUP BY P_ID) s ON s.P_ID = p.ID " +
                        "LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM WithDraws GROUP BY P_ID) w ON w.P_ID = p.ID " + w +
                        " GROUP BY p.Color, pp.Art, pp.Des, pp.Sup ORDER BY pp.Art";
                }
                DGVs(q, dgv_Sells, Params.ToArray());
            }
            else
            {
                pnl_Filter.Height = 115;
                tlp_Filter.Show();
                filter = true;
            }
        }

        private void Btn_Refresh_Click(object sender, EventArgs e)
        {
            // Load Data To DGV
            DGVs("SELECT pp.Des الوصف, pp.Art الأرتكل, p.Color اللون, SUM((p.Qty - ISNULL(s.QTY, 0) - ISNULL(w.Qty, 0) - ISNULL(o.Qty, 0)))" +
                " الكمية, AVG((pp.Price - pp.Descount)) 'سعر البيع', pp.Sup المورد FROM Products_Prices pp " +
                "LEFT JOIN Products p ON p.Art = pp.Art LEFT JOIN Online o ON o.P_ID = p.ID " +
                "LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM Sells GROUP BY P_ID) s ON s.P_ID = p.ID " +
                "LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM WithDraws GROUP BY P_ID) w ON w.P_ID = p.ID " +
                "WHERE (p.Qty - ISNULL(s.QTY, 0) - ISNULL(w.Qty, 0) - ISNULL(o.Qty, 0)) != 0 " +
                "GROUP BY p.Color, pp.Art, pp.Des, pp.Sup ORDER BY pp.Art", dgv_Sells);

            // Load Data From DB
            All_Sorts = Pro_Des.GetAllDes();
            cbox_Type.DataSource = All_Sorts;
            All_Colors.Add("الجميع");
            All_Colors.AddRange(GetDataArray("SELECT * FROM Colors"));
            cbox_Color.DataSource = All_Colors;
            txt_PriceFrom.Text = GetDataInt("SELECT MIN((Price - Descount)) FROM Products_Prices").ToString();
            txt_PriceTo.Text = GetDataInt("SELECT MAX((Price - Descount)) FROM Products_Prices").ToString();

            // Chs
            ch_Empty.Checked = true;
            ch_Sizes.Checked = false;

            search = null;
            txt_PriceFrom.ReadOnly = false;
            txt_PriceTo.ReadOnly = false;
            btn_PrintBarcode.Hide();
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            // Validation
            if(String.IsNullOrWhiteSpace(txt_Art.Text)) { msg.ShowError("قم بكتابة الأرتكل أولا"); txt_Art.Focus(); return; }

            // Load Data To DGV
            search = "SELECT p.ID كود, pp.Des الوصف, pp.Art الأرتكل, p.Color اللون, p.Size المقاس, " +
                "(p.Qty - ISNULL(s.QTY, 0) - ISNULL(w.Qty, 0) - ISNULL(o.Qty, 0)) الكمية, (pp.Price - pp.Descount) 'سعر البيع'," +
                " pp.Sup المورد FROM Products_Prices pp LEFT JOIN Products p ON p.Art = pp.Art " +
                "LEFT JOIN Online o ON o.P_ID = p.ID LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM Sells GROUP BY P_ID) s ON s.P_ID = p.ID " +
                "LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM WithDraws GROUP BY P_ID) w ON w.P_ID = p.ID " +
                "WHERE p.Art = @Art ORDER BY pp.Art, p.Color, p.Size";
            DGVs(search, dgv_Sells, new string[] { "@Art", txt_Art.Text });

            // Load Type
            cbox_Type.DataSource = null;
            cbox_Type.Items.Clear();
            cbox_Type.Items.Add(Pro_Des.GetDes(txt_Art.Text));
            cbox_Type.SelectedIndex = 0;

            // Load colors from db
            cbox_Color.DataSource = null;
            cbox_Color.Items.Clear();
            List<string> colors = new List<string>();
            colors.Add("الجميع");
            colors.AddRange(GetDataArray("SELECT DISTINCT Color FROM Products WHERE Art=@Art", new string[] { "@Art", txt_Art.Text }));
            cbox_Color.DataSource = colors;

            // Price
            int price;
            try { price = GetDataInt("SELECT (Price - Descount) FROM Products_Prices WHERE Art=@Art", new string[] { "@Art", txt_Art.Text }); }
            catch { msg.ShowError("لا يوجد منتج بهذا الأرتكل"); price = 0; }
            txt_PriceFrom.Text = price.ToString();
            txt_PriceTo.Text = price.ToString();
            txt_PriceFrom.ReadOnly = true;
            txt_PriceTo.ReadOnly = true;

            ch_Sizes.Checked = true;
            ch_Empty.Checked = false;
            btn_PrintBarcode.Show();
        }

        private void Btn_PrintBarcode_Click(object sender, EventArgs e)
        {
            if (dgv_Sells.SelectedCells.Count == 0)
            { msg.ShowError("قم بتحديد أكواد للطباعة أولا"); return; }

            foreach(DataGridViewCell cell in dgv_Sells.SelectedCells)
            {
                if(cell.ColumnIndex == 0)
                {
                    using (var r = dgv_Sells.Rows[cell.RowIndex])
                    {
                        printBarcode(Convert.ToInt32(cell.Value), r.Cells["الأرتكل"].Value.ToString(), r.Cells["اللون"].Value.ToString(),
                            Convert.ToInt32(r.Cells["المقاس"].Value), Convert.ToInt32(r.Cells["سعر البيع"].Value));
                    }
                }
            }
        }

        private void Dgv_Sells_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1 && dgv_Sells.Columns.Count == 6)
            {
                txt_Art.Text = dgv_Sells.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                btn_Search.PerformClick();
            }
        }
    }
}
