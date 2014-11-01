using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XinJishu.Games.Empires.Models;

namespace XinJishu.Games.Empires.Data
{
    public static class Converter
    {
        public static Galaxy ToGalaxy(DataRow dr)
        {
            Galaxy g = new Galaxy();

            g.galaxy_shape = (GalaxyShape)Enum.Parse(typeof(GalaxyShape), dr["Shape"].ToString(), true);
            g.id = Convert.ToInt32(dr["id"]);
            g.name = Convert.ToString(dr["name"]);
            g.active = Convert.ToBoolean(dr["active"]);
            g.create_on = Convert.ToDateTime(dr["create_on"]);

            return g;
        }
    }
}
