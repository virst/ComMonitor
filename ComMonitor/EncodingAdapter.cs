using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComMonitor
{
    class EncodingAdapter
    {
        public Encoding Encoding { get; private set; }
        private string s;

        public EncodingAdapter(Encoding e,string name)
        {
            Encoding = e;
            s = name;
        }

        public override string ToString()
        {
            return s;
        }
    }
}
