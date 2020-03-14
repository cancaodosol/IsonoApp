using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HelloRazorWorldApp.Data;
using HelloRazorWorldApp.Models;

namespace HelloRazorWorldApp.Pages.Messages
{
    public class IndexModel : PageModel
    {
        private readonly HelloRazorWorldApp.Data.HelloRazorWorldAppContext _context;

        public IndexModel(HelloRazorWorldApp.Data.HelloRazorWorldAppContext context)
        {
            _context = context;
        }

        public IList<Message> Message { get;set; }

        public async Task OnGetAsync()
        {
            Message = await _context.Message
                .Include(m => m.Person).ToListAsync();
        }
    }
}
