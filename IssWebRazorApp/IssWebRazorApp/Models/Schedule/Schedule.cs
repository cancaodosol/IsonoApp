using IssWebRazorApp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models.Schedule
{
    public class Schedule
    {
        public int ScheduleId { get; private set; }
        public string EventDate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string EventType { get; private set; }
        public string Title { get; private set; }
        public Context Context { get; private set; }
        public Place Place { get; private set; }

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
        Practice,
        Meeting,
        PracticeGame,
        Game,
        Other
    }

    public static class EventTypeService 
    {
        private readonly static string[] EventName = { "練習", "МＴ", "練習試合", "試合", "その他" };
        public static string DisplayName(this EventType type)
        {
            return EventName[(int)type];
        }

        public static string GetName(string type) 
        {
            string name;
            try 
            {
                name = EventName[int.Parse(type)];
            }
            catch (Exception ex) 
            {
                name = "";
            }
            return name;
        }

        public static SelectList GetSelectList() 
        {
            var types = GetHashtable();
            return new SelectList(types,"Key","Value");
        }
        public static Hashtable GetHashtable() 
        {
            var types = new Hashtable();
            foreach (var type in Enum.GetValues(typeof(EventType))) 
            {
                string key = ((int)type).ToString();
                string value = ((EventType)type).DisplayName();
                types.Add(key,value);
            }
            return types;
        }
    }
}
