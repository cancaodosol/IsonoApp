using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IssWebRazorApp.Data;

namespace IssWebRazorApp.ScheduleAnswers
{
    public class CreateModel : PageModel
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;

        public CreateModel(IssWebRazorApp.Data.IssWebRazorAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ScheduleAnswerData ScheduleAnswerData { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ScheduleAnswerData.Add(ScheduleAnswerData);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
