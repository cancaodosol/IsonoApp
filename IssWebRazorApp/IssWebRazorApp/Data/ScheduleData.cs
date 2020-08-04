using IssWebRazorApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Data
{
    [Table("Schedule")]
    public class ScheduleData
    {
        [Key]
        public int ScheduleId { get; set; }
        public string EventType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public string Place { get; set; }

        public Schedule ToModel()
        {
            var schedule = new Schedule(ScheduleId);
            schedule.ChangeEventType(EventType);
            schedule.ChangeTitle(Title);
            schedule.ChangeScheduleDate(StartDate,EndDate);
            schedule.ChangeContext(new Context(Context));
            schedule.ChangePlace(new Place(Place));

            return schedule;
        }
    }
}
