using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace DM.WPF_Tree
{
    public class IndexVertex
    {
        public PocVertex vertex;
        public int index;
        public Tuple<decimal, decimal> d1;

        public IndexVertex() { }

        public IndexVertex(string id, int index, Tuple<decimal, decimal> d1, Brush Color)
        {
            this.index = index;
            this.vertex = new PocVertex(id, true, Color);
            this.d1 = d1;
        }
    }
}
