using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Motaz_Store
{
    public class Msgs
    {
        Timer errorShow = new Timer();

        Label Lbl;

        Color EColor = Color.FromArgb(242, 54, 12);
        Color DColor = Color.FromArgb(45, 64, 59);

        public Msgs(Label lbl)
        {
            // Error Msg Control
            Lbl = lbl;

            errorShow.Interval = 3000;
            errorShow.Tick += (s, e) =>
            {
                lbl.Hide();
                errorShow.Stop();
            };
        }

        public void ShowError(string err, bool done = false)
        {
            errorShow.Stop();
            if (done)
            {
                Lbl.Text = err;
                Lbl.Show();
                Lbl.ForeColor = DColor;
                errorShow.Start();
                System.Media.SystemSounds.Beep.Play();
            }
            else
            {
                Lbl.Text = err;
                Lbl.Show();
                Lbl.ForeColor = EColor;
                errorShow.Start();
                System.Media.SystemSounds.Beep.Play();
            }
        }
    }
}
