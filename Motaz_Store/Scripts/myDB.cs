using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using IronBarCode;

namespace Motaz_Store
{
    public class myDB
    {
        #region Conniction

        private static SqlConnection conn = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\myDB.mdf;
            Integrated Security=True");

        public static void TestConn()
        {
            try
            {
                conn.Open();
                conn.Close();
            }
            catch
            {
                MessageBox.Show("فشل الإتصال بقاعدة البيانات", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void OpenConn()
        {
            try
            {
                conn.Open();
            }
            catch
            {
                MessageBox.Show("فشل الإتصال بقاعدة البيانات", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public static void CloseConn()
        {
            try
            {
                conn.Close();
                conn.Dispose();
            }
            catch
            {
                MessageBox.Show("فشل الإتصال بقاعدة البيانات", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        #endregion

        #region Quries

        #region Get Some Values From DataBase
        public static List<string> GetDataArray(string Query, string[] Params = null)
        {
            List<string> Data = new List<string>();

            SqlCommand cmd = new SqlCommand(Query, conn);

            if(Params != null)
            {
                for(int i = 0; i < Params.Length; i += 2)
                {
                    cmd.Parameters.AddWithValue(Params[i], Params[i + 1]);
                }
            }

            SqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                Data.Add(r[0].ToString());
            }

            r.Close();

            return Data;
        }

        public static List<string> GetDataArray(string Query, int Columns, string[] Params = null)
        {
            List<string> Data = new List<string>();

            SqlCommand cmd = new SqlCommand(Query, conn);

            if (Params != null)
            {
                for (int i = 0; i < Params.Length; i += 2)
                {
                    cmd.Parameters.AddWithValue(Params[i], Params[i + 1]);
                }
            }

            SqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                for(int i = 0; i < Columns; i++)
                {
                    Data.Add(r[i].ToString());
                }
            }

            r.Close();

            return Data;
        }

        public static int GetDataInt(string Query, string[] Params = null)
        {
            SqlCommand cmd = new SqlCommand(Query, conn);

            if (Params != null)
            {
                for (int i = 0; i < Params.Length; i += 2)
                {
                    cmd.Parameters.AddWithValue(Params[i], Params[i + 1]);
                }
            }

            return (Int32)(cmd.ExecuteScalar());
        }

        public static string GetDataString(string Query, string[] Params = null)
        {
            SqlCommand cmd = new SqlCommand(Query, conn);

            if (Params != null)
            {
                for (int i = 0; i < Params.Length; i += 2)
                {
                    cmd.Parameters.AddWithValue(Params[i], Params[i + 1]);
                }
            }

            return (string)(cmd.ExecuteScalar());
        }

        public static DataTable GetDataTable(string Query, string[] Params = null)
        {
            SqlCommand cmd = new SqlCommand(Query, conn);

            if (Params != null)
            {
                for (int i = 0; i < Params.Length; i += 2)
                {
                    cmd.Parameters.AddWithValue(Params[i], Params[i + 1]);
                }
            }

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            ad.Fill(dt);

            return dt;
        }

        #region Products
        public static Product GetProduct(int Code)
        {
            try
            {
                List<string> l = GetDataArray("SELECT p.ID ID, p.Art Art, p.Color Color, p.Size size, pp.Price Price, pp.Descount Dis," +
                    " pp.Des Des,  (p.Qty - ISNULL(s.QTY, 0) - ISNULL(w.Qty, 0) - ISNULL(o.Qty, 0)) Qty  " +
                    "FROM Products p LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM Sells GROUP BY P_ID) s ON p.ID = s.P_ID " +
                    "LEFT JOIN (SELECT SUM(Qty) Qty, P_ID FROM WithDraws GROUP BY P_ID) w ON p.ID = w.P_ID " +
                    "LEFT JOIN Products_Prices pp ON p.Art = pp.Art LEFT JOIN Online o ON p.ID = o.P_ID WHERE p.ID=@id", 8,
                new string[] { "@id", Code.ToString() });

                if (l.Count == 0) return null;
                else
                {
                    return new Product(Convert.ToInt32(l[0]), l[1], l[2], Convert.ToInt32(l[3]), Convert.ToInt32(l[4]), Convert.ToInt32(l[5]), l[6],
                        Convert.ToInt32(l[7]));
                }
            }
            catch
            {
                return null;
            }
        }

        public static Product GetProduct(string art, string color, int size)
        {
            try
            {
                int code = GetDataInt("SELECT ID FROM Products WHERE Art=@art AND Color=@color AND Size=@size", new string[] { "@art", art,
                "@color", color, "@size", size.ToString()});

                return GetProduct(code);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Bills
        public static Bill GetBill(int ID)
        {
            try
            {
                // Get Bill Details
                List<string> bill = GetDataArray("SELECT * FROM Bills WHERE ID=@ID", 4, new string[] { "@ID", ID.ToString() });

                // Get Bill's Products
                DataTable dt = GetDataTable("SELECT s.P_ID P_ID, s.Price Price, s.QTY Qty, s.Discount Dis, s.inDay s_inDay, " +
                    "p.Art Art, p.Color Color, p.Size Size, pp.Des Des FROM Sells s LEFT JOIN Products p ON s.P_ID = p.ID LEFT JOIN " +
                    "Products_Prices pp ON p.Art = pp.Art WHERE s.B_ID = @B_ID", new string[] { "@B_ID", ID.ToString() });

                List<Product> p = new List<Product>();

                // Assign new Bill with Details
                Bill mybill = new Bill(ID, bill[3], bill[2], bill[1], p);

                // Assign Products to the Bill
                foreach (DataRow r in dt.Rows)
                {
                    mybill.Products.Add(new Product(Convert.ToInt32(r["P_ID"]), r["Art"].ToString(), r["Color"].ToString(),
                        Convert.ToInt32(r["Size"]), Convert.ToInt32(r["Price"]), Convert.ToInt32(r["Dis"]), r["Des"].ToString(),
                        Convert.ToInt32(r["Qty"]), r["s_inDay"].ToString()));
                }

                mybill.Clean_Products();

                return mybill;
            }
            catch
            {
                return null;
            }
        }

        public static List<Bill> GetBill(int P_ID, bool FindTheBillByProductID)
        {
            if(FindTheBillByProductID)
            {
                try
                {
                    List<Bill> bills = new List<Bill>();

                    // Get Bills Contains The Product
                    List<string> bills_ID = GetDataArray("SELECT B_ID FROM Sells WHERE P_ID=@P_ID", new string[] { "@P_ID", P_ID.ToString() });

                    if (bills_ID.Count <= 0)
                        throw new Exception("لا يوجد فواتير بهذا الكود");

                    // foreach Bill Assign new Bill
                    foreach (string id in bills_ID)
                    {
                        bills.Add(GetBill(Convert.ToInt32(id)));
                    }

                    return bills;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static List<Bill> GetBill(string Art, string Color, int Size)
        {
            try
            {
                // Get Product_ID
                int id = GetDataInt("SELECT ID FROM Products WHERE Art=@Art AND Color=@Color AND Size=@Size", new string[] { "@Art", Art,
                "@Color", Color, "@Size", Size.ToString()});

                // Get Bills For that ID
                return GetBill(id, true);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #endregion

        #region Validations
        public static bool isAvailable(string Query, string[] Params = null)
        {
            SqlCommand cmd = new SqlCommand(Query, conn);

            if (Params != null)
            {
                for (int i = 0; i < Params.Length; i += 2)
                {
                    cmd.Parameters.AddWithValue(Params[i], Params[i + 1]);
                }
            }

            SqlDataReader r = cmd.ExecuteReader();
            bool Ava = false;
            while(r.Read())
            {
                Ava = true;
                break;
            }
            r.Close();

            return Ava;
        }
        #endregion

        #region Excute
        public static bool CmdExcute(string Query, string[] Params = null)
        {
            SqlCommand cmd = new SqlCommand(Query, conn);

            if (Params != null)
            {
                for (int i = 0; i < Params.Length; i += 2)
                {
                    cmd.Parameters.AddWithValue(Params[i], Params[i + 1]);
                }
            }

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static int CmdExcuteRows(string Query, string[] Params = null)
        {
            SqlCommand cmd = new SqlCommand(Query, conn);

            if (Params != null)
            {
                for (int i = 0; i < Params.Length; i += 2)
                {
                    cmd.Parameters.AddWithValue(Params[i], Params[i + 1]);
                }
            }

            try
            {
                
                return cmd.ExecuteNonQuery();
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region DGVs
        public static void DGVs(string Query, DataGridView dgv, string[] Params = null)
        {
            // Clear dgv
            dgv.Columns.Clear();

            SqlCommand cmd = new SqlCommand(Query, conn);

            if (Params != null)
            {
                for (int i = 0; i < Params.Length; i += 2)
                {
                    cmd.Parameters.AddWithValue(Params[i], Params[i + 1]);
                }
            }

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            ad.Fill(dt);

            dgv.DataSource = dt;
        }
        #endregion

        #region Transaction
        public class Transaction
        {
            private static SqlConnection conn2 = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\myDB.mdf;
            Integrated Security=True");

            List<SqlCommand> cmds = new List<SqlCommand>();

            SqlTransaction trans = null;

            public Transaction(bool readUnCommited = false)
            {
                conn2.Open();
                if (readUnCommited)
                {
                    trans = conn2.BeginTransaction(IsolationLevel.ReadUncommitted);
                }
                else
                {
                    trans = conn2.BeginTransaction();
                }
            }

            public void AddCmd(string Query)
            {
                cmds.Add(new SqlCommand(Query, conn2, trans));
            }

            public void AddCmd(string Query, string[] Params)
            {
                SqlCommand cmd = new SqlCommand(Query, conn2, trans);
                for (int i = 0; i < Params.Length; i += 2)
                {
                    cmd.Parameters.AddWithValue(Params[i], Params[i + 1]);
                }
                cmds.Add(cmd);
            }

            public string AddCmdExecuted(string Query)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(Query, conn2, trans);
                    cmd.ExecuteNonQuery();
                    return null;
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }

            public int GetDataInt(string Query)
            {
                SqlCommand cmd = new SqlCommand(Query, conn2, trans);

                return (Int32)(cmd.ExecuteScalar());
            }

            public string StartTrans()
            {
                try
                {
                    foreach (SqlCommand cmd in cmds)
                    {
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    conn2.Close();
                    return null;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    conn2.Close();
                    return e.Message;
                }
            }

            public void CloseTrans()
            {
                trans.Rollback();
                conn2.Close();
            }
        };
        #endregion

        #region Other Classes
        public class Product
        {
            public int Code;
            public string Art;
            public string Color;
            public int Size;
            public int Price;
            public int Dis;
            public string Des;
            public int Qty;
            public string inDay;

            public Product(int code, string art, string color, int size, int price, int dis, string des, int qty, string inday = null)
            {
                Code = code;
                Art = art;
                Color = color;
                Size = size;
                Price = price;
                Dis = dis;
                Des = des;
                Qty = qty;
                inDay = inday;
            }
        }

        public class Bill
        {
            public int ID;
            public string DateTime;
            public string Casher;
            public string Seller;
            public List<Product> Products;

            public Bill(int id, string date, string casher, string seller, List<Product> products)
            {
                ID = id;
                DateTime = date;
                Casher = casher;
                Seller = seller;
                Products = products;
            }

            public void Clean_Products()
            {
                int i = 1;
                foreach(Product p in Products)
                {
                    for(int j = i; j < Products.Count; j++)
                    {
                        if(Products[j].Code == p.Code)
                        {
                            p.Qty += Products[j].Qty;
                            Products.Remove(Products[j]);
                            j--;
                            i--;
                        }
                    }
                    i++;
                }
                foreach (Product p in Products)
                    if (p.Qty == 0) Products.Remove(p);
            }

            public void Print_Bill(int paid)
            {
                Clean_Products();
                Reports.DataSets.Receipt rd = new Reports.DataSets.Receipt();
                int i = 1;
                int qty = 0;
                int total = 0;
                int total_dis = 0;
                foreach(Product p in Products)
                {
                    rd.Products.AddProductsRow(i.ToString(), p.Des + " " + p.Art.Substring(2) + " " + p.Color + " " + p.Size, 
                        p.Price.ToString(), p.Qty.ToString(), (p.Price * p.Qty - p.Dis).ToString());

                    qty += p.Qty;
                    total += p.Price * p.Qty - p.Dis;
                    total_dis += p.Dis;
                }

                int baqy;

                if (paid > 0) baqy = paid - total;
                else { paid = total; baqy = 0; }

                var Barcode = BarcodeWriter.CreateBarcode(ID.ToString(), BarcodeWriterEncoding.Code128);

                rd.Details.AddDetailsRow(qty.ToString(), total_dis.ToString(), total.ToString(), paid.ToString(), baqy.ToString(),
                    Casher, Seller, Session.inDay, ID.ToString(), Barcode.ToJpegBinaryData());

                Reports.Reports.Receipt rr = new Reports.Reports.Receipt();
                rr.SetDataSource(rd);
                rr.PrintOptions.PrinterName = Forms.settings_Adv.Recipt_Printer;
                rr.PrintToPrinter(1, false, 0, 0);
            }
        }

        public class Sizes
        {
            public
                int Size,
                Qty = 0;

            public Sizes(int s, int q = 1)
            {
                Size = s;
                Qty = q;
            }
        }
        #endregion

        #region Other

        public static void printBarcode(List<Sizes> sizes, string art, string color, int price)
        {
            foreach(Sizes s in sizes)
            {
                for(int i = 0; i < s.Qty; i++)
                {
                    // Get product Id from db
                    int code = GetDataInt("SELECT ID FROM Products WHERE Art=@Art AND Color=@Color AND Size=@Size", new string[]
                        { "@Art", art, "@Color", color, "@Size", s.Size.ToString() });

                    // Print barcode for this size
                    printBarcode(code, art, color, s.Size, price);
                }
            }
        }

        public static void printBarcode(int code, string art, string color, int size, int price)
        {
            // Print BarCode
            Task.Run(() => {
                Reports.DataSets.BarCode ds = new Reports.DataSets.BarCode();
                var bar = BarcodeWriter.CreateBarcode(code.ToString(), BarcodeWriterEncoding.Code128);
                ds.DataTable1.AddDataTable1Row(bar.ToJpegBinaryData(), code.ToString(), art.Insert(2, " "), color, 
                    size.ToString(), price.ToString() + " LE");

                Reports.Reports.BarCode b = new Reports.Reports.BarCode();
                b.SetDataSource(ds);
                b.PrintOptions.PrinterName = Forms.settings_Adv.Barcode_Printer;
                b.PrintToPrinter(1, false, 0, 0);
            });
        }

        public static void Restore(string file_path)
        {
            var dbname = Environment.CurrentDirectory + "\\myDB.mdf";
            string cmmd = "USE MASTER RESTORE DATABASE [" + dbname + "] FROM DISK='" + file_path + "'";
            SqlCommand cmd = new SqlCommand(cmmd, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("فشل إستعادة النسخة الإحطياطية", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void BackUp(string path)
        {
            var dbname = Environment.CurrentDirectory + "\\myDB.mdf";
            string cmmd = @"BACKUP DATABASE [" + dbname + "] TO DISK = '" + path
                + "\\DBbackup - " + DateTime.Now.Ticks.ToString() + ".Bak'";
            SqlCommand cmd = new SqlCommand(cmmd, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("فشل حفظ نسخة إحطياطية", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion
    }
}
