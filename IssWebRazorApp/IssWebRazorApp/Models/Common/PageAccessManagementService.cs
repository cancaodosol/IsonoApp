using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models.Common
{
    public class PageAccessManagementService
    {
        public bool CanAccsessThisPage(IssWebRazorApp.Playbooks.IndexModel page,User user) 
        {
            bool result = true;
            return result;
        }
    }
}
