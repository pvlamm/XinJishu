using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinJishu.Data.Interfaces
{
    public interface ISettings
    {
        T GetValue<T>(String Key);
        Int32 SetValue<T>(String Key, Object Value);
        Boolean KeyExists(String Key);
    }
}
