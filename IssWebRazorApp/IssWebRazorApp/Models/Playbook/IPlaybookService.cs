using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public interface IPlaybookService
    {
        public Playbook Find(int id);
        public List<Playbook> FindAll();
        public List<PlaybookUnit> FindAll(PlaybookSortType type);
        public Task Add(Playbook playbook);
        public Task Edit(Playbook playbook);
        public List<Category> GetCategoryList(string Session);
    }
}
