using IssWebRazorApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public interface IUserRepository
    {
        public UserData Find(string loginName);
        public UserData Find(int userId);
        public Task AddAsync(UserData data);
        public List<PositionData> GetPotision();
    }
}
