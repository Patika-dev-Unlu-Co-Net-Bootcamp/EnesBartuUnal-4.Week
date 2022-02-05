using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerApi4.Attributes
{
    
    public class UserCheckAttribute:Attribute
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        
        public string RoleType { get; set; }
    }
        
}
