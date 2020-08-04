using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IssWebRazorApp.Data;
using IssWebRazorApp.Models.Common;
using IssWebRazorApp.Models;

namespace IssWebRazorApp.Users
{
    public class EditModel : PageModel
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;
        private readonly SessionService _sessionService;
        public User LoginUser;

        public EditModel(IssWebRazorApp.Data.IssWebRazorAppContext context)
        {
            _context = context;
            _sessionService = new SessionService();
        }

        [BindProperty]
        public UserData UserData { get; set; }
        public SelectList PositionDatas { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            LoginUser = _sessionService.GetLoginUser(HttpContext);
            if (LoginUser == null) return RedirectToPage("/Login");
            if (!(LoginUser.SystemRole.Equals(SystemRole.Admin.ToString())
                ||LoginUser.SystemRole.Equals(SystemRole.Owner.ToString()))) return RedirectToPage("/Users");


            UserData = await _context.UserData
                .Include(u => u.PositionData).FirstOrDefaultAsync(m => m.UserId == id);

            if (UserData == null)
            {
                return NotFound();
            }
           PositionDatas = new SelectList(_context.PositionData, "PositionId", "FullName");
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDataExists(UserData.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserDataExists(int id)
        {
            return _context.UserData.Any(e => e.UserId == id);
        }
    }
}
