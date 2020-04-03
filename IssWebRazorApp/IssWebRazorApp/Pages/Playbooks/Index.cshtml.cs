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

namespace IssWebRazorApp.Playbooks
{
    public class IndexModel : PageModel
    {
        private const string Session = "Offense";
        private readonly IPlaybookRepository _playbookRepository;
        public Hashtable InstallStatuses = InstallSatusService.GetHushtable();
        public IList<Category> Cotegories;

        public IndexModel(IPlaybookRepository playbookRepository)
        {
            _playbookRepository = playbookRepository;
            InstallStatuses = InstallSatusService.GetHushtable();
            Cotegories = _playbookRepository.GetCategoryList(Session); 
        }

        public IList<Playbook> Playbooks { get;set; }

        public async Task OnGetAsync()
        {
            Playbooks = _playbookRepository.FindAll();
        }
    }
}
