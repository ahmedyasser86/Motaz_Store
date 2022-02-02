using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

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
        #endregion

        #endregion
    }
}
