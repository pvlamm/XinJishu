﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XinJishu.Games.Empires.Models;

namespace XinJishu.Games.Empires.Data
{
    public class DataSource : XinJishu.Data.SQLServer.ConnectionManager
    {
        private Int32 galaxy_id { get; set; }


        public DataSource(String connection_string, Int32 galaxy_id)
            : base(connection_string)
        {
            this.galaxy_id = galaxy_id;
        }

        public IEnumerable<Galaxy> GetGalaxies()
        {
            using (var cmd = this.GetCommand())
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[dbo].[Galaxy_SelectAll]";

                DataTable dt = ExecuteDataTable(cmd);
                
                List<Galaxy> galaxies = new List<Galaxy>();

                foreach (DataRow dr in dt.Rows)
                    galaxies.Add(Converter.ToGalaxy(dr));

                return galaxies;
            }
        }
    }
}