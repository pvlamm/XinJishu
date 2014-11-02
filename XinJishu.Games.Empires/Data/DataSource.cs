using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public IEnumerable<Galaxy> Galaxies_SelectAll()
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

        public Galaxy Galaxy_GetByPublicId(Guid public_id)
        {
            using (var cmd = this.GetCommand())
            {

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[dbo].[Galaxy_ByPublicId]";
                cmd.Parameters.AddWithValue("@public_id", public_id);

                DataTable dt = ExecuteDataTable(cmd);

                if (dt.Rows.Count > 0)
                    return Converter.ToGalaxy(dt.Rows[0]);

                return null;

            }
        }

        public Galaxy Galaxy_Create(String name, GalaxyShape shape, Boolean active)
        {
            using (var cmd = this.GetCommand())
            {
                Int32 id = 0;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[dbo].[Galaxy_ByPublicId]";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@shape", shape.ToString());
                cmd.Parameters.AddWithValue("@active", active);
                SqlParameter param = new SqlParameter("@id", id);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();

                id = Convert.ToInt32( param.Value );

                return new Galaxy();

            }
        }


    }
}
