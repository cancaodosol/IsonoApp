using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class NoteTarget
    {
        public int NoteId { get; set; }
        public string Session { get; set; }
        public string CategoryCode { get; set; }
        public int PlaybookId { get; set; }
        public int PositionId { get; set; }
        public int ScheduleId { get; set; }
    }
}
