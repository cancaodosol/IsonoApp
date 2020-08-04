using IssWebRazorApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class ScheduleAnswer
    {
        public int ScheduleId { get; set; }
        public User User { get; set; }
        public string Answer { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public ScheduleAnswer(ScheduleAnswerData data,User user) 
        {
            ScheduleId = data.ScheduleId;
            User = user;
            Answer = data.Answer;
            Comment = data.Comment;
            CreateDate = data.CreateDate;
            LastUpdateDate = data.LastUpdateDate;
        }
        /*
        public ScheduleAnswer(int scheduleId,string answer,User user)
        {
            ScheduleId = scheduleId;
            User = user;
            Answer = answer;
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
        }

        public void ChangeAnswer(string answer) 
        {
            Answer = answer;
            LastUpdateDate = DateTime.Now;
        }
        */
    }
}
