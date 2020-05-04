using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class FootballNotePostRequestModel
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public string PhotoUrl { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public int LastUpdateUserId { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int TargetNoteId { get; set; }
        public string TargetSession { get; set; }
        public string TargetCategoryCode { get; set; }
        public int TargetPlaybookId { get; set; }
        public int TargetPositionId { get; set; }
        public int TargetScheduleId { get; set; }

    }
}
