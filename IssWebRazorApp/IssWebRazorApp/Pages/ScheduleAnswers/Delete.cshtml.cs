using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssWebRazorApp.Data;

namespace IssWebRazorApp.ScheduleAnswers
{
    public class DeleteModel : PageModel
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;

        public DeleteModel(IssWebRazorApp.Data.IssWebRazorAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ScheduleAnswerData ScheduleAnswerData { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ScheduleAnswerData = await _context.ScheduleAnswerData.FirstOrDefaultAsync(m => m.ScheduleId == id);

            if (ScheduleAnswerData == null)
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

            ScheduleAnswerData = await _context.ScheduleAnswerData.FindAsync(id);

            if (ScheduleAnswerData != null)
            {
                _context.ScheduleAnswerData.Remove(ScheduleAnswerData);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
