using IssWebRazorApp.Models.Exceptions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public User CheckPasswordAndGetLoginUser(string loginId,string password) 
        {
            var data = _userRepository.Find(loginId);
            if (data == null) return null;
            var user = data.ToModel();
            return user.LoginPassword.Equals(password) ? user : null;
        }

        public User Find(int userId) 
        {
            var data = _userRepository.Find(userId);
            var user = data.ToModel();
            return user;
        }
        public async Task AddAsync(User user) 
        {
            if (this.Exist(user.LoginId)) 
            {
                throw new ISSServiceException("ログインID["+ user.LoginId +"]は既に使用されています。");
            }
            var data = user.ToData();
            data.PositionData = null;
            await _userRepository.AddAsync(data);
        }
        public List<Position> GetPosition() 
        {
            var positions = _userRepository.GetPotision().Select(_ => _.ToModel()).ToList();
            return positions;
        }

        public SelectList GetPositionSelectList() 
        {
            var positions = this.GetPosition().OrderBy(_ => _.PositionId);
            var list = new SelectList(positions, "PositionId", "FullName");
            return list;
        }

        private bool Exist(string loginId)
        {
            var user = _userRepository.Find(loginId);
            return user != null;
        }
    }
}
