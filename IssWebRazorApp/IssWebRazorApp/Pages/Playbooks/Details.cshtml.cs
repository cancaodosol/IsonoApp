using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssWebRazorApp.Data;

namespace IssWebRazorApp.Playbooks
{
    public class DetailsModel : PageModel
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;

        public DetailsModel(IssWebRazorApp.Data.IssWebRazorAppContext context)
        {
            _context = context;
        }

        public PlaybookData PlaybookData { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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
    }
}
