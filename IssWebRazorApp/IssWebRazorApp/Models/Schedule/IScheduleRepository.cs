using IssWebRazorApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public interface IScheduleRepository
    {
        public IList<ScheduleData> FindAll();

        public IList<ScheduleData> FindRecently(int day);
        public IList<ScheduleAnswerData> GetScheduleAnswers(int scheduleId);
    }
}
