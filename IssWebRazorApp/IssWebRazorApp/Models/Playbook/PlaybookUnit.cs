using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class PlaybookUnit
    {
        public string UnitTitle { get; set; }
        public IList<Playbook> Playbooks { get; set; }
        public PlaybookUnit() 
        {
            Playbooks = new List<Playbook>();
        }
    }
}
