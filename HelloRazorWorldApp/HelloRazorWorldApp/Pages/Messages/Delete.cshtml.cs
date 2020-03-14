using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelloRazorWorldApp.Data;
using HelloRazorWorldApp.Models;

namespace HelloRazorWorldApp.Pages.Messages
{
    public class DeleteModel : PageModel
    {
        private readonly HelloRazorWorldApp.Data.HelloRazorWorldAppContext _context;

        public DeleteModel(HelloRazorWorldApp.Data.HelloRazorWorldAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Message Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Message = await _context.Message
                .Include(m => m.Person).FirstOrDefaultAsync(m => m.MessageId == id);

            if (Message == null)
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

            Message = await _context.Message.FindAsync(id);

            if (Message != null)
            {
                _context.Message.Remove(Message);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
