using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinJishu.Games.Empires.Models
{
    public class Planet : ObjectBase
    {
        public String dna { get; set; }
        public String name { get; set; }
        public PlanetType type { get; set; }
        public Double radius { get; set; }
        public Double orbital_period { get; set; }
        public Double orbital_speed { get; set; }
        public Double solar_radius { get; set; }
    }
}
