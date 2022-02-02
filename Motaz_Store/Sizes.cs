using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motaz_Store
{
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
}
