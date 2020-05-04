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
using IssWebRazorApp.Models.Common;
using IssWebRazorApp.Models.Exceptions;
using DocumentFormat.OpenXml.Bibliography;

namespace IssWebRazorApp.Playbooks
{
    public class CreateModel : PageModel
    {
        private readonly IPlaybookRepository _playbookRepository;
        private readonly SessionService _sessionService;
        public User LoginUser;
        public SelectList CaterogyList;
        public SelectList StatusList;

        public CreateModel(IPlaybookRepository playbookRepository)
        {
            _playbookRepository = playbookRepository;
            IList<Category> categories = _playbookRepository.GetCategoryList("Offense");
            _sessionService = new SessionService();
            CaterogyList = new SelectList(categories,"Code","Name");
            StatusList = InstallSatusService.GetSelectList();
            PlaybookData = new PlaybookData();
        }

        public IActionResult OnGet()
        {
            LoginUser = (User)_sessionService.Get(HttpContext, "LoginUser");
            if (LoginUser == null) return RedirectToPage("/Login");
            PlaybookData.IntroduceStatus = ((int)InstallStatus.Want_Instoll).ToString();
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
                var sessionService = new SessionService();
                var createUser = (User)sessionService.ToObject(HttpContext.Session.Get("LoginUser"));
                var playbook = new Playbook(PlaybookData, PlayDesignFile, createUser);
                _playbookRepository.Add(playbook, "Offense");
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
