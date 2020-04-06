using IssWebRazorApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models.Schedule
{
    public class Schedule
    {
        public int ScheduleId { get;}
        public string EventDate { get;}
        public DateTime StartDate { get;}
        public DateTime EndDate { get;}
        public string EventType { get;}
        public string Title { get;}
        public Context Context { get;}
        public Place Place { get;}

        public Schedule(int id) 
        {
            ScheduleId = id;
        }
        public Schedule(ScheduleData data) 
        {
            ChangeEventType(data.EventType);
            ChangeTitle(data.Title);
            ChangeContext(new Context(data.Context));
            ChangePlace(new Place(data.Place));
            ChangeScheduleDate(data.StartDate, data.EndDate);
        }

        public void ChangeEventType(string type) 
        {
            EventType = type;
        }
        public void ChangeTitle(string title) 
        {
            Title = title;
        }
        public void ChangeContext(Context context) 
        {
            Context = context;
        }
        public void ChangePlace(Place place) 
        {
            Place = place;
        }

        public void ChangeScheduleDate(DateTime startDate , DateTime endDate) 
        {
            StartDate = startDate;
            EndDate = endDate;
            ChangeEventDate();
        }

        private void ChangeEventDate()
        {
            string eventDayOfWeek;

            DayOfWeek dow = StartDate.DayOfWeek;
            switch (dow)
            {
                case DayOfWeek.Saturday:
                    eventDayOfWeek = "土";
                    break;
                case DayOfWeek.Sunday:
                    eventDayOfWeek = "日";
                    break;
                case DayOfWeek.Monday:
                    eventDayOfWeek = "月";
                    break;
                case DayOfWeek.Tuesday:
                    eventDayOfWeek = "火";
                    break;
                case DayOfWeek.Wednesday:
                    eventDayOfWeek = "水";
                    break;
                case DayOfWeek.Thursday:
                    eventDayOfWeek = "木";
                    break;
                case DayOfWeek.Friday:
                    eventDayOfWeek = "金";
                    break;
                default:
                    eventDayOfWeek = "";
                    break;
            }

            EventDate = StartDate.ToString("M/d") +"["+ eventDayOfWeek +"] " + StartDate.ToString("HH:mm") + "-" + EndDate.ToString("HH:mm");
        }
    }
    public enum EventType
    {
        Practice = 0,
        Meeting = 1,
        PracticeGame = 2,
        Game = 3,
        Other = 9
    }
}
