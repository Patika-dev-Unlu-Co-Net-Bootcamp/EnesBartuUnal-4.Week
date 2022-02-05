using ManagerApi4.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerApi4.Models
{
    [UserCheck(UserName = "Bartu", Password = "1234Ba.", RoleType = "Admin")]
    public class PlayerViewModel
    {
        public int Id { get; set; }
             
        public string Name { get; set; }
    
        public int Age { get; set; }

        public int TeamId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

    }
}
