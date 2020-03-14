using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HelloRazorWorldApp.Data;
using HelloRazorWorldApp.Models;

namespace HelloRazorWorldApp.Pages.Messages
{
    public class CreateModel : PageModel
    {
        private readonly HelloRazorWorldApp.Data.HelloRazorWorldAppContext _context;

        public CreateModel(HelloRazorWorldApp.Data.HelloRazorWorldAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PersonKey"] = new SelectList(_context.Person, "PersonId", "Name");
            return Page();
        }

        [BindProperty]
        public Message Message { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Message.Add(Message);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
