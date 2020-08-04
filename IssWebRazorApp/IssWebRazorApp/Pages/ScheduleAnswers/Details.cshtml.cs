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
    public class DetailsModel : PageModel
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;

        public DetailsModel(IssWebRazorApp.Data.IssWebRazorAppContext context)
        {
            _context = context;
        }

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
    }
}
