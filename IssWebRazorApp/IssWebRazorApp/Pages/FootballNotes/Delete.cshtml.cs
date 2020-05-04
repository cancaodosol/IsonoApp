using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssWebRazorApp.Data;
using IssWebRazorApp.Models;
using IssWebRazorApp.Models.Common;

namespace IssWebRazorApp.FootballNotes
{
    public class DeleteModel : PageModel
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;
        private readonly SessionService _sessionService;
        public DeleteModel(IssWebRazorApp.Data.IssWebRazorAppContext context)
        {
            _context = context;
            _sessionService = new SessionService();
        }

        public User LoginUser;

        [BindProperty]
        public FootballNoteData FootballNoteData { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LoginUser = _sessionService.GetLoginUser(HttpContext);
            if (LoginUser == null) return RedirectToPage("/Login");

            FootballNoteData = await _context.FootballNoteData.FirstOrDefaultAsync(m => m.NoteId == id);

            if (FootballNoteData == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LoginUser = _sessionService.GetLoginUser(HttpContext);
            if (LoginUser == null) return RedirectToPage("/Login");

            FootballNoteData = await _context.FootballNoteData.FindAsync(id);

            if (FootballNoteData != null)
            {
                _context.FootballNoteData.Remove(FootballNoteData);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
