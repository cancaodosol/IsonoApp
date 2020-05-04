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
            var user = _userRepository.Find(loginId);
            return user.LoginPassword.Equals(password) ? user : null;
        }
    }
}
