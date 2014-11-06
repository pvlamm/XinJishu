using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace XinJishu
{
    /// <summary>
    /// RequestBlocker is designed to be an In Memory Dictionary 
    /// for checking to see if a third party is making repeated
    /// requests and then denying repeats if they come too rapidly.
    /// </summary>
    public class RequestBlocker
    {
        private static volatile Lazy<RequestBlocker> _instance =
            new Lazy<RequestBlocker>(() => new RequestBlocker());

        private static Object syncRoot = new object();
        private MemoryCache _cache { get; set; }
        private RequestBlocker() { _cache = MemoryCache.Default; }

        public static RequestBlocker Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public Boolean RequestExists(Object requestId)
        {
            return _cache.Contains(requestId.ToString());
        }

        public Boolean AddRequest(Object requestId, Object value = null, Int32 ExpirationInMinutes = 60)
        {
            if (value == null)
                value = 0;

            if (!RequestExists(requestId))
                return _cache.Add(requestId.ToString(), value, DateTimeOffset.Now.AddMinutes(ExpirationInMinutes));
            else
                return false;
        }

        public Boolean RemoveRequest(Object requestId)
        {
            _cache.Remove(requestId.ToString());
            return !RequestExists(requestId.ToString());
        }

    }
}
