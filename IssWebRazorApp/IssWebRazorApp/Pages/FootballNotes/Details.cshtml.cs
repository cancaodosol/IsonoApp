using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssWebRazorApp.Data;
using IssWebRazorApp.Models;

namespace IssWebRazorApp.FootballNotes
{
    public class DetailsModel : PageModel
    {
        private readonly IFootballNoteService _footballNoteService;

        public DetailsModel(IFootballNoteRepository repository)
        {
            _footballNoteService = new FootballNoteService(repository);
        }

        public FootballNoteUnit FootballNoteUnit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FootballNoteUnit = _footballNoteService.FindUnit((int)id);

            if (FootballNoteUnit == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
