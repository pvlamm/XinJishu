using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XinJishu.Games.Empires.Models;

namespace XinJishu.Games.Empires.Generators
{
    // TODO: Revamp the GameObject Factory, make some private objects public, and actually DO something
    public class GameObjectFactory : IGameObjectFactory
    {
        public ObjectBase Generate<T>(Int32? seed)
        {
            if (typeof(T) == typeof(Planet)) { return GeneratePlanet(seed); }
            else if (typeof(T) == typeof(Star)) { return GenerateSolarSystem(seed); }
            else if (typeof(T) == typeof(Galaxy)) { return GenerateGalaxy(seed); }

            return null;
        }

        private Planet GeneratePlanet(Int32? seed = null)
        {
            Planet planet = new Planet();
            if (seed.HasValue)
            {
                planet.type = (PlanetType)(seed.Value % Enum.GetNames(typeof(PlanetType)).Length);


            }
            else
            {

            }
            return planet;
        }

        private Star GenerateSolarSystem(Int32? seed = null)
        {
            Star solar_system = new Star();
            if (seed.HasValue) { 
            }
            else { 
            
            }

            Int32 Roll18 = Dice.RollDice(18);

            if (Roll18 > 14) { solar_system.type = StarType.Blue; }
            else if (Roll18 > 12) { solar_system.type = StarType.BlueWhite; }
            else if (Roll18 > 9) { solar_system.type = StarType.Orange; }
            else if (Roll18 > 7) { solar_system.type = StarType.Red; }
            else if (Roll18 > 5) { solar_system.type = StarType.White; }
            else if (Roll18 > 3) { solar_system.type = StarType.Yellow; }
            else if (Roll18 > 2) { solar_system.type = StarType.YellowWhite; }
            else if (Roll18 >= 1) { solar_system.type = StarType.BlueWhite; }

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
