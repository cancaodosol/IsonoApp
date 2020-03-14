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
    public class DetailsModel : PageModel
    {
        private readonly HelloRazorWorldApp.Data.HelloRazorWorldAppContext _context;

        public DetailsModel(HelloRazorWorldApp.Data.HelloRazorWorldAppContext context)
        {
            _context = context;
        }

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
    }
}
