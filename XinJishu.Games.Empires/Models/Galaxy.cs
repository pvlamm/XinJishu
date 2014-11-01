using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinJishu.Games.Empires.Models
{
    public class Galaxy : ObjectBase
    {
        public Galaxy()
        {
            stars = new List<Star>();
        }

        public Int32 id { get; set; }
        public Guid public_id { get; set; }
        public String name { get; set; }
        public GalaxyShape galaxy_shape { get; set; }
        public Boolean active { get; set; }
        public DateTime create_on { get; set; }
        public virtual List<Star> stars { get; set; }
    }
}
