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
    public class IndexModel : PageModel
    {
        private readonly IFootballNoteService _footballNoteService;

        public IndexModel(IFootballNoteRepository repository)
        {
            _footballNoteService = new FootballNoteService(repository);
        }

        public IList<FootballNoteUnit> FootballNoteUnits { get;set; }

        public async Task OnGetAsync()
        {
            FootballNoteUnits = _footballNoteService.FindAllUnits().OrderByDescending(_ => _.ParentNote.Editor.CreateDate).ToList<FootballNoteUnit>();
        }
    }
}
