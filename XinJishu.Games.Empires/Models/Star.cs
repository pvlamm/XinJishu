using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinJishu.Games.Empires.Models
{
    public class Star : ObjectBase
    {
        public Decimal radius { get; set; }
        public Decimal speed { get; set; }
        public StarType type { get; set; }
        public Decimal diameter { get; set; }
        public Int32 size { get; set; }
        public Decimal mass { get; set; }
        public Decimal biozone_lower { get; set; }
        public Decimal biozone_upper { get; set; }

    }
}
