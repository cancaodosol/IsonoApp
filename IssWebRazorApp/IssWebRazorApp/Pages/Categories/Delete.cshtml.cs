using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssWebRazorApp.Data;

namespace IssWebRazorApp.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;

        public DeleteModel(IssWebRazorApp.Data.IssWebRazorAppContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryData = await _context.CategoryData.FindAsync(id);

            if (CategoryData != null)
            {
                _context.CategoryData.Remove(CategoryData);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
