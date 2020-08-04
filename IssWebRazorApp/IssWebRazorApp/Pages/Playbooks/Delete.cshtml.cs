using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssWebRazorApp.Data;
using IssWebRazorApp.Models.Common;
using IssWebRazorApp.Models;

namespace IssWebRazorApp.Playbooks
{
    public class DeleteModel : PageModel
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;
        private readonly SessionService _sessionService;
        public User LoginUser;

        public DeleteModel(IssWebRazorApp.Data.IssWebRazorAppContext context)
        {
            _context = context;
            _sessionService = new SessionService();
        }

        [BindProperty]
        public PlaybookData PlaybookData { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            LoginUser = _sessionService.GetLoginUser(HttpContext);
            if (LoginUser == null) return RedirectToPage("/Login");

            if (id == null)
            {
                return NotFound();
            }

            PlaybookData = await _context.PlaybookData.FirstOrDefaultAsync(m => m.PlaybookSystemId == id);

            if (PlaybookData == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            LoginUser = _sessionService.GetLoginUser(HttpContext);
            if (LoginUser == null) return RedirectToPage("/Login");

            if (id == null)
            {
                return NotFound();
            }

            PlaybookData = await _context.PlaybookData.FindAsync(id);

            if (PlaybookData != null)
            {
                _context.PlaybookData.Remove(PlaybookData);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
