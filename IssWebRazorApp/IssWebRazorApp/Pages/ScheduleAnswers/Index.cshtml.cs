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
    public class IndexModel : PageModel
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;

        public IndexModel(IssWebRazorApp.Data.IssWebRazorAppContext context)
        {
            _context = context;
        }

        public IList<ScheduleAnswerData> ScheduleAnswerData { get;set; }

        public async Task OnGetAsync()
        {
            ScheduleAnswerData = await _context.ScheduleAnswerData.ToListAsync();
        }
    }
}
