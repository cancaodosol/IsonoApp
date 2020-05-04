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
    public class _LoginModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        public _LoginModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _userService = new UserService(userRepository);
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
            var user = _userService.CheckPasswordAndGetLoginUser(LoginId, LoginPassword);
            if (user == null)
            {
                Message = "ログインIDかパスワードが正しくありません。";
                return Page();
            }
            var sessionService = new SessionService();
            HttpContext.Session.Set("LoginUser", sessionService.ToBytes((Object)user));

            return RedirectToPage("./Index");
        }
    }
}