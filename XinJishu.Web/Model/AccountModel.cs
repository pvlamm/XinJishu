﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinJishu.Web.Model
{
    public class AccountModel
    {
        public AccountModel() { }

        public Int32 id { get; set; }
        public Guid publicId { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public IList<RoleModel> roles { get; set; }
        public IDictionary<String, Object> fields { get; set; }
    }
}
