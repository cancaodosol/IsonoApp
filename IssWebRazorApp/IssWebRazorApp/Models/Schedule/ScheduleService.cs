using IssWebRazorApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IUserService _userService;
        public ScheduleService(IScheduleRepository scheduleRepository,IUserRepository userRepository) 
        {
            _scheduleRepository = scheduleRepository;
            _userService = new UserService(userRepository);
        }
        public IList<Schedule> FindAll()
        {
            var datas = _scheduleRepository.FindAll();

            var schedules = new List<Schedule>();
            foreach (var data in datas)
            {
                var schedule = data.ToModel();
                schedules.Add(schedule);
            }

            return schedules;
        }

        public IList<Schedule> FindRecentry(int day)
        {
            var datas = _scheduleRepository.FindRecently(day);

            var schedules = new List<Schedule>();
            foreach (var data in datas)
            {
                var schedule = data.ToModel();
                schedules.Add(schedule);
            }

            return schedules;
        }

        public IList<Schedule> FindRecentry(int day,int userId) 
        {
            var schedules = this.FindRecentry(day);

            foreach (var schedule in schedules)
            {
                var answers = _scheduleRepository.GetScheduleAnswers(schedule.ScheduleId);
                foreach (var answer in answers)
                {
                    var user = _userService.Find(answer.UserId);
                    var scheduleAnswer = new ScheduleAnswer(answer,user);
                    switch (scheduleAnswer.Answer) 
                    {
                        case "OK":
                            schedule.OKAnswers.Add(scheduleAnswer);
                            break;
                        case "NG":
                            schedule.NGAnswers.Add(scheduleAnswer);
                            break;
                        case "HOLD":
                            schedule.HOLDAnswers.Add(scheduleAnswer);
                            break;
                    }
                    if (scheduleAnswer.User.UserId == userId) schedule.LoginUsersAnswer = scheduleAnswer;
                }
            }

            return schedules;
        }
    }
}
