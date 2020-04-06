using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public User(int id ,string name) {
            UserId = id;
            UserName = name;
        }
    }
}
