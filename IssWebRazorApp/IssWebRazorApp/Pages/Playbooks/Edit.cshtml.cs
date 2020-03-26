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
using Microsoft.AspNetCore.Http;
using System.IO;

namespace IssWebRazorApp.Playbooks
{
    public class EditModel : PageModel
    {
        private readonly IPlaybookRepository _playbookRepository;
        public SelectList CaterogyList;
        public SelectList StatusList;

        public EditModel(IPlaybookRepository playbookRepository)
        {
            _playbookRepository = playbookRepository;
            IList<Category> categories = _playbookRepository.GetCategoryList("Offense");
            CaterogyList = new SelectList(categories, "Code", "Name");
            StatusList = InstallSatusService.GetSelectList();
        }

        [BindProperty]
        public PlaybookData PlaybookData { get; set; }
        public Playbook Playbook { get; set; }
        public IFormFile PlayDesignFile { get; set; }
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Playbook = _playbookRepository.Find((int)id);

            if (Playbook == null)
            {
                return NotFound();
            }

            PlaybookData = Playbook.ToData();

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

            try
            {
                Playbook = _playbookRepository.Find(PlaybookData.PlaybookSystemId);

                var updateUser = new User(71,"");

                Playbook.ChangePlaybook(PlaybookData,PlayDesignFile,updateUser);

                _playbookRepository.Edit(Playbook, "Offense");
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
