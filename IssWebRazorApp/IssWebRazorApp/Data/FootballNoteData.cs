using IssWebRazorApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Data
{
    [Table("FootballNotes")]
    public class FootballNoteData
    {
        [Key]
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public string PhotoUrl { get; set; }
        public int CreateUserId { get; set; }
        [ForeignKey("CreateUserId")]
        public UserData CreateUserData { get; set; }
        public DateTime CreateDate { get; set; }
        public int LastUpdateUserId { get; set; }
        [ForeignKey("LastUpdateUserId")]
        public UserData LastUpdateUserData { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int TargetNoteId { get; set; }
        public string TargetSession { get; set; }
        public string TargetCategoryCode { get; set; }
        public int TargetPlaybookId { get; set; }
        public int TargetPositionId { get; set; }
        public int TargetScheduleId { get; set; }

        public FootballNote ToModel() 
        {
            FootballNote note = new FootballNote(this);
            return note;
        }
    }
}
