using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Motaz_Store
{
    public partial class Store : Form
    {
        Visual_Scripts scripts;
        public Store()
        {
            InitializeComponent();

            scripts = new Visual_Scripts(pnl_Top, Color.FromArgb(242, 242, 242), Color.FromArgb(79, 108, 115));
            scripts.OnBtnHover();
        }
    }
}
