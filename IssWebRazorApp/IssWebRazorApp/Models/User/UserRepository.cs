using IssWebRazorApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;
        public UserRepository(IssWebRazorApp.Data.IssWebRazorAppContext context) 
        {
            _context = context;            
        }
        public User Find(string loginId) 
        {
            if (String.IsNullOrEmpty(loginId)) return null;
            var data = _context.UserData.Include(u => u.PositionData).FirstOrDefault(m => m.LoginId.Equals(loginId));
            return data.ToModel();
        }
        public UserData Find(int userId)
        {
            var data = _context.UserData.Include(u => u.PositionData).FirstOrDefault(m => m.UserId.Equals(userId));
            return data;
        }
        public List<UserData> FindAll()
        {
            return _context.UserData.Include(u => u.PositionData).ToList<UserData>();            
        }
    }
}
