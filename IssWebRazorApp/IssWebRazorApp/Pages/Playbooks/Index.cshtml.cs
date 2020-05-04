using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssWebRazorApp.Data;
using IssWebRazorApp.Models;
using System.Collections;
using IssWebRazorApp.Models.Common;
using Microsoft.AspNetCore.Http;

namespace IssWebRazorApp.Playbooks
{
    public class IndexModel : PageModel
    {
        private const string Session = "Offense";
        private readonly IPlaybookRepository _playbookRepository;
        private readonly SessionService _sessionService;
        public Hashtable InstallStatuses = InstallSatusService.GetHushtable();
        public IList<Category> Cotegories;
        public User LoginUser;

        public IndexModel(IPlaybookRepository playbookRepository)
        {
            _playbookRepository = playbookRepository;
            _sessionService = new SessionService();
            InstallStatuses = InstallSatusService.GetHushtable();
            Cotegories = _playbookRepository.GetCategoryList(Session);
        }

        public IList<Playbook> Playbooks { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            //LoginUser = (User)_sessionService.Get(HttpContext,"LoginUser");
            //if(LoginUser == null)return RedirectToPage("/Login");
            
            Playbooks = _playbookRepository.FindAll();
            return Page();
        }
    }
}
