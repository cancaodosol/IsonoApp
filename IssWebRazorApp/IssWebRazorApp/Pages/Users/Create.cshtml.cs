using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IssWebRazorApp.Data;
using IssWebRazorApp.Models;
using IssWebRazorApp.Models.Exceptions;

namespace IssWebRazorApp.Users
{
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;
        public SelectList PositionList { get; set; }
        public SelectList UserTypeList { get; set; }

        public CreateModel(IUserRepository userRepository)
        {
            _userService = new UserService(userRepository);
            PositionList = _userService.GetPositionSelectList();
            var items = new Dictionary<string, int> { { "選手", 0 }, { "スタッフ", 1 } };
            UserTypeList = new SelectList(items, "Value", "Key");
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserPostRequestModel UserData { get; set; }
        public string Message { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!UserData.AuthenticationCode.Equals("issshiga")) 
            {
                Message = "識別コードが違います。";
                return Page();
            }

            try
            {
                var user = new User(UserData);
                await _userService.AddAsync(user);
            }
            catch (ISSModelException ex) 
            {
                Message = ex.Message;
                return Page();
            }
            catch (ISSServiceException ex)
            {
                Message = ex.Message;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
