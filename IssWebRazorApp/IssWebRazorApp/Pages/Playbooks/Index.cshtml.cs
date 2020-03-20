using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssWebRazorApp.Data;
using IssWebRazorApp.Models;

namespace IssWebRazorApp.Playbooks
{
    public class IndexModel : PageModel
    {
        private readonly IPlaybookRepository _playbookRepository;

        public IndexModel(IPlaybookRepository playbookRepository)
        {
            _playbookRepository = playbookRepository;
        }

        public IList<Playbook> Playbooks { get;set; }

        public async Task OnGetAsync()
        {
            Playbooks = _playbookRepository.FindAll();
        }
    }
}
