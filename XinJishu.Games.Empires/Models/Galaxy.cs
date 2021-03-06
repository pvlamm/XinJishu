﻿using System;
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

        public GalaxyShape galaxy_shape { get; set; }
        public Boolean active { get; set; }
        public virtual List<Star> stars { get; set; }
    }
}
