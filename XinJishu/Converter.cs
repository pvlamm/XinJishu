using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinJishu
{
    public static class Converter
    {
        public static Int32 ToInt32(Object o)
        {
            var t = o.ToString();
            Int32 r = default(Int32);

            Int32.TryParse(t, out r);
            
            return r;
        }

        public static Guid ToGuid(Object o)
        {
            var s = o.ToString();
            Guid g = Guid.Empty;

            Guid.TryParse(s, out g);

            return g;
        }
    }
}
