using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Data
{
    [Table("ScheduleAnswers")]
    public class ScheduleAnswerData
    {
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
        [Display(Name = "回答")]
        public string Answer { get; set; }
        [Display(Name = "コメント")]
        public string Comment { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastUpdateDate { get; set; }
    }
}
