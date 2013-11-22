using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XinJishu.Games.Empires.Models;

namespace XinJishu.Games.Empires.Generators
{
    public interface IGameObjectFactory
    {
        ObjectBase Generate<T>(Int32? seed);
    }
}
