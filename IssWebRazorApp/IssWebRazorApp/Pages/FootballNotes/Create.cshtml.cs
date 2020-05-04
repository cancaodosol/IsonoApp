using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IssWebRazorApp.Data;
using IssWebRazorApp.Models;
using IssWebRazorApp.Models.Common;
using Microsoft.AspNetCore.Http;
using IssWebRazorApp.Models.Exceptions;

namespace IssWebRazorApp.FootballNotes
{
    public class CreateModel : PageModel
    {
        private readonly IFootballNoteService _footballNoteService;
        private readonly SessionService _sessionService;

        public CreateModel(IFootballNoteRepository repository)
        {
            _footballNoteService = new FootballNoteService(repository);
            _sessionService = new SessionService();
        }

        public NoteType NoteType;
        public User LoginUser;
        [BindProperty]
        public int TargetNoteId { get; set; }
        public string Message { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LoginUser = _sessionService.GetLoginUser(HttpContext);
            if (LoginUser == null) return RedirectToPage("/Login");
            
            TargetNoteId = (int)id;

            if ((int)id == 0) NoteType = NoteType.Parent;
            else NoteType = NoteType.Child;

            return Page();
        }

        [BindProperty]
        public FootballNotePostRequestModel NoteModel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            LoginUser = _sessionService.GetLoginUser(HttpContext);
            if (LoginUser == null) return RedirectToPage("/Login");

            try
            {
                NoteModel.TargetNoteId = TargetNoteId;
                var note = new FootballNote(NoteModel, LoginUser);
                await _footballNoteService.AddAsync(note);
            }
            catch (ISSModelException ex)
            {
                Message = ex.Message;
                return Page();
            }
            catch (ISSServiceException ex)
            {
                Message = ex.Message;
                return Page();
            }
            catch (ISSRepositoryException ex)
            {
                Message = ex.Message;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
