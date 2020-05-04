using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class FootballNoteUnit
    {
        public FootballNote ParentNote{ get; set; }
        public List<FootballNote> ChildrenNotes{ get; set; }

        public FootballNoteUnit(FootballNote parent) 
        {
            ParentNote = parent;
        }
    }
}
