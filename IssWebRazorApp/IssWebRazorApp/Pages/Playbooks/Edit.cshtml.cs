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
using IssWebRazorApp.Models.Common;

namespace IssWebRazorApp.Playbooks
{
    public class EditModel : PageModel
    {
        private readonly IPlaybookService _playbookService;
        private readonly SessionService _sessionService;
        public User LoginUser;
        public SelectList CaterogyList;
        public SelectList StatusList;

        public EditModel(IPlaybookRepository playbookRepository)
        {
            _playbookService = new PlaybookService(playbookRepository);
            IList<Category> categories = _playbookService.GetCategoryList("Offense");
            _sessionService = new SessionService();
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
            LoginUser = _sessionService.GetLoginUser(HttpContext);
            if (LoginUser == null) return RedirectToPage("/Login");

            if (id == null)
            {
                return NotFound();
            }

            Playbook = _playbookService.Find((int)id);

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
            LoginUser = _sessionService.GetLoginUser(HttpContext);
            if (LoginUser == null) return RedirectToPage("/Login");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                Playbook = _playbookService.Find(PlaybookData.PlaybookSystemId);

                var updateUser = LoginUser;
                Playbook.ChangePlaybook(PlaybookData,PlayDesignFile,updateUser);

                _playbookService.Edit(Playbook);
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
