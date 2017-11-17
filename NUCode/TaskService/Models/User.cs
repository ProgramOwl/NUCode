using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Model
{
    public class User
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string RoleId { get; set; }

        public User(string name, string id, string roleId)
        {
            Name = name;
            ID = id;
            RoleId = roleId;
        }
    }
}