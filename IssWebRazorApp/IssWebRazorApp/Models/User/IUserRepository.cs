using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public interface IUserRepository
    {
        public User Find(string loginName);
    }
}
