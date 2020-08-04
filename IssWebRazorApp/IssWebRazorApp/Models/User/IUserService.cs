using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    interface IUserService
    {
        public User CheckPasswordAndGetLoginUser(string loginId, string password);
        public User Find(int userId);
        public Task AddAsync(User user);
        public List<Position> GetPosition();
        public SelectList GetPositionSelectList();
    }
}
