using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HelloRazorWorldApp.Models;

namespace HelloRazorWorldApp.Data
{
    public class HelloRazorWorldAppContext : DbContext
    {
        public HelloRazorWorldAppContext (DbContextOptions<HelloRazorWorldAppContext> options)
            : base(options)
        {
        }

        public DbSet<HelloRazorWorldApp.Models.Person> Person { get; set; }

        public DbSet<HelloRazorWorldApp.Models.Message> Message { get; set; }
    }
}
