using IssWebRazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Data
{
    public interface IPlaybookRepository
    {
        public void Add(Playbook playbook);
    }
}
