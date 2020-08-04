using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IssWebRazorApp.Data;
using IssWebRazorApp.Models;

namespace IssWebRazorApp.ScheduleManagement
{
    public class EditModel : PageModel
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;

        public SelectList EventTypeList;
        public EditModel(IssWebRazorApp.Data.IssWebRazorAppContext context)
        {
            _context = context;
            EventTypeList = EventTypeService.GetSelectList();
        }

        [BindProperty]
        public ScheduleData ScheduleData { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ScheduleData = await _context.ScheduleData.FirstOrDefaultAsync(m => m.ScheduleId == id);

            if (ScheduleData == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ScheduleData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleDataExists(ScheduleData.ScheduleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ScheduleDataExists(int id)
        {
            return _context.ScheduleData.Any(e => e.ScheduleId == id);
        }
    }
}
