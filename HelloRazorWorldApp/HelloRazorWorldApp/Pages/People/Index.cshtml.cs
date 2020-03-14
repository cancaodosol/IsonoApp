using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelloRazorWorldApp.Data;
using HelloRazorWorldApp.Models;

namespace HelloRazorWorldApp.Pages.People
{
    public class IndexModel : PageModel
    {
        private readonly HelloRazorWorldApp.Data.HelloRazorWorldAppContext _context;

        public IndexModel(HelloRazorWorldApp.Data.HelloRazorWorldAppContext context)
        {
            _context = context;
        }

        public IList<Person> Person { get;set; }

        public async Task OnGetAsync()
        {
            Person = await _context.Person.Include("Messages").ToListAsync();
        }
        public async Task OnPostAsync(string Find)
        {
            Person = await _context.Person.Where(m => m.Name.Contains(Find) || m.Mail.Contains(Find)).ToListAsync();
        }
    }
}
