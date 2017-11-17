using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Model
{
    public class UserList
    {
        public List<User> Users { get; set; }

        public UserList()
        {
            Users = new List<User>();
        }
    }
}