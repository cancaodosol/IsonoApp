using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IssWebRazorApp.Data;
using IssWebRazorApp.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace IssWebRazorApp.Playbooks
{
    public class CreateModel : PageModel
    {
        private readonly IPlaybookRepository _playbookRepository;
        public SelectList CaterogyList;

        public CreateModel(IPlaybookRepository playbookRepository)
        {
            _playbookRepository = playbookRepository;
            IList<Category> categories = _playbookRepository.GetCategoryList("Offense");
            CaterogyList = new SelectList(categories,"Code","Name");
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PlaybookData PlaybookData { get; set; }
        public IFormFile PlayDesignFile { get; set; }

        public string Message { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var createUser = new User(71, "hide");
                var playbook = new Playbook(PlaybookData, PlayDesignFile, createUser);
                _playbookRepository.Add(playbook, "Offense");
            }
            catch (Exception ex) 
            {
                Message = ex.Message;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
