using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XinJishu.Games.Empires.Models;

namespace XinJishu.Games.Empires.Generators
{
    public class GameObjectFactory : IGameObjectFactory
    {
        public Models.ObjectBase Generate<T>(Int32? seed)
        {
            if (typeof(T) == typeof(Planet)) { return GeneratePlanet(seed); }
            else if (typeof(T) == typeof(SolarSystem)) { return GenerateSolarSystem(seed); }
            else if (typeof(T) == typeof(Galaxy)) { return GenerateGalaxy(seed); }

            return null;
        }

        private Planet GeneratePlanet(Int32? seed = null)
        {
            Planet planet = new Planet();
            if (seed.HasValue)
            {


            }
            else
            {

            }
            return planet;
        }

        private SolarSystem GenerateSolarSystem(Int32? seed = null)
        {
            SolarSystem solar_system = new SolarSystem();
            if (seed.HasValue) { }
            else { }

            return solar_system;
        }

        private Galaxy GenerateGalaxy(Int32? seed = null)
        {
            Galaxy galaxy = new Galaxy();
            if (seed.HasValue) { }
            else { }

            return galaxy;

        }
    }
}
