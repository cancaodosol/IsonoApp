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
        private readonly IPlaybookService _playbookService;
        private readonly SessionService _sessionService;
        public Hashtable InstallStatuses = InstallSatusService.GetHushtable();
        public IList<Category> Cotegories;
        public User LoginUser;

        public IndexModel(IPlaybookRepository playbookRepository)
        {
            _playbookService = new PlaybookService(playbookRepository);
            _sessionService = new SessionService();
            InstallStatuses = InstallSatusService.GetHushtable();
            Cotegories = _playbookService.GetCategoryList(Session);
        }

        public IList<PlaybookUnit> PlaybookUnits { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {            
            PlaybookUnits = _playbookService.FindAll(PlaybookSortType.Category);
            return Page();
        }
    }
}
