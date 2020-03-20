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
    public class DetailsModel : PageModel
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;

        public DetailsModel(IssWebRazorApp.Data.IssWebRazorAppContext context)
        {
            _context = context;
        }

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
    }
}
