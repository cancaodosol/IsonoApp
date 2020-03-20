using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IssWebRazorApp.Data;

namespace IssWebRazorApp.Categories
{
    public class EditModel : PageModel
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;

        public EditModel(IssWebRazorApp.Data.IssWebRazorAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CategoryData CategoryData { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryData = await _context.CategoryData.FirstOrDefaultAsync(m => m.CategoryId == id);

            if (CategoryData == null)
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

            _context.Attach(CategoryData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryDataExists(CategoryData.CategoryId))
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

        private bool CategoryDataExists(int id)
        {
            return _context.CategoryData.Any(e => e.CategoryId == id);
        }
    }
}
