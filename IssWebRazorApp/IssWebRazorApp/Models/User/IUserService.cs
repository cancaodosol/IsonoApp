using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    interface IUserService
    {
        public User CheckPasswordAndGetLoginUser(string loginId, string password);
    }
}
