using Adapter.Helpers;
using Adapter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Repositories
{
    public class UsersRepository
    {
        public List<User> Users { get; set; }
        public UsersRepository()
        {
            Users = new List<User>();
        }
    }
}
