using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IssWebRazorApp.Models;
using IssWebRazorApp.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssWebRazorApp
{
    public class LoginModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly SessionService _sessionService;
        public LoginModel(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
            _userService = new UserService(userRepository);
            _sessionService = new SessionService();
        }
        public string Message { get; set; }
        [BindProperty]
        public string LoginId { get; set; }
        [BindProperty]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }
        public IActionResult OnGetAsync()
        {
            Message = "";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            var user = _userService.CheckPasswordAndGetLoginUser(LoginId,LoginPassword);
            if (user == null) 
            {
                Message = "ログインIDまたはパスワードが正しくありません。";
                return Page();
            }
            _sessionService.Set(HttpContext, "LoginUser", user.ToData());
            return RedirectToPage("./Index");
        }
    }
}